using System;
using TreeSimulation.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TreeSimulation
{
    public sealed partial class MainPage : Page
    {
        private readonly ApplicationDataContainer _settings;

        public MainPage()
        {
            InitializeComponent();
            _settings = ApplicationData.Current.LocalSettings;

            _heightSlider.Value = (double)(_settings.Values["height"] ?? _heightSlider.Value);
            _widthSlider.Value = (double)(_settings.Values["width"] ?? _widthSlider.Value);
            _populationSlider.Value = (double)(_settings.Values["population"] ?? _populationSlider.Value);

            _budMassField.Value = (double)(_settings.Values["budMass"] ?? _budMassField.Value);
            _budProfitField.Value = (double)(_settings.Values["budProfit"] ?? _budProfitField.Value);
            _cellsTransparentsyField.Value = (double)(_settings.Values["cellsTransparentsy"] ?? _cellsTransparentsyField.Value);
            _fruitMassField.Value = (double)(_settings.Values["fruitMass"] ?? _fruitMassField.Value);
            _initialEnergyField.Value = (double)(_settings.Values["initialEnergy"] ?? _initialEnergyField.Value);
            _leastEnergyField.Value = (double)(_settings.Values["leastEnergy"] ?? _leastEnergyField.Value);
            _largestEnergyField.Value = (double)(_settings.Values["largestEnergy"] ?? _largestEnergyField.Value);
            _minLifetimeField.Value = (double)(_settings.Values["minLifetime"] ?? _minLifetimeField.Value);
            _maxLifetimeField.Value = (double)(_settings.Values["maxLifetime"] ?? _maxLifetimeField.Value);
        }

        private void CreateWorld(object sender, RoutedEventArgs e)
        {
            int seed = Int32.TryParse(_seedField.Text, out int val) ? val : (int)DateTime.Now.Ticks;

            _settings.Values["height"] = _heightSlider.Value;
            _settings.Values["width"] = _widthSlider.Value;
            _settings.Values["population"] = _populationSlider.Value;

            _settings.Values["budMass"] = _budMassField.Value;
            _settings.Values["budProfit"] = _budProfitField.Value;
            _settings.Values["cellsTransparentsy"] = _cellsTransparentsyField.Value;
            _settings.Values["fruitMass"] = _fruitMassField.Value;
            _settings.Values["initialEnergy"] = _initialEnergyField.Value;
            _settings.Values["leastEnergy"] = _leastEnergyField.Value;
            _settings.Values["largestEnergy"] = _largestEnergyField.Value;
            _settings.Values["minLifetime"] = _minLifetimeField.Value;
            _settings.Values["maxLifetime"] = _maxLifetimeField.Value;

            WorldSettings settings = new WorldSettings(_largestEnergyField.Value,
                                                       _leastEnergyField.Value,
                                                       _cellsTransparentsyField.Value / 100d,
                                                       _initialEnergyField.Value,
                                                       _energyConsumptionField.Value,
                                                       _budMassField.Value,
                                                       _budProfitField.Value / 100d,
                                                       _fruitMassField.Value,
                                                  (int)_minLifetimeField.Value,
                                                  (int)_maxLifetimeField.Value);

            World world = new World(seed, (int)_widthSlider.Value, (int)_heightSlider.Value, (int)_populationSlider.Value, settings);
            Frame.Navigate(typeof(SimulationPage), world);
        }
    }
}