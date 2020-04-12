using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace TreeSimulation.Core.Settings
{
    public class SettingsModel : DependencyObject
    {
        public SettingsModel()
        {
        }

        public static SettingsModel Load(string json)
        {
            return json == null ? new SettingsModel() : JObject.Parse(json).ToObject<SettingsModel>();
        }
        public static string Save(SettingsModel settings)
        {
            return JObject.FromObject(settings).ToString();
        }
        public WorldSettings GetData()
        {
            return new WorldSettings()
            {
                CellsTransparency = CellsTransparency,
                MaxEnergy = MaxEnergy,
                MinEnergy = MinEnergy,
                MaxEnergyPass = MaxEnergyPass,
                BudMass = BudMass,
                FruitMass = FruitMass,
                BudProfit = BudProfit,
                EnergyCosts = EnergyCosts,
                InitialEnergy = InitialEnergy,
                MaxLife = MaxLife,
                MinLife = MinLife,
            };

        }

        public static DependencyProperty CellsTransparencyProperty =
            DependencyProperty.Register("CellsTransparency", typeof(double), typeof(SettingsModel), new PropertyMetadata(0));
        public double CellsTransparency
        {
            get => (double)GetValue(CellsTransparencyProperty);
            set => SetValue(CellsTransparencyProperty, value);
        }

        public static DependencyProperty MaxEnergyProp =
            DependencyProperty.Register("MaxEnergy", typeof(double), typeof(SettingsModel), new PropertyMetadata(800));
        public double MaxEnergy
        {
            get => (double)GetValue(MaxEnergyProp);
            set => SetValue(MaxEnergyProp, value);
        }

        public static DependencyProperty MinEnergyProperty =
            DependencyProperty.Register("MinEnergy", typeof(double), typeof(SettingsModel), new PropertyMetadata(10));
        public double MinEnergy
        {
            get => (double)GetValue(MinEnergyProperty);
            set => SetValue(MinEnergyProperty, value);
        }

        public static DependencyProperty MaxEnergyPassProperty =
            DependencyProperty.Register("MaxEnergyPass", typeof(double), typeof(SettingsModel), new PropertyMetadata(10));
        public double MaxEnergyPass
        {
            get => (double)GetValue(MaxEnergyPassProperty);
            set => SetValue(MaxEnergyPassProperty, value);
        }

        public static DependencyProperty InitialEnergyProperty =
            DependencyProperty.Register("InitialEnergy", typeof(double), typeof(SettingsModel), new PropertyMetadata(200));
        public double InitialEnergy
        {
            get => (double)GetValue(InitialEnergyProperty);
            set => SetValue(InitialEnergyProperty, value);
        }

        public static DependencyProperty EnergyCostsProperty =
            DependencyProperty.Register("EnergyCosts", typeof(double), typeof(SettingsModel), new PropertyMetadata(9));
        public double EnergyCosts
        {
            get => (double)GetValue(EnergyCostsProperty);
            set => SetValue(EnergyCostsProperty, value);
        }

        public static DependencyProperty BudProfitProperty =
            DependencyProperty.Register("BudProfit", typeof(double), typeof(SettingsModel), new PropertyMetadata(0.5));
        public double BudProfit
        {
            get => (double)GetValue(BudProfitProperty);
            set => SetValue(BudProfitProperty, value);
        }

        public static DependencyProperty BudMassProperty =
            DependencyProperty.Register("BudMass", typeof(double), typeof(SettingsModel), new PropertyMetadata(40));
        public double BudMass
        {
            get => (double)GetValue(BudMassProperty);
            set => SetValue(BudMassProperty, value);
        }

        public static DependencyProperty FruitMassProperty =
            DependencyProperty.Register("FruitMass", typeof(double), typeof(SettingsModel), new PropertyMetadata(10));
        public double FruitMass
        {
            get => (double)GetValue(FruitMassProperty);
            set => SetValue(FruitMassProperty, value);
        }

        public static DependencyProperty MinLifeProperty =
            DependencyProperty.Register("MinLife", typeof(int), typeof(SettingsModel), new PropertyMetadata(80));
        public int MinLife
        {
            get => (int)GetValue(MinLifeProperty);
            set => SetValue(MinLifeProperty, value);
        }

        public static DependencyProperty MaxLifeProperty =
            DependencyProperty.Register("MaxLife", typeof(int), typeof(SettingsModel), new PropertyMetadata(120));
        public int MaxLife
        {
            get => (int)GetValue(MaxLifeProperty);
            set => SetValue(MaxLifeProperty, value);
        }
    }
}
