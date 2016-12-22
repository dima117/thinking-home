using System.Linq;
using NHibernate;
using NLog;
using ThinkingHome.Plugins.UniUI.Model;
using ThinkingHome.Plugins.UniUI.Widgets;
using NHibernate.Linq;
using ThinkingHome.Plugins.Scripts.Data;
using ThinkingHome.Plugins.Scripts.Lang;

namespace ThinkingHome.Plugins.Scripts
{
    [Widget("scripts-exec")]
    public class ScriptExecWidgetDefinition : IWidgetDefinition
    {
        public string DisplayName
        {
            get { return ScriptsUiLang.Execute_script; }
        }

        public object GetWidgetData(Widget widget, WidgetParameter[] parameters, ISession session, Logger logger)
        {
            var id = parameters.Single(p => p.Name == "script-id").ValueGuid;
            var script = session.Get<UserScript>(id); ;

            return new { scriptId = script.Id, scriptName = script.Name };
        }

        public WidgetParameterMetaData[] GetWidgetMetaData(ISession session, Logger logger)
        {
            var scripts = session
                .Query<UserScript>()
                .Select(s => new WidgetSelectItem(s.Id, s.Name))
                .ToArray();

            var paramScriptId = new WidgetParameterMetaData
            {
                Name = "script-id",
                DisplayName = ScriptsUiLang.Script,
                Type = WidgetParameterType.Guid,
                Items = scripts
            };
            
            return new[] { paramScriptId };
        }
    }
}
