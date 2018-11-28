using System;
using System.Windows;

namespace WpfApp3
{
    class GameObjects
    {
        private protected Point Position { get; set; }
        private protected Point Velocity { get; set; }
        private protected UIElement Element { get; set; }

        public void UpdatePosition()
        {
            Position = new Point(Position.X + Velocity.X, Position.Y + Velocity.Y);
        }

        public bool OnVertical(Point mapSize) => Position.Y > 0 && Position.Y < mapSize.Y;

        public bool OnHorizontal(Point mapSize) => Position.X > 0 && Position.X < mapSize.X;

        public bool IsObjectTouch(GameObjects secondElement) => throw new NotImplementedException();

        public static Point GenerateHorizontalPosition(Point dimensions)
        {
            var multiplier = new Random().Next(100, 900) / 1000.0;
            return new Point(dimensions.X, multiplier * dimensions.Y);
        }
    }
}
