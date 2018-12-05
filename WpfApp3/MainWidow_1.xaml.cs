using System.Windows;
using System.Windows.Input;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MainWidow_1 : Window
    {
        private int countCoint;

        string nameBird = "Images/Bird/green_Bird_";

        public MainWidow_1(int count)
        {
            countCoint = count;
            InitializeComponent();
            CountCoin.Content = countCoint;

        }

        public void UpdateCoin(int coin)
        {
            countCoint += coin;
            CountCoin.Content = countCoint;
        }

        private void CreateNewGame()
        {
            var game = new Game.Game(nameBird + "Up.png", nameBird + "Down.png", this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
                CreateNewGame();
        }

        private void buttonForStartNewGame_MouseDown(object sender, MouseButtonEventArgs e) => CreateNewGame();

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (countCoint >= 10)
            {
                nameBird = "Images/Bird/blue_Bird_";
            }
            else
                MessageBox.Show("У вас еще не хватает семечек, чтобы завербовать этого голубя", "Ошибочка вышла", MessageBoxButton.OK);
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (countCoint >= 20)
                nameBird = "Images/Bird/pink_Bird_";
            else
                MessageBox.Show("Одумайся, ты его не прокормишь", "Ошибочка вышла", MessageBoxButton.OK);
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (countCoint >= 50)
                nameBird = "Images/Bird/chicken_Bird_";
            else
                MessageBox.Show("Поднакопи еще немного, \n\tи все памятники будут тряcтиcь от страха", "Ошибочка вышла", MessageBoxButton.OK);
        }

        private void Image_MouseDown_3(object sender, MouseButtonEventArgs e) => nameBird = "Images/Bird/green_Bird_";
    }
}
