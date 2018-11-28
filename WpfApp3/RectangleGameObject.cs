using System.Windows;

namespace WpfApp3
{
    public class RectangleGameObject:GameObjects
    {
        public Point SizeRectangele { get; private protected set; } 

        public Point GetPosition()
        {
            return this.Position;
        }

        public bool IsCollided(CircleGameObject circle)
        {
            return circle.IsCollided(this);
        }
    }
}
