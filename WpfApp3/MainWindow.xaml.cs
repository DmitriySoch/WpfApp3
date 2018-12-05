using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        Canvas Canvas;
        Image Player;
        BitmapImage Up;
        BitmapImage Down;
        int TotalCountCoin;
        Label CoinCounter;
        MainWidow_1 Widow_1;
        Dictionary<int, UIElement> allObjectToId;

        public MainWindow(string pathToBirdUp, string pathToBirdDown, MainWidow_1 widow_1)
        {
            Up = new BitmapImage(new Uri(pathToBirdUp, UriKind.Relative));
            Down = new BitmapImage(new Uri(pathToBirdDown, UriKind.Relative));
            Widow_1 = widow_1;
            InitializeComponent();
        }

        internal void CreatePlayer(Player player)
        {
            if (Player != null)
                throw new Exception("Попытка добавления нового игрока, при уже существуещем");
            Player = new Image() {
                Height = player.Size.Height,
                Width = player.Size.Width,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri("Images/Bird/green_Bird_Up.png",UriKind.Relative))
            };
            allObjectToId.Add(player.Id, Player);
            Canvas.Children.Add(Player);
            Canvas.SetLeft(Player, player.Position.X);
            Canvas.SetTop(Player, player.Position.Y);
        }

        internal void RemoveElement(int id)
        {
            Canvas.Children.Remove(allObjectToId[id]);
            allObjectToId.Remove(id);
        }

        internal void ChangePositionElement(int id, Vector offset)
        {
            Canvas.SetLeft(allObjectToId[id], Canvas.GetLeft(allObjectToId[id])+ offset.X);
            Canvas.SetTop(allObjectToId[id], Canvas.GetTop(allObjectToId[id]) + offset.Y);
        }

        internal async Task GameOverAsync(GameObjects player)
        {
            var rectangle = new Rectangle() {
                Width = this.ActualWidth * 2, Height = this.ActualHeight * 2,
                Fill = Brushes.Black, Opacity = 0.7
            };
            Canvas.Children.Add(rectangle);
            Panel.SetZIndex(rectangle, 3);
            var text = new Image() {
                Width = ActualWidth / 3,
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

        internal void CreatePipe(SaveZona item)
        {
            var pipe = new Image() {
                Height = item.Size.Height*6,
                Width = item.Size.Width,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri("Images/truba_2.png", UriKind.Relative))
            };
            allObjectToId.Add(item.Id,pipe);
            Canvas.Children.Add(pipe);
            Canvas.SetLeft(pipe, item.Position.X);
            Canvas.SetTop(pipe, item.Position.Y-item.Size.Height/2*5);
        }

        internal void CreateCoin(Coins item)
        {
            var coin = new Image() {
                Height = item.Size.Height,
                Width = item.Size.Width,
                Stretch = Stretch.Fill,
                Source = new BitmapImage(new Uri("Images/coin.png", UriKind.Relative))
            };
            allObjectToId.Add(item.Id, coin);
            Canvas.Children.Add(coin);
            Canvas.SetLeft(coin, item.Position.X);
            Canvas.SetTop(coin, item.Position.Y);
        }

        //private async void SetPause()
        //{
        //    if (timer.IsEnabled)
        //    {
        //        timer.Stop();
        //        var backGround = new Rectangle {
        //            Fill = Brushes.White,
        //            Width = 10000, Height = this.ActualHeight / 3, Opacity = 0.5
        //        };
        //        Panel.SetZIndex(backGround, 8);
        //        Canvas.Children.Add(backGround);
        //        Canvas.SetTop(backGround, (this.ActualHeight - backGround.Height) / 2);
        //        var pause = new Image() {
        //            Width = this.ActualWidth / 4, Height = this.ActualHeight / 5,
        //            Source = new BitmapImage(new Uri("Images/image_pause.png", UriKind.Relative)),
        //            Stretch = Stretch.Fill
        //        };
        //        Canvas.Children.Add(pause);
        //        Panel.SetZIndex(pause, 8);
        //        Canvas.SetLeft(pause, (ActualWidth + 600 - pause.Width) / 2);
        //        Canvas.SetTop(pause, (ActualHeight - pause.Height) / 2);
        //    }
        //    else
        //    {
        //        Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
        //        var counterImages = new Image() {
        //            Width = ActualWidth / 7, Height = ActualHeight / 6
        //        };
        //        Canvas.Children.Add(counterImages);
        //        Canvas.SetTop(counterImages, (ActualHeight - counterImages.Height) / 2);
        //        Canvas.SetLeft(counterImages, (ActualWidth + 600 - counterImages.Width) / 2);
        //        Canvas.SetZIndex(counterImages, 8);
        //        for (var i = 1; i <= 3; i++)
        //        {
        //            counterImages.Source = new BitmapImage(new Uri($"Images/number_{4 - i}.png", UriKind.Relative));
        //            await Task.Delay(1000);
        //        }
        //        counterImages.Source = new BitmapImage(new Uri("Images/image_Go.png", UriKind.Relative));
        //        Canvas.Children.RemoveAt(Canvas.Children.Count - 2);
        //        await Task.Delay(300);
        //        Canvas.Children.RemoveAt(Canvas.Children.Count - 1);
        //        timer.Start();
        //    }
        //} 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allObjectToId = new Dictionary<int, UIElement>();
            var canvas = new Canvas() { Margin = new Thickness(-300, 0, -300, 0) };
            Canvas = canvas;
            this.Content = canvas;

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
