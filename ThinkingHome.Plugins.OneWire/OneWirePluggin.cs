using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.OneWire
{
    [Plugin]
    public class OneWirePluggin : PluginBase 
    {        
        public override void InitPlugin()
        {
            Debugger.Launch();
            base.InitPlugin();
        }

        public override void StartPlugin()
        {
            Logger.Info("Start Plugin OneWire");
            base.StartPlugin();
        }

        public override void StopPlugin()
        {
            base.StopPlugin();            
        }
    }
}
