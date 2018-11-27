using System;
using System.Windows;

namespace WpfApp3
{
    class GameObjects
    {
        public Point position;
        public Point ofsetPosition;
        public readonly UIElement Element; 

        public bool OnVertical(Point mapSize) => position.Y > 0 && position.Y < mapSize.Y;

        public bool OnHorizontal(Point mapSize) => position.X > 0 && position.X < mapSize.X;

        public bool IsObjectTouch(GameObjects secondElement) => throw new NotImplementedException();

        public void UpdatePosition() => position = new Point(position.X + ofsetPosition.X, position.Y + ofsetPosition.Y);
    }
}
