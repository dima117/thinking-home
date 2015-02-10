using System;
using ThinkingHome.Plugins.Scripts.Data;
using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.Scripts
{
	[Tile]
	public class ScriptsTileDefinition : TileDefinition
	{
		public override void FillModel(TileModel model, dynamic options)
		{
			try
			{
				UserScript script = GetScript(options);

				model.title = script.Name;
				model.content = "Run the script\r\n" + script.Name;
				model.className = "btn-primary th-tile-icon th-tile-icon-fa fa-rocket";
			}
			catch (Exception ex)
			{
				model.content = ex.Message;
			}
		}

		public override string ExecuteAction(object options)
		{
			try
			{
				UserScript script = GetScript(options);

				Context.GetPlugin<ScriptsPlugin>().ExecuteScript(script);
				return null;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		private UserScript GetScript(dynamic options)
		{
			string strId = options.id;

			if (string.IsNullOrWhiteSpace(strId))
			{
				throw new Exception("Missing id parameter");
			}

			Guid scriptId;

			if (!Guid.TryParse(strId, out scriptId))
			{
				throw new Exception("Id parameter must contain GUID value");
			}

			using (var session = Context.OpenSession())
			{
				var script = session.Get<UserScript>(scriptId);

				if (script == null)
				{
					throw new Exception(string.Format("Script '{0}' is not found", scriptId));
				}

				return script;
			}
		}
	}
}
