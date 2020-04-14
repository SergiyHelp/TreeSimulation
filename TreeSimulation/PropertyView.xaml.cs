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
                var slider = new Slider();



                _tool.Children.Add(slider);
            }
            else if(property.Type == typeof(Range))
            {

            }
            else
            {
                throw new NotImplementedException();
            }
        }


    }
}
