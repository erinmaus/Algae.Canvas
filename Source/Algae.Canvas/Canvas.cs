using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace CommaExcess.Algae.Graphics
{
	public partial class Canvas : IDisposable
	{
		Renderer renderer;
		ICanvasMaterial material;
		CanvasSceneWalker walker = new CanvasSceneWalker();
		GroupDrawable rootGroupObject;
		Stack<GroupDrawable> nestedGroups = new Stack<GroupDrawable>();
		Stack<GroupDrawable> nestedClipGroups = new Stack<GroupDrawable>();

		List<Drawable> translucentDrawables = new List<Drawable>();
		List<Drawable> opaqueDrawables = new List<Drawable>();

		const int TextureSize = 512;
		Texture2D colorsTexture, depthsTexture;

		Mesh mesh;
		List<DrawAction> actions = new List<DrawAction>();
		MeshData<Color> colors = new MeshData<Color>(TextureSize * TextureSize);
		MeshData<float> depths = new MeshData<float>(TextureSize * TextureSize);
		MeshData<PathVertex> vertices = new MeshData<PathVertex>();
		MeshData<uint> indices = new MeshData<uint>();
		int currentPath;
		uint currentPathIndex;

		object taskSync = new object();
		SemaphoreSlim taskSignalStart, taskSignalFinish;
		BlockingCollection<Task> taskQueue = new BlockingCollection<Task>();
		List<Thread> taskThreads = new List<Thread>();

		int foobars;

		public bool IsMultithreaded
		{
			get { return taskThreads.Count > 0; }
		}

		int clipping = 0;
		bool isInClip = false;

		Path clipRectangle;

		public Canvas(Renderer renderer, ICanvasMaterial material, int threadCount = 2)
		{
			this.renderer = renderer;
			this.material = material;

			BuildClipRectangle();
			GenerateThreads(threadCount);

			colorsTexture = new Texture2D(renderer, TextureSize, TextureSize, TextureFormat.RedGreenBlueAlpha8);
			depthsTexture = new Texture2D(renderer, TextureSize, TextureSize, TextureFormat.Red32);

			mesh = new Mesh(renderer, PathVertex.VertexDeclaration) { IsDynamic = true, CacheBuffers = false };
			material.Prepare(mesh);
		}

		void BuildClipRectangle()
		{
			clipRectangle = new Path();
			clipRectangle.MoveTo(Vector2.Zero);
			clipRectangle.LineTo(Vector2.UnitY);
			clipRectangle.LineTo(Vector2.One);
			clipRectangle.LineTo(Vector2.UnitX);
			clipRectangle.End();
		}

		void GenerateThreads(int count)
		{
			if (count > 0)
			{
				taskSignalStart = new SemaphoreSlim(0, count);
				taskSignalFinish = new SemaphoreSlim(count, count);

				for (int i = 0; i < count; i++)
				{
					Thread thread = new Thread(PerformTaskThread);
					taskThreads.Add(thread);

					thread.Start();
				}
			}
		}

		void PerformTaskThread()
		{
			bool isRunning = true;
			bool isDrawing = false;

			while (isRunning)
			{
				taskSignalStart.Wait();
				taskSignalFinish.Wait();
				isDrawing = true;

				while (isDrawing)
				{
					Task task = taskQueue.Take();

					if (task is DisposingTask)
					{
						isRunning = false;
						isDrawing = false;
					}
					else if (task is DoneTask)
					{
						taskSignalFinish.Release();
						isDrawing = false;
					}
					else
					{
						task.PerformTask();
					}
				}
			}
		}

		public void Prepare(Matrix projection)
		{
			foobars = 0;

			clipping = 0;
			isInClip = false;

			actions.Clear();
			colors.Reset();
			depths.Reset();
			vertices.Reset();
			indices.Reset();
			currentPath = 0;
			currentPathIndex = 0;

			translucentDrawables.Clear();
			opaqueDrawables.Clear();
			nestedGroups.Clear();
			nestedClipGroups.Clear();

			rootGroupObject = new GroupDrawable()
			{
				Color = Color.White,
				GlobalColor = Color.White,
				GlobalTransform = Matrix.Identity,
				Transform = Matrix.Identity
			};

			material.Use();
			material.SetProjection(projection);
			material.SetIndexRowLength(TextureSize);

			StartGroup(Color.White, Matrix.Identity);
		}

		public void Finish()
		{
			FinishGroup();
			Flush();
		}

		void EnqueueTask(Task task)
		{
			if (IsMultithreaded)
			{
				taskQueue.Add(task);
			}
			else
			{
				task.PerformTask();
			}
		}

		void Flush()
		{
			walker.Prepare();
			rootGroupObject.Walk(walker);
			rootGroupObject.CalculateDepths(this);
			rootGroupObject.Draw(this);

			FlushDrawables();
		}

		void ResolveClipGroup(GroupDrawable groupDrawable)
		{
			foreach (var drawable in groupDrawable)
			{
				if (drawable is GroupDrawable)
				{
					ResolveClipGroup(drawable as GroupDrawable);
				}
				else if (drawable is PathDrawable)
				{
					ResolvePath(drawable as PathDrawable);
				}
			}
		}

		void ResolvePath(PathDrawable pathDrawable)
		{
			if (!pathDrawable.GeneratedCachedPath)
			{
				Task task = new TransformPathTask(pathDrawable, pathDrawable.PathIndex, currentPathIndex);
				EnqueueTask(task);

				currentPathIndex += (uint)pathDrawable.PathObject.CachedMesh.GetVertices().Length;
				pathDrawable.GeneratedCachedPath = true;
			}
		}

		void ResolvePaths(List<Drawable> drawables)
		{
			for (int i = 0; i < drawables.Count; i++)
			{
				Drawable drawable = drawables[i];

				if (drawable is ClipDrawable)
				{
					ResolveClipGroup((drawable as ClipDrawable).Group);
				}
				else if (drawable is PathDrawable)
				{
					ResolvePath(drawable as PathDrawable);
				}
			}
		}

		void ResolvePaths()
		{
			ResolvePaths(opaqueDrawables);
			ResolvePaths(translucentDrawables);
		}

		void CollectFlushCurrent(ref int indexStart, ClipDrawable clip = null)
		{
			int position = indices.GetOffset();
			int count = position - indexStart;

			if (count != 0)
			{
				actions.Add(new DrawAction(indexStart, count, clip));
				indexStart = position;
			}
		}

		void CollectClipGroup(GroupDrawable groupDrawable, ref int indexStart)
		{
			foreach (var drawable in groupDrawable)
			{
				if (drawable is GroupDrawable)
				{
					CollectClipGroup(drawable as GroupDrawable, ref indexStart);
				}
				else if (drawable is PathDrawable)
				{
					CollectPath(drawable as PathDrawable);
				}
			}
		}

		void CollectPath(PathDrawable path)
		{
			if (!path.CachedVertices)
			{
				vertices.Append(path.CachedPath.GetVertices());
				path.CachedVertices = true;
			}

			indices.Append(path.CachedPath.GetIndices());
		}

		void CollectActions(List<Drawable> drawables, ref int indexStart)
		{
			for (int i = 0; i < drawables.Count; i++)
			{
				Drawable drawable = drawables[i];

				if (drawable is ClipDrawable)
				{
					CollectFlushCurrent(ref indexStart);
					CollectClipGroup((drawable as ClipDrawable).Group, ref indexStart);
					CollectFlushCurrent(ref indexStart, drawable as ClipDrawable);
				}
				else if (drawable is PathDrawable)
				{
					CollectPath(drawable as PathDrawable);
				}
			}
		}

		void CollectActions()
		{
			int indexStart = 0;

			CollectActions(opaqueDrawables, ref indexStart);
			CollectActions(translucentDrawables, ref indexStart);
			CollectFlushCurrent(ref indexStart);
		}

		void CollectTextures()
		{
			colorsTexture.SetData(colors.Get(), TextureFormat.RedGreenBlueAlpha32);
			depthsTexture.SetData(depths.Get(), TextureFormat.Red32);
		}

		void BuildMesh()
		{
			mesh.BufferVertexData(vertices.Get());
			mesh.BufferIndexData(indices.Get(), 4);
		}

		void FlushDrawables()
		{
			if (IsMultithreaded)
			{
				taskSignalStart.Release(taskThreads.Count);
			}

			ResolvePaths();

			CollectTextures();
			material.SetColorTexture(colorsTexture);
			material.SetDepthTexture(depthsTexture);

			if (IsMultithreaded)
			{
				for (int i = 0; i < taskThreads.Count; i++)
				{
					EnqueueTask(new DoneTask());
					taskSignalFinish.Wait();
				}

				taskSignalFinish.Release(taskThreads.Count);
			}

			CollectActions();
			BuildMesh();

			foreach (var action in actions)
			{
				action.Draw(this);
			}
		}

		public void StartClip(Matrix view, BoundingRectangle? rectangle = null)
		{
			if (!isInClip)
			{
				GroupDrawable currentGroup = nestedGroups.Peek();
				GroupDrawable nextGroup = new GroupDrawable()
				{
					GlobalColor = Color.White,
					Color = Color.White,
					GlobalTransform = currentGroup.GlobalTransform * currentGroup.Transform,
					Transform = view
				};

				nestedClipGroups.Push(nextGroup);
				isInClip = true;

				if (rectangle.HasValue)
				{
					Matrix m = Matrix.Translation(new Vector3(rectangle.Value.X, rectangle.Value.Y, 0.0f)) * Matrix.Scale(new Vector3(rectangle.Value.Width, rectangle.Value.Height, 1.0f));
					StartGroup(Color.White, view);
					Paint(clipRectangle, Color.White, m);
					FinishGroup();
				}
			}
		}

		public void UseClip()
		{
			UseClip(Color.White, Matrix.Identity);
		}

		public void UseClip(Color color, Matrix matrix)
		{
			if (isInClip)
			{
				isInClip = false;

				StartGroup(color, matrix);
				GroupDrawable currentGroup = nestedGroups.Peek();

				BeginClipDrawable opaqueBeginClip = new BeginClipDrawable() { Group = nestedClipGroups.Peek().Clone() as GroupDrawable, Color = new Color(0, 0, 0, 1) };
				BeginClipDrawable translucentBeginClip = new BeginClipDrawable() { Group = nestedClipGroups.Peek().Clone() as GroupDrawable, Color = new Color(0, 0, 0, 0) };

				currentGroup.AddDrawable(opaqueBeginClip);
				currentGroup.AddDrawable(translucentBeginClip);
			}
		}

		public void FinishClip()
		{
			if (nestedClipGroups.Count > 0)
			{
				GroupDrawable currentGroup = nestedGroups.Pop();

				EndClipDrawable opaqueEndClip = new EndClipDrawable() { Group = nestedClipGroups.Peek().Clone() as GroupDrawable, Color = new Color(0, 0, 0, 1) };
				EndClipDrawable translucentEndClip = new EndClipDrawable() { Group = nestedClipGroups.Peek().Clone() as GroupDrawable, Color = new Color(0, 0, 0, 0) };

				currentGroup.AddDrawable(opaqueEndClip);
				currentGroup.AddDrawable(translucentEndClip);

				nestedClipGroups.Pop();
			}
		}

		public void StartGroup(Color color, Matrix view)
		{
			GroupDrawable nextGroup;
			GroupDrawable currentGroup;

			if (nestedGroups.Count == 0)
			{
				nextGroup = rootGroupObject;
			}
			else
			{
				if (isInClip)
				{
					currentGroup = nestedClipGroups.Peek();
				}
				else
				{
					currentGroup = nestedGroups.Peek();
				}

				nextGroup = new GroupDrawable()
				{
					GlobalColor = currentGroup.GlobalColor * currentGroup.Color,
					Color = color,
					GlobalTransform = currentGroup.GlobalTransform * currentGroup.Transform,
					Transform = view
				};

				currentGroup.AddDrawable(nextGroup);
			}

			if (isInClip)
			{
				nestedClipGroups.Push(nextGroup);
			}
			else
			{
				nestedGroups.Push(nextGroup);
			}
		}

		public void FinishGroup()
		{
			if (isInClip)
			{
				nestedClipGroups.Pop();
			}
			else
			{
				nestedGroups.Pop();
			}
		}

		public void Paint(Path path, Color color, Vector2 position)
		{
			Paint(path, color, Matrix.Translation(new Vector3(position.X, position.Y, 0.0f)));
		}

		public void Paint(Path path, Color color, Matrix world)
		{
			if (path.IsFinished && (path.CachedMesh != null || path.Compile()))
			{
				PathDrawable p = new PathDrawable() { PathObject = path, Color = color, Transform = world };
				GroupDrawable g;

				if (isInClip)
				{
					g = nestedClipGroups.Peek();
				}
				else
				{
					g = nestedGroups.Peek();
				}

				g.AddDrawable(p);
				p.PathIndex = currentPath++;

				colors.Append(p.GlobalColor);
				depths.Append(0.0f);
			}
		}

		public void Dispose()
		{
			if (IsMultithreaded)
			{
				for (int i = 0; i < taskThreads.Count; i++)
					taskQueue.Add(new DisposingTask());

				taskSignalStart.Release(taskThreads.Count);

				foreach (var thread in taskThreads)
				{
					thread.Join();
				}
			}
		}
	}
}
