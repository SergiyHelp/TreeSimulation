using System;
using System.Linq;
using Newtonsoft.Json.Linq;
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
        private readonly WorldSettings _settings;


        public MainPage()
        {
            InitializeComponent();
            _data = ApplicationData.Current.LocalSettings;
            
            _widthSlider.Value =       (double)(_data.Values["width"]      ?? _widthSlider.Value     );
            _heightSlider.Value =      (double)(_data.Values["height"]     ?? _heightSlider.Value    );
            _populationSlider.Value =  (double)(_data.Values["population"] ?? _populationSlider.Value);
            _landscapeRange.Maximum = _heightSlider.Value;
            _landscapeRange.RangeMin = (double)(_data.Values["landMin"]    ?? _landscapeRange.Minimum);
            _landscapeRange.RangeMax = (double)(_data.Values["landMax"]    ?? _landscapeRange.Maximum);
            _closedCheck.IsChecked =   (bool)  (_data.Values["closed"]     ?? _closedCheck.IsChecked);

            string settingsSerialized = _data.Values["settings"].ToString();
            _settings = JObject.Parse(settingsSerialized).ToObject<WorldSettings>();

            var properties = _settings
                .GetType()
                .GetProperties()
                .Select(x => new Property(_settings, x));

            var tools = properties
                .Where(x => ! x.Advanced)
                .Select(x => new PropertyView(x) { Width = 370, Height = 70 })
                .ToArray();

            var advancesTools = properties
                .Where(x => x.Advanced)
                .Select(x => new PropertyView(x) { Width = 370, Height = 70 })
                .ToArray();



            foreach (var item in advancesTools)            
                _advancedToolList.Children.Add(item);            

        }

        private void CreateWorld(object sender, RoutedEventArgs e)
        {
            _data.Values["width"]      = _widthSlider.Value;
            _data.Values["height"]     = _heightSlider.Value;
            _data.Values["population"] = _populationSlider.Value;
            _data.Values["landMin"]    = _landscapeRange.RangeMin;
            _data.Values["landMax"]    = _landscapeRange.RangeMax;
            _data.Values["settings"]   = JObject.FromObject(_settings).ToString();
            _data.Values["closed"]     = _closedCheck.IsChecked;


            int seed = Int32.TryParse(_seedField.Text, out int val) ? val : (int)DateTime.Now.Ticks;
            int width = (int)_widthSlider.Value;
            int height = (int)_heightSlider.Value;
            int population = (int)_populationSlider.Value;
            bool isClosed = (bool)_closedCheck.IsChecked;

            Landscape landscape = new Landscape((int)_widthSlider.Value, seed, new Range(_landscapeRange.RangeMin, _landscapeRange.RangeMax));
           
            World world = new World(seed, width, height, population, isClosed, landscape, _settings);
            Frame.Navigate(typeof(SimulationPage), world);
        }
    }
}