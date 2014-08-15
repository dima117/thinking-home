using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
	// webapp: tiles
	[JavaScriptResource("/application/tiles/tiles.js", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles.js")]
	[JavaScriptResource("/application/tiles/tiles-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles-view.js")]
	[JavaScriptResource("/application/tiles/tiles-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles-model.js")]
	[HttpResource("/application/tiles/tile.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tile.tpl")]
	[HttpResource("/application/tiles/tiles.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles.tpl")]

	// webapp: tiles-edit-mode
	[JavaScriptResource("/application/tiles/tiles-edit-mode.js", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles-edit-mode.js")]
	[JavaScriptResource("/application/tiles/tiles-edit-mode-view.js", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles-edit-mode-view.js")]
	[JavaScriptResource("/application/tiles/tiles-edit-mode-model.js", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles-edit-mode-model.js")]
	[HttpResource("/application/tiles/tile-edit-mode.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tile-edit-mode.tpl")]
	[HttpResource("/application/tiles/tiles-edit-mode.tpl", "ThinkingHome.Plugins.WebUI.Resources.Application.tiles.tiles-edit-mode.tpl")]
	
	[Plugin]
	public class WebUiTilesPlugin : PluginBase
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

		#region api

		[HttpCommand("/api/webui/tiles")]
		public object GetTiles(HttpRequestParams request)
		{

			using (var session = Context.OpenSession())
			{
				var result = new List<TileModel>();

				var tiles = session.Query<Tile>().OrderBy(t => t.SortOrder).ToList();

				foreach (var obj in tiles)
				{
					TileDefinition def;

					if (availableTiles.TryGetValue(obj.HandlerKey, out def))
					{
						var model = new TileModel(obj.Id, def);

						try
						{
							var options = obj.GetParameters();
							def.FillModel(model, options);
						}
						catch (Exception ex)
						{
							model.content = ex.Message;
						}

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

		[HttpCommand("/api/webui/tiles/add")]
		public object AddTile(HttpRequestParams request)
		{
			var strDef = request.GetRequiredString("def");
			var strOptions = request.GetString("options");

			TileDefinition def;

			if (!availableTiles.TryGetValue(strDef, out def))
			{
				throw new Exception(string.Format("invalid tile definition: {0}", strDef));
			}
			
			using (var session = Context.OpenSession())
			{
				var lastTile = session.Query<Tile>()
					.OrderByDescending(t => t.SortOrder)
					.FirstOrDefault();

				int sortOrder = lastTile == null ? 0 : lastTile.SortOrder + 1;


				var tile = new Tile
						   {
							   Id = Guid.NewGuid(),
							   HandlerKey = strDef,
							   SortOrder = sortOrder,
							   SerializedParameters = strOptions
						   };

				session.Save(tile);
				session.Flush();
			}

			return null;
		}

		[HttpCommand("/api/webui/tiles/sort")]
		public object UpdateSortOrder(HttpRequestParams request)
		{
			var json = request.GetRequiredString("data");
			var ids = Extensions.FromJson<Guid[]>(json);

			using (var session = Context.OpenSession())
			{
				var tiles = session.Query<Tile>().ToList();

				for (int i = 0; i < ids.Length; i++)
				{
					var tile = tiles.FirstOrDefault(t => t.Id == ids[i]);

					if (tile != null)
					{
						tile.SortOrder = i;
					} 
				}

				session.Flush();
			}

			return null;
		}

		#endregion
	}
}
