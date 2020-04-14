using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace TreeSimulation.Core.Settings
{
    public abstract class Property : DependencyObject
    {
        public Property(string name, Range range)
        {
            Name = name;
            Range = range;
        }

        public abstract object Value
        {
            get;
            set;
        }

        public string Name 
        { 
            get; 
            private set; 
        }
        public Range Range
        {
            get; 
            private set;
        }

        public void SaveTo(ApplicationDataCompositeValue data)
        {
            data[Name] = Value;
        }
        public void LoadFrom(ApplicationDataCompositeValue data)
        {
            Value = data[Name];
        }
    }

    public class DoubleProperty : Property
    {
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Property), new PropertyMetadata(0d));

        public DoubleProperty(string name, Range range) : base(name, range)
        {
        }

        public override object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }

    public class RangeProperty : Property
    {
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Range), typeof(Property), new PropertyMetadata(Range.Default));

        public RangeProperty(string name, Range range) : base(name, range)
        {
        }

        public override object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }

}
