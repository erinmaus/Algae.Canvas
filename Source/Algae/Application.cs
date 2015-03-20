using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommaExcess.Algae.Platform;
using CommaExcess.Algae.Graphics;

namespace CommaExcess.Algae
{
	/// <summary>
	/// Defines a platform-agnostic application that processes an event loop.
	/// </summary>
	public class Application : IDisposable, IInitializable
	{
		/// <summary>
		/// Gets a value indicating if the application has been initialized.
		/// </summary>
		public bool IsInitialized
		{
			get;
			private set;
		}

		AlgaeKeyboard keyboard = new AlgaeKeyboard();

		/// <summary>
		/// Gets the keyboard instance.
		/// </summary>
		public Keyboard Keyboard
		{
			get { return keyboard; }
		}

		AlgaeMouse mouse = new AlgaeMouse();

		/// <summary>
		/// Gets the mouse instance.
		/// </summary>
		public Mouse Mouse
		{
			get { return mouse; }
		}

		AlgaeJoystick joystick = new AlgaeJoystick();

		/// <summary>
		/// Gets the joystick manager.
		/// </summary>
		public Joystick Joystick
		{
			get { return joystick; }
		}

		AlgaePlatformContext platformContext = new AlgaePlatformContext();

		/// <summary>
		/// Gets the platform context.
		/// </summary>
		public PlatformContext PlatformContext
		{
			get { return platformContext; }
		}

		AlgaeDisplay display = new AlgaeDisplay();

		/// <summary>
		/// Gets the current display.
		/// </summary>
		public Display Display
		{
			get { return display; }
		}

		AlgaeTimer timer = new AlgaeTimer();

		/// <summary>
		/// Gets or sets the updated interval in seconds.
		/// </summary>
		public float UpdateInterval
		{
			get { return timer.Interval; }
			set { timer.Interval = value; }
		}

		/// <summary>
		/// Gets the elapsed time since initialization.
		/// </summary>
		public float ElapsedTime
		{
			get { return (float)AllegroMethods.al_get_time(); }
		}

		IntPtr queue;
		bool wasUpdated = false;
		bool isRunning = false;

		/// <summary>
		/// Gets a value indicating if the game is running.
		/// </summary>
		public bool IsRunning
		{
			get { return isRunning; }
		}

		GL3Renderer renderer = new GL3Renderer();

		/// <summary>
		/// Gets the renderer.
		/// </summary>
		public Renderer Renderer
		{
			get { return renderer; }
		}

		/// <summary>
		/// Creates a default instance of the application.
		/// </summary>
		public Application()
		{
			// Nothing.
		}

