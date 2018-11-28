using System.Windows;

namespace WpfApp3
{
    public class SaveZona : RectangleGameObject
    {
        public SaveZona(Point dimensions) => this.Position = GenerateHorizontalPosition(dimensions);
    }
}
