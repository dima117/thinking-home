using System;
using System.IO;
using System.Linq;
using WixSharp;
using File = WixSharp.File;

namespace ThinkingHome.Setup
{
	class Program
	{
		static void Main(string[] args)
		{
			//var content = BuildContent(args[0]);
			var content = BuildContent(@"C:\Users\dima117\Documents\Visual Studio 2015\Projects\thinking-home\Setup\dist\service");

			var project = new Project("Thinking Home", content)
			{
				Platform = Platform.x64,
				GUID = new Guid("25F88AAD-CF60-4CA4-ADD3-F01A4F0F0313")
			};


			Compiler.BuildMsi(project);
		}

		static Dir BuildContent(string path)
		{
			var allFiles = Directory
				.GetFiles(path)
				.Select(file =>
				{
					Console.WriteLine(file);
					return new File(file);
				})
				.Cast<WixEntity>()
				.ToArray();

			return new Dir(@"%ProgramFiles%\ThinkingHome\My Product", allFiles);
		}
	}
}
