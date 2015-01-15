using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.OneWire
{
    public class OneWireAdapterConfiguration
    {
        public string AdapterName { get; set; }

        public string PortName { get; set; }

        public static OneWireAdapterConfiguration Default
        {
            get
            {
                return new OneWireAdapterConfiguration
                {
                    AdapterName = "{DS9490}",
                    PortName = "USB1"
                };
            }
        }
    }
}
