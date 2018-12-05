using System.Windows;

namespace WpfApp3
{
    public class Coins : CircleGameObject
    {
        public Coins(Point dimensions,Size size, Vector velocity)
        {
            Position = GenerateHorizontalPosition(dimensions);
            Size = size;
            Velocity = velocity;
        }
    }
}
