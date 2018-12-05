using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfApp3.Game
{
    class Game
    {
        private Map MainMap;
        private int counter;
        DispatcherTimer timer;
        MainWindow window;
        private int countOfPipe;
         
        //Добавить взаимодействие объектов
        //Добавить тесты
        //Добавить установку паузы
        //Добавить счетчик монеток
        //Почистить код
        //Добавить вывод разных птичек

        public Game(string v1, string v2, MainWidow_1 mainWidow_1)
        {
            window = new MainWindow(v1, v2, mainWidow_1);
            window.KeyDown += new KeyEventHandler(KeyDown);
            StartTimer(TimeSpan.FromMilliseconds(10));
            window.Loaded += (s, e) => WindowLoad(s, e);
            window.ShowDialog();
        }

        private void WindowLoad(object sender, EventArgs e)
        {
            MainMap = new Map(window.ActualHeight, window.ActualWidth + 600);
            window.CreatePlayer(MainMap.CreatePlayer());
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                MainMap.MoveUpPlayer(MainMap.mapSize.Y / 50);
            }
            // if (e.Key == Key.Escape)
            //SetPause();
        }

        private void StartTimer(TimeSpan IntervalTick)
        {
            timer = new DispatcherTimer() { Interval = IntervalTick };
            timer.Tick += new EventHandler(UpdatePerTick);
            timer.Start();
        }

        private void UpdatePerTick(object sender, EventArgs e)
        { 
            UpdatePositionAllElements();
            RemoveAllUnusedElements();
            CheckPlayerDead();
            GenerateGameObject();
        }

        private void GenerateGameObject()
        {  
            if (++counter == (int)MainMap.mapSize.X / 32)
            {
                GeneratePipe(countOfPipe/10);
                countOfPipe++;
                counter = 0;
            }
            else if (counter == (int)MainMap.mapSize.X / 64)
                GenerateCoin(countOfPipe/10);
        }

        private void GeneratePipe(int speed)
        {
            var pipe = new SaveZona(
                MainMap.mapSize,
                new System.Windows.Vector(-10-speed, 0), 
                new System.Windows.Size(150, 300));
            MainMap.Add(pipe);
            window.CreatePipe(pipe);
        }

        private void GenerateCoin(int speed)
        {
            var coin = new Coins(MainMap.mapSize, new System.Windows.Size(100, 100), new System.Windows.Vector(-10-speed, 0));
            MainMap.Add(coin);
            window.CreateCoin(coin);
        }

        private void CheckPlayerDead()
        {
            var player = MainMap.GetPlayerIfHeDead();
            if (player != null)
            {
                window.GameOverAsync(player);
                timer.Stop();
            }
        }

        private void RemoveAllUnusedElements()
        {
            foreach (var item in MainMap.GetUnusedItems())
            {
                window.RemoveElement(item.Id);
            }
        }

        private void UpdatePositionAllElements()
        {
            foreach (var item in MainMap.UpdateMap())
            {
                window.ChangePositionElement(item.Id, item.UpdatePosition()); 
            }
        }
    }
}