		/// <summary>
		/// Processes an event.
		/// </summary>
		/// <remarks>
		/// Used when a custom application event loop is necessary.
		/// </remarks>
		public void ProcessEvent()
		{
			AllegroEvent e = new AllegroEvent();
			AllegroMethods.al_wait_for_event(queue, ref e);
			AllegroEventType type = (AllegroEventType)e.type;

			// Unsightly code below
			switch (type)
			{
				case AllegroEventType.ALLEGRO_EVENT_JOYSTICK_AXIS:
					OnJoystickAxisChange(new JoystickEventArgs()
					{
						Handle = new JoystickHandle(e.joystick.id),
						Stick = e.joystick.stick,
						Axis = e.joystick.axis,
						Position = e.joystick.pos
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_JOYSTICK_BUTTON_DOWN:
					OnJoystickButtonDown(new JoystickEventArgs()
					{
						Handle = new JoystickHandle(e.joystick.id),
						Button = e.joystick.button
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_JOYSTICK_BUTTON_UP:
					OnJoystickButtonUp(new JoystickEventArgs()
					{
						Handle = new JoystickHandle(e.joystick.id),
						Button = e.joystick.button
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_JOYSTICK_CONFIGURATION:
					AllegroMethods.al_reconfigure_joysticks();

					OnJoystickReconfiguration(EventArgs.Empty);
					break;
				case AllegroEventType.ALLEGRO_EVENT_KEY_DOWN:
					OnKeyDown(new KeyboardEventArgs()
					{
						Key = (KeyCode)e.keyboard.keycode,
						Modifiers = (KeyModifier)e.keyboard.modifiers
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_KEY_CHAR:
					OnKeyInput(new KeyboardEventArgs()
					{
						Key = (KeyCode)e.keyboard.keycode,
						Character = Char.ConvertFromUtf32((int)e.keyboard.unichar),
						Modifiers = (KeyModifier)e.keyboard.modifiers
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_KEY_UP:
					OnKeyUp(new KeyboardEventArgs()
					{
						Key = (KeyCode)e.keyboard.keycode,
						Modifiers = (KeyModifier)e.keyboard.modifiers
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_MOUSE_AXES:
					OnMouseMove(new MouseEventArgs()
					{
						Position = new Vector3(e.mouse.x, Display.Height - e.mouse.y, e.mouse.z),
						Difference = new Vector3(e.mouse.dx, -e.mouse.dy, e.mouse.dz)
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_MOUSE_BUTTON_DOWN:
					OnMouseButtonDown(new MouseEventArgs()
					{
						Position = new Vector3(e.mouse.x, Display.Height - e.mouse.y, e.mouse.z),
						Button = (int)e.mouse.button
					});
					break;
				case AllegroEventType.ALLEGRO_EVENT_MOUSE_BUTTON_UP:
					OnMouseButtonUp(new MouseEventArgs()
					{
						Position = new Vector3(e.mouse.x, Display.Height - e.mouse.y, e.mouse.dz),
						Button = (int)e.mouse.button
					});
					break;
				//case AllegroEventType.ALLEGRO_EVENT_MOUSE_ENTER_DISPLAY:
				//    break;
				//case AllegroEventType.ALLEGRO_EVENT_MOUSE_LEAVE_DISPLAY:
				//    break;
				//case AllegroEventType.ALLEGRO_EVENT_MOUSE_WARPED:
				//    break;
				case AllegroEventType.ALLEGRO_EVENT_TIMER:
					if (e.any.timestamp > startDropTime)
					{
						Update();
						wasUpdated = true;
					}
					break;
				//case AllegroEventType.ALLEGRO_EVENT_DISPLAY_EXPOSE:
				//    break;
				case AllegroEventType.ALLEGRO_EVENT_DISPLAY_RESIZE:
					display.MarkDirty();
					AllegroMethods.al_acknowledge_resize(display.GetDisplayHandle());
					OnResize(EventArgs.Empty);
				    break;
				case AllegroEventType.ALLEGRO_EVENT_DISPLAY_CLOSE:
					OnClose(EventArgs.Empty);
					break;
				//case AllegroEventType.ALLEGRO_EVENT_DISPLAY_LOST:
				//    break;
				//case AllegroEventType.ALLEGRO_EVENT_DISPLAY_FOUND:
				//    break;
				//case AllegroEventType.ALLEGRO_EVENT_DISPLAY_SWITCH_IN:
				//    break;
				//case AllegroEventType.ALLEGRO_EVENT_DISPLAY_SWITCH_OUT:
				//    break;
				//case AllegroEventType.ALLEGRO_EVENT_DISPLAY_ORIENTATION:
				//    break;
				//default:
				//    break;
			}

			if (AllegroMethods.al_is_event_queue_empty(queue) != 0 && wasUpdated)
			{
				Draw();
				wasUpdated = false;
			}
		}

		/// <summary>
		/// Prepares the application.
		/// </summary>
		/// <remarks>
		/// Used when a custom application event loop is necessary.
		/// </remarks>
		public void Prepare()
		{
			if (!IsInitialized)
				Initialize();

			LoadContent();

			// Everything is a-go.
			isRunning = true;

			AllegroMethods.al_flush_event_queue(queue);
			timer.Start();
		}

		/// <summary>
		/// Called when the application event loop ceases.
		/// </summary>
		/// <remarks>
		/// Used when a custom application event loop is necessary.
		/// </remarks>
		public void Finish()
		{
			timer.Stop();
		}

		/// <summary>
		/// Starts the event loop.
		/// </summary>
		public void Run()
		{
			Prepare();

			while (isRunning)
			{
				ProcessEvent();
			}

			Finish();
		}

		float startDropTime;

		/// <summary>
		/// Called when events need dropped.
		/// </summary>
		public void FinishLoading()
		{
			startDropTime = ElapsedTime;
		}

		/// <summary>
		/// Initializes the application.
		/// </summary>
		public void Initialize()
		{
			if (IsInitialized)
				throw new InitializationException("Application was already initialized.");

			// Initialize the core dependencies.
			platformContext.Initialize();

			// Initialize input devices.
			keyboard.Initialize();
			mouse.Initialize();
			joystick.Initialize();

			// Inform the base application.
			OnRun();

			// Initialize some other stuff.
			display.Initialize();
			renderer.Initialize();
			timer.Initialize();

			// Create the event queue and register the event sources.
			queue = AllegroMethods.al_create_event_queue();

			if (queue == IntPtr.Zero)
				throw new InitializationException("Could not create event queue.");

			AllegroMethods.al_register_event_source(queue, timer.EventSource);
			AllegroMethods.al_register_event_source(queue, display.EventSource);
			AllegroMethods.al_register_event_source(queue, keyboard.EventSource);
			AllegroMethods.al_register_event_source(queue, mouse.EventSource);
			AllegroMethods.al_register_event_source(queue, joystick.EventSource);

			IsInitialized = true;

			OnInitialize();
		}

		/// <summary>
		/// Called before the application display and renderer are created.
		/// </summary>
		/// <remarks>
		/// At this point, one can set the display settings.
		/// </remarks>
		protected virtual void OnRun()
		{
			// Nothing.
		}

		/// <summary>
		/// Called when the application has been initialized.
		/// </summary>
		protected virtual void OnInitialize()
		{
			// Nothing.
		}

		/// <summary>
		/// Loads the content.
		/// </summary>
		/// <remarks>At this point, the renderer can be used.</remarks>
		public virtual void LoadContent()
		{
			// Nothing.
		}

		/// <summary>
		/// Unloads the content.
		/// </summary>
		public virtual void UnloadContent()
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when the mouse moves.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnMouseMove(MouseEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a mouse button is pressed.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnMouseButtonDown(MouseEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a mouse button is released.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnMouseButtonUp(MouseEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a key is input or repeated.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		/// <remarks>This is useful for text input, but not general purpose keyboard input.</remarks>
		public virtual void OnKeyInput(KeyboardEventArgs e)
		{
			// Nothing.
		}
		
		/// <summary>
		/// Occurs when a key is pressed.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnKeyDown(KeyboardEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a key is released.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnKeyUp(KeyboardEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a stick is moved.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnJoystickAxisChange(JoystickEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a joystick button is pressed.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnJoystickButtonDown(JoystickEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a joystick button is released.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnJoystickButtonUp(JoystickEventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when a joystick is hotplugged.
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnJoystickReconfiguration(EventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Occurs when the display is closed.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		public virtual void OnClose(EventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Immediately exits the game.
		/// </summary>
		public void Exit()
		{
			isRunning = false;
		}

		/// <summary>
		/// Updates the game.
		/// </summary>
		public virtual void Update()
		{
			// Nothing.
		}

		/// <summary>
		/// Draws the game.
		/// </summary>
		public virtual void Draw()
		{
			Display.Flip();
		}

		/// <summary>
		/// Occurs when the application is resized.
		/// </summary>
		public virtual void OnResize(EventArgs e)
		{
			// Nothing.
		}

		/// <summary>
		/// Disposes of all resources allocated by the application.
		/// </summary>
		public void Dispose()
		{
			UnloadContent();

			// Dispose of all resources.
			if (keyboard.IsInitialized)
				keyboard.Dispose();

			if (mouse.IsInitialized)
				mouse.Dispose();

			if (joystick.IsInitialized)
				joystick.Dispose();

			if (Display.IsInitialized)
				Display.Dispose();

			if (timer.IsInitialized)
				timer.Dispose();

			if (queue != IntPtr.Zero)
				AllegroMethods.al_destroy_event_queue(queue);

			if (platformContext.IsInitialized)
				platformContext.Dispose();
		}
	}
}
