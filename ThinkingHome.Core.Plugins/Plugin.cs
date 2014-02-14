using System;
using System.ComponentModel.Composition;
using System.Linq;
using NHibernate.Mapping.ByCode;
using NLog;

namespace ThinkingHome.Core.Plugins
{
	public abstract class Plugin
	{
		#region fields

		[Import(typeof(IServiceContext))]
		private IServiceContext context;

		private readonly Logger logger;

		#endregion

		#region life cycle

		protected Plugin()
		{
			logger = LogManager.GetLogger(GetType().FullName);
		}

		public virtual void Init()
		{

		}

		public virtual void InitDbModel(ModelMapper mapper)
		{

		}

		public virtual void Start()
		{

		}

		public virtual void Stop()
		{

		}

		#endregion

		#region utils

		public Logger Logger
		{
			get { return logger; }
		}

		public IServiceContext Context
		{
			get { return context; }
		}

		public void Run<T>(T[] actions, Action<T> task)
		{
			if (actions != null && actions.Any())
			{
				foreach (var action in actions)
				{
					try
					{
						task(action);
					}
					catch (Exception ex)
					{
						Logger.ErrorException(ex.Message, ex);
					}
				}
			}
		}

		#endregion
	}
}
