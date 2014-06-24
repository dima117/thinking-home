using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NLog;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.Listener.Attributes;
using ThinkingHome.Plugins.WebUI.Attributes;
using ThinkingHome.Plugins.WebUI.Data;
using ThinkingHome.Plugins.WebUI.Model;

namespace ThinkingHome.Plugins.WebUI
{
	[Plugin]
	public class WebUiTilesPlugin : Plugin
	{
		private InternalDictionary<TileInfo> availableTiles;

		[ImportMany("FA4F97A0-41A0-4A72-BEF3-6DB579D909F4")]
		public Lazy<Action<TileModel>, ITileAttribute>[] TileHandlers { get; set; }

		public override void Init()
		{
			availableTiles = RegisterTiles(TileHandlers, Logger);
		}

		private static InternalDictionary<TileInfo> RegisterTiles(Lazy<Action<TileModel>, ITileAttribute>[] handlers, Logger logger)
		{
			var tiles = new InternalDictionary<TileInfo>();

			// регистрируем обработчики для методов плагинов
			foreach (var handler in handlers)
			{
				logger.Info("Register TILE: '{0}'", handler.Metadata.Key);

				var tileInfo = new TileInfo(handler.Metadata, handler.Value);
				tiles.Register(handler.Metadata.Key, tileInfo);
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
				return GetListModel(session, availableTiles, (id, info, model) => info.Handler(model));
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

		#region helpers

		private static TileModel[] GetListModel(ISession session, InternalDictionary<TileInfo> available, Action<Guid, TileInfo, TileModel> func = null)
		{
			var result = new List<TileModel>();

			var tiles = session.Query<Tile>().ToList();

			foreach (var obj in tiles)
			{
				TileInfo tile;

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

	internal class TileInfo
	{
		public TileInfo(ITileAttribute metadata, Action<TileModel> handler)
		{
			Title = metadata.Title;
			Url = metadata.Url;
			IsWide = metadata.IsWide;
			Handler = handler;
		}

		public readonly string Title;
		public readonly string Url;
		public readonly bool IsWide;
		public readonly Action<TileModel> Handler;
	}
}
