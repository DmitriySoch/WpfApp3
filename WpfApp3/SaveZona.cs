using System.Windows;

namespace WpfApp3
{
    class SaveZona : RectangleGameObject
    {
        public SaveZona(Point dimensions) => this.Position = GenerateHorizontalPosition(dimensions);
    }
}
