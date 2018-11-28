using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    class Player : CircleGameObject
    {
        public Player(Image element,Point position)
        {
            this.Element = element;
            Position = position;
            Radius = element.ActualHeight / 2.0;
        }

        public void Jump(double heightJump)
        {
            Position = new Point(Position.X,Position.Y - heightJump);
        }
    }
}
