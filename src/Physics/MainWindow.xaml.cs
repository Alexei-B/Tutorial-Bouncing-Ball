using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Rendering;
using System;

namespace Physics
{
    public class MainWindow : Window
    {
        private PhysicsCanvas PhysicsCanvas;
        private RenderLoop RenderLoop;

        private Button ClearButton;

        private static Color[] BallColors = new Color[]
        {
            Colors.Blue,
            Colors.Cyan,
            Colors.Green,
            Colors.GreenYellow,
            Colors.YellowGreen,
            Colors.Yellow,
            Colors.Orange,
            Colors.OrangeRed,
            Colors.Red,
            Colors.Purple
        };

        public MainWindow()
        {
            InitializeComponent();

            PhysicsCanvas = this.Find<PhysicsCanvas>("PhysicsCanvas");
            PointerPressed += MainWindow_PointerPressed;

            ClearButton = this.Find<Button>("Clear");
            ClearButton.Click += ClearButton_Click;

            RenderLoop = new RenderLoop();
            RenderLoop.Add(PhysicsCanvas);
        }

        private void MainWindow_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            var rng = new Random();
            var radius = rng.NextDouble() * 100;
            Point position = e.GetPosition(PhysicsCanvas);

            var ball = new Ball(
                new Vector(position.X, position.Y),
                radius,
                new Vector(rng.NextDouble() * 100 - 50, rng.NextDouble() * 100 - 50),
                BallColors[rng.Next(BallColors.Length)]
            );

            PhysicsCanvas.AddBall(ball);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            PhysicsCanvas.Clear();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
