using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.Storage;
using Windows.UI.Xaml;

namespace TreeSimulation.Core.Settings
{
    public abstract class Property : DependencyObject
    {
        PropertyInfo _info;


        public Property(object origin, PropertyInfo info)
        {
            Origin = origin;
            _info = info;
        }

        public object Value
        {
            get => _info.GetValue(Origin);
            set => _info.SetValue(Origin, value);
        }

        public string Name 
        {
            get => _info.Name;
        }
        public Type Type
        {
            get => _info.PropertyType;
        }
        public Range Range
        {
            get
            {
                var attr = _info.GetCustomAttribute<RangeAttribute>();
                return attr == null ? Range.Default : new Range(attr.Lower, attr.Upper);
            }
        }
        public object Origin
        { get; set; }
    }
}
