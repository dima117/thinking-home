using System;
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

		public abstract string Title { get; }

		public abstract string Url { get; }

		public virtual bool HasParameters
		{
			get { return false; }
		}

		public virtual bool IsWide
		{
			get { return false; }
		}

		public abstract void FillModel(TileModel model);

		public virtual TileParameter[] GetParameters(Guid id)
		{
			return new TileParameter[0];
		}

		#endregion
	}
}
