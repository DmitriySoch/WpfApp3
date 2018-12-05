using System.Windows;

namespace WpfApp3
{
    public class SaveZona : RectangleGameObject
    {
        public SaveZona(Point dimensions, Vector velocity, Size size)
        {
            Position = GenerateHorizontalPosition(dimensions);
            Velocity = velocity;
            Size = size;
        }
    }
}
