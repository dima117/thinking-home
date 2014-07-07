using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using NHibernate;
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
	[AppSection("Tiles", SectionType.System, "/webapp/webui/tiles-editor.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor.js")]
	[JavaScriptResource("/webapp/webui/tiles-editor-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-view.js")]
	[JavaScriptResource("/webapp/webui/tiles-editor-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-model.js")]
	[HttpResource("/webapp/webui/tiles-editor-layout.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-layout.tpl")]
	[HttpResource("/webapp/webui/tiles-editor-form.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-form.tpl")]
	[HttpResource("/webapp/webui/tiles-editor-list-item.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tiles-editor-list-item.tpl")]

	[JavaScriptResource("/webapp/webui/tile-params.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tile-params.js")]
	[JavaScriptResource("/webapp/webui/tile-params-view.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tile-params-view.js")]
	[JavaScriptResource("/webapp/webui/tile-params-model.js", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tile-params-model.js")]
	[HttpResource("/webapp/webui/tile-params.tpl", "ThinkingHome.Plugins.WebUI.Resources.Plugin.tile-params.tpl")]

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

		public override void Init()
		{
			availableTiles = RegisterTiles(TileDefinitions, Logger);
		}

		private static InternalDictionary<TileDefinition> RegisterTiles(TileDefinition[] definitions, Logger logger)
		{
			var tiles = new InternalDictionary<TileDefinition>();

			// регистрируем обработчики для методов плагинов
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
				return GetListModel(session, availableTiles, (id, info, model) => info.FillModel(model));
			}
		}

		#endregion

		#region tiles editor

		[HttpCommand("/api/webui/tiles/editor-form")]
		public object LoadTilesEditorForm(HttpRequestParams request)
		{
			var available = availableTiles
				.Select(el => new { id = el.Key, name = el.Value.Title })
				.ToArray();

			var selectedKey = available.Any() ? available.First().id : null;

			return new { available, selectedKey };
		}

		[HttpCommand("/api/webui/tiles/editor-list")]
		public object LoadTilesEditorList(HttpRequestParams request)
		{
			using (var session = Context.OpenSession())
			{
				var list = GetListModel(session, availableTiles);
				return list;
			}
		}

		[HttpCommand("/api/tiles/editor-add")]
		public object AddTile(HttpRequestParams request)
		{
			var key = request.GetRequiredString("key");

			using (var session = Context.OpenSession())
			{
				var guid = Guid.NewGuid();

				var tile = new Tile { Id = guid, HandlerKey = key };
				session.Save(tile);
				session.Flush();

				return guid;
			}
		}

		[HttpCommand("/api/tiles/editor-delete")]
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

		#endregion

		#region tile params

		[HttpCommand("/api/webui/tiles/params")]
		public object GetTileParams(HttpRequestParams request)
		{
			Guid id = request.GetRequiredGuid("id");

			using (var session = Context.OpenSession())
			{
				var tile = session.Get<Tile>(id);

				TileDefinition def;

				if (availableTiles.TryGetValue(tile.HandlerKey, out def))
				{
					return def
						.GetParameters(id)
						.Select(CreateTileParameterModel)
						.ToArray();
				}
			}

			return null;
		}

		private object CreateTileParameterModel(TileParameter p)
		{
			return new
			{
				name = p.Name,
				value = p.Value,
				label = p.Label,
				list = GetList(p.List)
			};
		}

		// todo: переименовать
		private object GetList(TileParameterValue[] list)
		{
			if (list == null)
			{
				return null;
			}

			return list
				.Select(obj => new { id = obj.Value, text = obj.Label })
				.ToArray();
		}

		#endregion

		#region helpers

		private static TileModel[] GetListModel(ISession session, InternalDictionary<TileDefinition> available, Action<Guid, TileDefinition, TileModel> func = null)
		{
			var result = new List<TileModel>();

			var tiles = session.Query<Tile>().ToList();

			foreach (var obj in tiles)
			{
				TileDefinition tile;

				if (available.TryGetValue(obj.HandlerKey, out tile))
				{
					var model = new TileModel { id = obj.Id, title = tile.Title, wide = tile.IsWide, url = tile.Url };

					if (func != null)
					{
						func(obj.Id, tile, model);
					}

					result.Add(model);
				}
			}

			return result.ToArray();
		}

		#endregion
	}
}
