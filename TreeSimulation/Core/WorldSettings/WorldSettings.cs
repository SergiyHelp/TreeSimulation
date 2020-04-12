using Newtonsoft.Json.Linq;
using Windows.Storage;
using Windows.UI.Xaml;

namespace TreeSimulation.Core.Settings
{
    public class WorldSettings : DependencyObject
    {
        public double MaxEnergy { get; set; }
        public double MinEnergy { get; set; }
        public double CellsTransparency { get; set; }
        public double InitialEnergy { get; set; }
        public double EnergyCosts { get; set; }
        public double BudMass { get; set; }
        public double FruitMass { get; set; }
        public int MinLife { get; set; }
        public int MaxLife { get; set; }
        public double BudProfit { get; set; }
        public double MaxEnergyPass { get; set; }
    }
}