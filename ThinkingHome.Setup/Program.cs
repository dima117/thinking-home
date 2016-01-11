using System;
using System.IO;
using WixSharp;

namespace ThinkingHome.Setup
{
	class Program
	{
		static void Main(string[] args)
		{
			var project = new Project("Thinking Home", new Dir(@"%ProgramFiles%\ThinkingHome", new Files("*.*")))
			{
				Platform = Platform.x64,
				GUID = new Guid("25F88AAD-CF60-4CA4-ADD3-F01A4F0F0313"),
				LicenceFile = "License.rtf",
				SourceBaseDir = @"C:\Users\dima117\Documents\Visual Studio 2015\Projects\thinking-home\Setup\dist\service"
			};

			Compiler.BuildMsi(project);
		}
	}
}
