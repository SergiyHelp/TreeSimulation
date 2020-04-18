using Newtonsoft.Json.Linq;
using System.Linq;
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

            string settingsSerialized = _data.Values["settings"].ToString();
            _settings = JObject.Parse(settingsSerialized).ToObject<WorldSettings>();

            var properties = _settings
                .GetType()
                .GetProperties()
                .Select(x => new Property(_settings, x));

            var tools = properties
                .Where(x => !x.Advanced)
                .Select(x => new PropertyView(x) { Width = 370, Height = 80 })
                .ToArray();

            var advancesTools = properties
                .Where(x => x.Advanced)
                .Select(x => new PropertyView(x) { Width = 370, Height = 70 })
                .ToArray();

            foreach (var item in tools)
                _toolList.Children.Insert(_toolList.Children.Count - 1, item);

            foreach (var item in advancesTools)
                _advancedToolList.Children.Add(item);
        }

        private void CreateWorld(object sender, RoutedEventArgs e)
        {
            _data.Values["settings"] = JObject.FromObject(_settings).ToString();

            World world = new World(_settings);
            Frame.Navigate(typeof(SimulationPage), world);
        }
    }
}