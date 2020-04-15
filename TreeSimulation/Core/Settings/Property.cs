using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace TreeSimulation.Core.Settings
{
    public class Property : DependencyObject
    {
        private readonly PropertyInfo _info;

        public Property(object origin, PropertyInfo info)
        {
            Origin = origin;
            _info = info;
        }

        public Type   Type  
        {
            get => _info.PropertyType;
        }
        public double Step  
        {
            get => _info.GetCustomAttribute<RangeAttribute>()?.Step ?? 1;
        }
        public string Name  
        {
            get => _info.Name;
        }
        public bool Advanced
        {
            get => _info.GetCustomAttribute<AdvancedAttribute>() != null;
        }
        public Range  Range 
        {
            get
            {
                var attr = _info.GetCustomAttribute<RangeAttribute>();
                return attr == null ? Range.Default : new Range(attr.Lower, attr.Upper);
            }
        }
        public object Value 
        {
            get => _info.GetValue(Origin);
            set => _info.SetValue(Origin, value);
        }
        public object Origin
        { 
            get; 
            set; 
        }
    }
}