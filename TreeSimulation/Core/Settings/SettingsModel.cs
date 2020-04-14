using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using System.Reflection;

namespace TreeSimulation.Core.Settings
{
    public class SettingsModel : DependencyObject
    {
        public SettingsModel()
        {
            Type type = typeof(WorldSettings);
            var props = type.GetProperties();
            Properties = new Property[props.Length];

            Properties = props.Select((x, i) =>
            {
                var rangeAttr = x.GetCustomAttribute<RangeAttribute>();

                Range range = rangeAttr != null ? new Range(rangeAttr.Lower, rangeAttr.Upper) : new Range(0, 100);

                if (x.PropertyType == typeof(Double))
                    return new DoubleProperty(x.Name, range);
                else if (x.PropertyType == typeof(Range))
                    return new DoubleProperty(x.Name, range);
                else
                    throw new Exception("Unsupported property type");
            }).ToArray();


            for (int i = 0; i < props.Length; i++)
            {
                
            }

        }

        public Property[] Properties
        {
            get;
            private set;
        }

        
    }
}
