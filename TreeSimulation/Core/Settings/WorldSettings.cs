using Newtonsoft.Json.Linq;
using Windows.Storage;
using Windows.UI.Xaml;

namespace TreeSimulation.Core.Settings
{
    public class WorldSettings : DependencyObject
    {
        public Range Light { get; set; }
        public double CellsTransparency { get; set; }
        public double InitialEnergy { get; set; }
        public double EnergyCosts { get; set; }
        public double BudMass { get; set; }
        public Range Lifetime { get; set; }
        public double BudProfit { get; set; }
    }
}