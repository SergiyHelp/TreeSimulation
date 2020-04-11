using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Threading.Tasks;
using TreeSimulation.Core;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TreeSimulation
{
    public sealed partial class SimulationPage : Page
    {
        private World _world;
        private bool _active;
        private int _period;

        public SimulationPage()
        {
            this.InitializeComponent();
        }

        public void Step()
        {
            while (_active)
            {
                _world.MakeStep();
                _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => _dayNumber.Text = _world.Day.ToString());
                System.Threading.Thread.Sleep(_period);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _world = e.Parameter as World;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _active = false;
            base.OnNavigatedFrom(e);
        }

        private void ChangePlayMode(object sender, RoutedEventArgs e)
        {
            if (_play.IsOn)
            {
                _speedSlider.IsEnabled = false;
                _period = (int)(1000 / _speedSlider.Value);
                _active = true;
                Task.Run(() => Step());
            }
            else
            {
                _speedSlider.IsEnabled = true;
                _active = false;
            }
        }

        private void CreateNewWorld(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void DrawCanvas(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            float cellSize = (float)Math.Min(sender.Size.Width / _world.Width, sender.Size.Height / _world.Height);
            float rectangleHeight = cellSize * _world.Height;

            foreach (var item in _world.View)
            {
                float x = item.X * cellSize;
                float y = rectangleHeight - (item.Y + 2) * cellSize;

                args.DrawingSession.FillRectangle(x, y, cellSize, cellSize, item.Color);
            }
            args.DrawingSession.FillRectangle(0, rectangleHeight - cellSize, (float)sender.Size.Width, cellSize, Colors.Gray);
        }

        private void MakeStep(object sender, RoutedEventArgs e)
        {
            _world.MakeStep();
            _dayNumber.Text = _world.Day.ToString();
        }
    }
}