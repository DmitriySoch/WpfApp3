using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        double horizontalSpeed;
        Image Player;
        Canvas Canvas;
        Queue<UIElement> queueOfPipe = new Queue<UIElement>();
        int counter = 0;
        BitmapImage Up;
        BitmapImage Down;
        DispatcherTimer timer;
        int TotalCountCoin;
        Queue<UIElement> holes = new Queue<UIElement>();
        Queue<UIElement> Coins = new Queue<UIElement>();
        Queue<UIElement> allOtherElements = new Queue<UIElement>();
        private bool PlayerIsDead = false;
        Label CoinCounter;
        MainWidow_1 Widow_1;

        public MainWindow(string pathToBirdUp, string pathToBirdDown, MainWidow_1 widow_1)
        {
            Up = new BitmapImage(new Uri(pathToBirdUp, UriKind.Relative));
            Down = new BitmapImage(new Uri(pathToBirdDown, UriKind.Relative));
            Widow_1 = widow_1;
            InitializeComponent();
            this.KeyDown += this.MainWindow_KeyDown;
        }
        private void UpdatePerTick(object sender, EventArgs e)
        {
            horizontalSpeed += 1;
            if (horizontalSpeed < 0 && counter % 5 == 0)
            {
                Player.Source = Player.Source == Up ? Down : Up;
            }
            Move(queueOfPipe, new Point(-10, 0));
            Move(Coins, new Point(-10, 0));
            Move(allOtherElements, new Point(-10, 0));
            if (Coins.Count > 0)
                CoinActionWithPlayer();
            if (holes.Count > 0)
                PlayerIsAlive();
            PlayerInMap();
            Move(new[] { Player }, new Point(0, horizontalSpeed));
            if (counter++ > 50)
            {
                UpdateMap();
                counter = 0;
            }
            else if (counter == 25)
            {
                CreateCoin();
            }
        }
        private void CreateCoin()
        {
            var coin = new Image() {
                Width = ActualWidth / 15,
                Height = ActualWidth / 15,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri("Images/coin.png", UriKind.Relative))
            };
            Canvas.Children.Add(coin);
            Canvas.SetLeft(coin, Canvas.ActualWidth - 10);
            Canvas.SetTop(coin, Canvas.ActualHeight * Map.GeneratePosition());
            Panel.SetZIndex(coin, 1);
            Coins.Enqueue(coin);
            if (allOtherElements.Count > 0 && Canvas.GetLeft(allOtherElements.Peek()) < 0)
            {
                Canvas.Children.Remove(allOtherElements.Dequeue());
            }
        }
        private void CoinActionWithPlayer()
        {
            var coin = Coins.Peek();
            if (Canvas.GetLeft(coin) <= Canvas.GetLeft(Player) + Player.Width)
            {
                if (Canvas.GetLeft(Player) > Canvas.GetLeft(coin) + coin.DesiredSize.Width)
                {
                    allOtherElements.Enqueue(Coins.Dequeue());
                    return;
                }
                if (Math.Abs(Canvas.GetTop(Player) + Player.ActualWidth / 2 - Canvas.GetTop(coin) - coin.DesiredSize.Height / 2) < 30)
                {
                    Canvas.Children.Remove(Coins.Dequeue());
                    TotalCountCoin++;
                    CoinCounter.Content = TotalCountCoin;
                }
            }
        }
        private void PlayerInMap()
        {
            if (Canvas.GetTop(Player) < 0 || Canvas.GetTop(Player) + Player.ActualHeight > ActualHeight)
                PLayerDead();
        }
        private void PlayerIsAlive()
        {
            var rectangle = holes.Peek();

            if (Canvas.GetLeft(rectangle) <= Canvas.GetLeft(Player) + Player.Width - 20)
            {
                if (Canvas.GetLeft(Player) > Canvas.GetLeft(rectangle) + rectangle.DesiredSize.Width)
                {
                    holes.Dequeue();
                    return;
                }
                if (Canvas.GetTop(rectangle) >= Canvas.GetTop(Player) + 20
                    || Canvas.GetTop(rectangle) + rectangle.DesiredSize.Height <= Canvas.GetTop(Player) + Player.Height - 20)
                {
                    PlayerIsDead = true;
                    PLayerDead();
                }
            }
        }
        private async void PLayerDead()
        {
            timer.Stop();
            var rectangle = new Rectangle() {
                Width = this.ActualWidth * 2, Height = this.ActualHeight * 2,
                Fill = Brushes.Black, Opacity = 0.7
            };
            Canvas.Children.Add(rectangle);
            Panel.SetZIndex(rectangle, 3);
            var text = new Image() {
                Width = ActualWidth/3,
                Height = ActualHeight / 3,
                Source = new BitmapImage(new Uri("Images/GameOver.png", UriKind.Relative)),
                Stretch = Stretch.Fill
            };
            Canvas.Children.Add(text);
            Canvas.SetLeft(text, Canvas.ActualWidth / 2 - text.Width / 2);
            Canvas.SetTop(text, ActualHeight / 2 - text.Height / 2);
            Panel.SetZIndex(text, 3);
            await Task.Delay(1000);
            this.Close();
        }
        private void UpdateMap()
        {
            var centralPos = ActualHeight * Map.GeneratePosition();

            var pipe_Up = new Image() {
                Width = ActualWidth / 10,
                Height = ActualHeight,
                Source = new BitmapImage(new Uri("Images/truba_Up.png", UriKind.Relative)),
                Stretch = Stretch.Fill
            };
            var pipe_Down = new Image() {
                Width = ActualWidth / 10,
                Height = ActualHeight,
                Source = new BitmapImage(new Uri("Images/truba_Down.png", UriKind.Relative)),
                Stretch = Stretch.Fill
            };
            var hole = new Rectangle() {
                Width = ActualWidth / 10,
                Height = ActualHeight / 3
            };

            Canvas.Children.Add(pipe_Down);
            Canvas.Children.Add(pipe_Up);
            Canvas.Children.Add(hole);

            queueOfPipe.Enqueue(pipe_Down);
            queueOfPipe.Enqueue(pipe_Up);
            queueOfPipe.Enqueue(hole);
            holes.Enqueue(hole);

            Canvas.SetLeft(pipe_Up, Canvas.ActualWidth - 10);
            Canvas.SetLeft(hole, Canvas.ActualWidth - 10);
            Canvas.SetLeft(pipe_Down, Canvas.ActualWidth - 10);

            Canvas.SetTop(pipe_Up, centralPos - hole.Height / 2 - pipe_Up.Height);
            Canvas.SetTop(hole, centralPos - hole.Height / 2);
            Canvas.SetTop(pipe_Down, centralPos + hole.Height / 2);

            if (Canvas.GetLeft(queueOfPipe.Peek()) < 0)
            {
                for (var i = 0; i < 3; i++)
                    Canvas.Children.Remove(queueOfPipe.Dequeue());
            }
        }
        private async void SetPause()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                var backGround = new Rectangle {
                    Fill = Brushes.White,
                    Width = 10000, Height = this.ActualHeight / 3, Opacity = 0.5
                };
                Panel.SetZIndex(backGround, 8);
                Canvas.Children.Add(backGround);
                Canvas.SetTop(backGround, (this.ActualHeight - backGround.Height) / 2);
                var pause = new Image() {
                    Width = this.ActualWidth / 4, Height = this.ActualHeight / 5,
                    Source = new BitmapImage(new Uri("Images/image_pause.png", UriKind.Relative)),
                    Stretch = Stretch.Fill
                };
                Canvas.Children.Add(pause);
                Panel.SetZIndex(pause, 8);
                Canvas.SetLeft(pause, (ActualWidth + 600 - pause.Width) / 2);
                Canvas.SetTop(pause, (ActualHeight - pause.Height) / 2);
            }
            else
            {
                Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
                var counterImages = new Image() {
                    Width = ActualWidth / 7, Height = ActualHeight / 6
                };
                Canvas.Children.Add(counterImages);
                Canvas.SetTop(counterImages, (ActualHeight - counterImages.Height) / 2);
                Canvas.SetLeft(counterImages, (ActualWidth + 600 - counterImages.Width) / 2);
                Canvas.SetZIndex(counterImages, 8);
                for (var i = 1; i <= 3; i++)
                {
                    counterImages.Source = new BitmapImage(new Uri($"Images/number_{4 - i}.png", UriKind.Relative));
                    await Task.Delay(1000);
                }
                counterImages.Source = new BitmapImage(new Uri("Images/image_Go.png", UriKind.Relative));
                Canvas.Children.RemoveAt(Canvas.Children.Count - 2);
                await Task.Delay(300);
                Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
                timer.Start();
            }
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && timer.IsEnabled)
            {
                horizontalSpeed = -15;
            }
            if (e.Key == Key.Escape)
            {
                if (!PlayerIsDead)
                    SetPause();
            }
        }
        private static void Move(IEnumerable<UIElement> elements, Point size)
        {
            foreach (var element in elements)
            {
                Canvas.SetLeft(element, Canvas.GetLeft(element) + size.X);
                Canvas.SetTop(element, Canvas.GetTop(element) + size.Y);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var canvas = new Canvas() { Margin = new Thickness(-300, 0, -300, 0) };
            Canvas = canvas;
            this.Content = canvas;
            var player = new Image() {
                Height = 100, Width = 100,
                Source = Down,
                Stretch = Stretch.Fill
            };

            Player = player;
            canvas.Children.Add(player);
            Canvas.SetLeft(player, 550);
            Canvas.SetTop(player, 100);
            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(20) };
            timer.Tick += new EventHandler(UpdatePerTick);
            timer.Start();

            var coinIcon = new Image() {
                Width = ActualWidth / 20,
                Height = ActualWidth / 15,
                Source = new BitmapImage(new Uri("Images/seeds.png", UriKind.Relative)),
                Stretch = Stretch.Fill
            };
            Canvas.Children.Add(coinIcon);
            Canvas.SetTop(coinIcon, 10);
            Canvas.SetLeft(coinIcon, 310);
            Panel.SetZIndex(coinIcon, 2);

            var coinCount = new Label() {
                Content = TotalCountCoin,
                Width = ActualWidth / 5,
                Height = ActualWidth / 30,
                Foreground = Brushes.AntiqueWhite,
                FontFamily = new FontFamily("Arial"),
                FontSize = 36
            };
            Canvas.Children.Add(coinCount);
            Canvas.SetTop(coinCount, 10);
            Canvas.SetLeft(coinCount, 310 + ActualWidth / 20);
            Panel.SetZIndex(coinCount, 2);
            CoinCounter = coinCount;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Widow_1.UpdateCoin(TotalCountCoin);
            this.Close();
        }
    }
}
