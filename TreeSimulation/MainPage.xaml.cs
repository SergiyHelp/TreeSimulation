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

            _settings = SettingsModel.Load(_data.Values["settings"].ToString());

        }

        private void CreateWorld(object sender, RoutedEventArgs e)
        {
            _data.Values["settings"] = SettingsModel.Save(_settings);

            int seed = Int32.TryParse(_seedField.Text, out int val) ? val : (int)DateTime.Now.Ticks;

            Landscape landscape = new Landscape((int)_widthSlider.Value, seed, (int)_landscapeRange.RangeMin, (int)_landscapeRange.RangeMax);

            World world = new World(seed, (int)_widthSlider.Value, (int)_heightSlider.Value, (int)_populationSlider.Value, landscape, _settings.GetData());
            Frame.Navigate(typeof(SimulationPage), world);
        }
    }
}