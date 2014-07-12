using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
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

		[HttpCommand("/api/webui/tiles")]
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
						var options = obj.GetParameters();

						def.FillModel(model, options);

						result.Add(model);
					}
				}

				return result.ToArray();
			}
		}

		[HttpCommand("/api/webui/tiles/delete")]
		public object DeleteTile(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var tile = session.Load<Tile>(id);
				session.Delete(tile);
				session.Flush();
			}

			return null;
		}

		[HttpCommand("/api/webui/tiles/action")]
		public object RunTileAction(HttpRequestParams request)
		{
			var id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var tile = session.Get<Tile>(id);
				TileDefinition def;

				if (availableTiles.TryGetValue(tile.HandlerKey, out def))
				{
					var options = tile.GetParameters();
					return def.ExecuteAction(options);
				}
			}

			return null;
		}

		#endregion
	}
}
