using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Core.Plugins;
using NLog;
using ThinkingHome.Plugins.WebUI.Model;

namespace ThinkingHome.Plugins.WebUI.Tiles
{
	public abstract class TileDefinition
	{
		#region fields

		[Import(typeof(IServiceContext))]
		private IServiceContext context;

		private readonly Logger logger;

		#endregion

		protected TileDefinition()
		{
			logger = LogManager.GetLogger(typeof(WebUiTilesPlugin).FullName);
		}

		protected Logger Logger
		{
			get { return logger; }
		}

		protected IServiceContext Context
		{
			get { return context; }
		}

		public abstract void FillModel(TileModel model);
	}
}
