using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp3
{
    public class GameObjects
    {
        public Point Position { get;  set; }
        public Vector Velocity { get; private protected set; }
        public Vector Boost = new Vector(0, 0);                 
        public int Id;
        public Size Size;

        public Vector UpdatePosition()
        {
            Position = new Point(Position.X + Velocity.X, Position.Y + Velocity.Y);
            return Velocity;
        }

        public virtual bool OnVertical(Point mapSize) => Position.Y > 0 && Position.Y < mapSize.Y;

        public virtual bool OnHorizontal(Point mapSize) => Position.X > 0 && Position.X < mapSize.X;

        public bool IsObjectTouch(GameObjects secondElement) => throw new NotImplementedException();

        public static Point GenerateHorizontalPosition(Point dimensions)
        {
            var multiplier = new Random().Next(200, 800) / 1000.0;
            return new Point(dimensions.X, multiplier * dimensions.Y);
        }

        internal void UpdateSpeed()
        {
            Velocity += Boost;
        }
    }
}
