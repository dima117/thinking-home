using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Listener;
using ThinkingHome.Plugins.Listener.Api;
using ThinkingHome.Plugins.WebUI.Attributes;
using ThinkingHome.Plugins.WebUI.Model;

namespace ThinkingHome.Plugins.WebUI
{
	[Plugin]
	public class WebUiTilesPlugin : Plugin
	{
		private Dictionary<string, TileInfo> tiles;

		[ImportMany("FA4F97A0-41A0-4A72-BEF3-6DB579D909F4")]
		private Lazy<Action<TileModel>, ITileAttribute>[] TileHandlers { get; set; }

		public override void Init()
		{
			tiles = RegisterTiles(TileHandlers, Logger);
		}

		private static Dictionary<string, TileInfo> RegisterTiles(Lazy<Action<TileModel>, ITileAttribute>[] handlers, Logger logger)
		{
			var tiles = new Dictionary<string, TileInfo>(StringComparer.InvariantCultureIgnoreCase);

			// регистрируем обработчики для методов плагинов
			foreach (var handler in handlers)
			{
				logger.Info("Register TILE: '{0}'", handler.Metadata.Key);

				if (tiles.ContainsKey(handler.Metadata.Key))
				{
					throw new Exception("duplicated tile key");
				}

				var tileInfo = new TileInfo(handler.Metadata, handler.Value);

				tiles.Add(handler.Metadata.Key, tileInfo);
			}

			return tiles;
		}

		#region tiles

		[HttpCommand("/api/webui/tiles/all")]
		public object GetTiles(HttpRequestParams request)
		{
			var result = new List<TileModel>();

			var objects = new[]
			{
				new { id = Guid.NewGuid(), key = "48AFCCC4-A3B1-41B3-B23A-2EA3DAFD6F55" },
				new { id = Guid.NewGuid(), key = "48AFCCC4-A3B1-41B3-B23A-2EA3DAFD6F55" },
				new { id = Guid.NewGuid(), key = "48AFCCC4-A3B1-41B3-B23A-2EA3DAFD6F55" }
			};

			foreach (var obj in objects)
			{
				TileInfo tile;

				if (tiles.TryGetValue(obj.key, out tile))
				{
					var model = new TileModel { id = obj.id, title = tile.Title, wide = tile.IsWide };
					tile.Handler(model);
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
