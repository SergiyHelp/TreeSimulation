using System;
using TreeSimulation.Core;
using TreeSimulation.Core.Settings;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TreeSimulation
{
    public sealed partial class MainPage : Page
    {
        private readonly ApplicationDataContainer _data;

        public MainPage()
        {
            InitializeComponent();
            _data = ApplicationData.Current.LocalSettings;

            _widthSlider.Value = (double)(_data.Values["width"] ?? _widthSlider.Value);
            _heightSlider.Value = (double)(_data.Values["height"] ?? _heightSlider.Value);
            _populationSlider.Value = (double)(_data.Values["population"] ?? _populationSlider.Value);

            
        }

        private void CreateWorld(object sender, RoutedEventArgs e)
        {
            _data.Values["width"] = _widthSlider.Value;
            _data.Values["height"] = _heightSlider.Value;
            _data.Values["population"] = _populationSlider.Value;


            int seed = Int32.TryParse(_seedField.Text, out int val) ? val : (int)DateTime.Now.Ticks;
            int width = (int)_widthSlider.Value;
            int height = (int)_heightSlider.Value;
            int population = (int)_populationSlider.Value;

            Landscape landscape = new Landscape((int)_widthSlider.Value, seed, (int)_landscapeRange.RangeMin, (int)_landscapeRange.RangeMax);

            
            World world = new World(seed, width, height, population, landscape, null);
            Frame.Navigate(typeof(SimulationPage), world);
        }
    }
}