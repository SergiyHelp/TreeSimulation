using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TreeSimulation.Core.Settings;
using TreeSimulation.Core;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace TreeSimulation
{
    public sealed partial class PropertyView : UserControl
    {        
        public PropertyView(Property property)
        {
            this.InitializeComponent();
            _title.Text = property.Name;

            if (property.Type == typeof(Double))
            {
                var slider = new Slider
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,

                    Value = (double)property.Value,
                    Minimum = property.Range.L,
                    Maximum = property.Range.U,
                    StepFrequency = property.Step,
                };
                slider.ValueChanged += (o, e) => property.Value = e.NewValue;

                _tool.Children.Add(slider);
            }
            else if(property.Type == typeof(Range))
            {
                var selector = new RangeSelector
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,

                    RangeMin = ((Range)property.Value).L,
                    RangeMax = ((Range)property.Value).U,
                    Minimum = property.Range.L,
                    Maximum = property.Range.U,
                    StepFrequency = property.Step,
                };
                selector.ValueChanged += (o, e) => property.Value = new Range(selector.RangeMin, selector.RangeMax);

                _tool.Children.Add(selector);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

    }
}
