using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Test
{
	public class Program
	{
		static void Main(string[] args)
		{
			string file = "content/default.lvg";
			if (args.Length > 0)
			{
				file = args[0];
			}

			using (TestApplication app = new TestApplication(file))
			{
				app.Run();
			}
		}
	}
}
