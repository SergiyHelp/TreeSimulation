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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TreeSimulation
{
    public sealed partial class NamedField : UserControl
    {
        public NamedField()
        {
            this.InitializeComponent();
        }


        public static DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(NamedField), new PropertyMetadata(100));
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static DependencyProperty MinValueProperty =
           DependencyProperty.Register("MinValue", typeof(double), typeof(NamedField), new PropertyMetadata(0));
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }


        public static DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(double), typeof(NamedField), new PropertyMetadata(0));
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NamedField), new PropertyMetadata("Field"));
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }


    }
}
