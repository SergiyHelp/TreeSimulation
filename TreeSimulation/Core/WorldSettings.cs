namespace TreeSimulation.Core
{
    public class WorldSettings
    {
        public WorldSettings(double maxEnergy, double minEnergy, double cellsTransparentsy, double initialEnergy, double energyConsumption, double budMass, double budProfit, double fruitMass, int minLife, int maxLife)
        {
            MaxEnergy = maxEnergy;
            MinEnergy = minEnergy;
            CellsTransparentsy = cellsTransparentsy;
            InitialEnergy = initialEnergy;
            EnergyCosts = energyConsumption;
            BudMass = budMass;
            FruitMass = fruitMass;
            MinLife = minLife;
            MaxLife = maxLife;
            BudProfit = budProfit;
        }

        public double MaxEnergy { get; }
        public double MinEnergy { get; }
        public double CellsTransparentsy { get; }
        public double InitialEnergy { get; }
        public double EnergyCosts { get; }
        public double BudProfit { get; }
        public double BudMass { get; }
        public double FruitMass { get; }
        public int MinLife { get; }
        public int MaxLife { get; }
    }
}