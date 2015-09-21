using System;
using System.ComponentModel.Composition;
using ECM7.Migrator.Framework;
using NHibernate.Mapping.ByCode;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Core.Plugins.Utils;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;

[assembly: MigrationAssembly("ThinkingHome.Plugins.UniUI")]

namespace ThinkingHome.Plugins.UniUI
{
	[Plugin]
	public partial class UniUiPlugin : PluginBase
	{
		public override void InitDbModel(ModelMapper mapper)
		{
			mapper.Class<Dashboard>(cfg => cfg.Table("UniUI_Dashboard"));
			mapper.Class<Widget>(cfg => cfg.Table("UniUI_Widget"));
            mapper.Class<Panel>(cfg => cfg.Table("UniUI_Panel"));
            mapper.Class<WidgetParameter>(cfg => cfg.Table("UniUI_WidgetParameter"));
		}

		private readonly InternalDictionary<IWidgetDefinition> defs = new InternalDictionary<IWidgetDefinition>();

		[ImportMany("ABD9D425-5836-4DC5-88B6-222CD7A658CA")]
		public Lazy<IWidgetDefinition, IWidgetAttribute>[] WidgetDefinitions { get; set; }

		public override void InitPlugin()
		{
			foreach (var def in WidgetDefinitions)
			{
				Logger.Info("Register UI widget : '{0}'", def.Metadata.TypeAlias);
				defs.Register(def.Metadata.TypeAlias, def.Value);
			}
		}
	}
}
