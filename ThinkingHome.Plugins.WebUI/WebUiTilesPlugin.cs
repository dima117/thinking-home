using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NLog;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.WebUI.Attributes;
using ThinkingHome.Plugins.WebUI.Data;
using ThinkingHome.Plugins.WebUI.Model;
using ThinkingHome.Plugins.WebUI.Tiles;

namespace ThinkingHome.Plugins.WebUI
{
	[JavaScriptResource("/webapp/webui/tiles.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles.js")]
	[JavaScriptResource("/webapp/webui/tiles-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-model.js")]
	[JavaScriptResource("/webapp/webui/tiles-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-view.js")]
	[HttpResource("/webapp/webui/tile.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tile.tpl")]


	[Plugin]
	public class WebUiTilesPlugin : Plugin
	{
		private InternalDictionary<TileDefinition> availableTiles;

		[ImportMany("FA4F97A0-41A0-4A72-BEF3-6DB579D909F4")]
		public TileDefinition[] TileDefinitions { get; set; }

		public override void InitPlugin()
		{
			availableTiles = RegisterTiles(TileDefinitions, Logger);
		}

		private static InternalDictionary<TileDefinition> RegisterTiles(TileDefinition[] definitions, Logger logger)
		{
			var tiles = new InternalDictionary<TileDefinition>();

			// регистрируем типы плитки
			foreach (var definition in definitions)
			{
				var key = definition.GetType().FullName;

				logger.Info("Register TILE DEFINITION: '{0}'", key);
				tiles.Register(key, definition);
			}

			return tiles;
		}

		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<Tile>(cfg => cfg.Table("WebUI_Tile"));
		}


		#region tiles

		[HttpCommand("/api/webui/tiles/all")]
		public object GetTiles(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var result = new List<TileModel>();

				var tiles = session.Query<Tile>().ToList();

				foreach (var obj in tiles)
				{
					TileDefinition def;

					if (availableTiles.TryGetValue(obj.HandlerKey, out def))
					{
						var model = new TileModel(obj.Id, def);

						def.FillModel(model);

						result.Add(model);
					}
				}

				return result.ToArray();
			}
		}

		#endregion
	}
}
