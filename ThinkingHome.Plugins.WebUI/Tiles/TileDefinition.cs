using System.ComponentModel.Composition;
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

		#region public

		public abstract void FillModel(TileModel model, dynamic options);

		public virtual string ExecuteAction(object options)
		{
			return null;
		}

		#endregion
	}
}
