using Newtonsoft.Json;
using Windows.Storage;
using Windows.UI.Xaml;

namespace TreeSimulation.Core.Settings
{
    public class WorldSettings
    {
        [Range(0, 500)] public double Width { get; set; }
        [Range(0, 200)] public double Height { get; set; }
        [Range(0, 1, 70)] public double Population { get; set; }
        [Range(0, 0.02, 1)] public Range Landscape { get; set; }
        [JsonIgnore] public string Seed { get; set; } 
        [Advanced] public bool IsClosed { get; set; }
        [Advanced] [Range(0, 5,  1000)] public Range  Light             { get; set; }
        [Advanced] [Range(0, 0.1,   1)] public double CellsTransparency { get; set; }
        [Advanced] [Range(1, 10,  500)] public double InitialEnergy     { get; set; }
        [Advanced] [Range(0, 0.2,  50)] public double EnergyCosts       { get; set; }
        [Advanced] [Range(0, 0.5, 100)] public double BudMass           { get; set; }
        [Advanced] [Range(0, 5,   300)] public Range  Lifetime          { get; set; }
        [Advanced] [Range(0, 0.1,   1)] public double BudProfit         { get; set; }
    }
}