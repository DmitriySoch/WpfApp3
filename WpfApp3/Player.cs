using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public class Player : CircleGameObject
    {
        public Player(Image element,Point position,double height)
        {
            this.Element = element;
            Position = position;
            Radius = height/2;            
        }

        public void Jump(double heightJump)
        {
            Position = new Point(Position.X,Position.Y - heightJump);
        }
    }
}
