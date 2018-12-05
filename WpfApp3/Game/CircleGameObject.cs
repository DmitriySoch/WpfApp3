using System;
using System.Windows;

namespace WpfApp3
{
    public class CircleGameObject : GameObjects
    {
        public CircleGameObject() { }
        public CircleGameObject(Size size)
        {
            Size = size;
            Radius = size.Height / 2;
        }

        private protected double Radius { get; set; } 
        
        public bool IsCollided(RectangleGameObject rectangle)
        #region Don`t open. There is a bad code. Sorry...
        {

            var rectanglePos = rectangle.GetPosition();
            if (Position.X - Radius <= rectanglePos.X + rectangle.SizeRectangele.Width / 2 ||
                Position.X + Radius >= rectanglePos.X - rectangle.SizeRectangele.Width / 2)
            {
                if (Position.X >= rectanglePos.X - rectangle.SizeRectangele.Width / 2 &&
                 Position.X <= rectanglePos.X + rectangle.SizeRectangele.Width/ 2)
                {
                    return Position.Y - Radius <= rectanglePos.Y - rectangle.SizeRectangele.Height / 2
                   || Position.Y + Radius >= rectanglePos.Y + rectangle.SizeRectangele.Height / 2;
                }
                else
                {
                    if (Position.X + Radius + 10 - rectanglePos.X + rectangle.SizeRectangele.Width / 2 > 0)
                    {
                        return LOL(Position, rectanglePos.X - rectangle.SizeRectangele.Width / 2, Radius,
                            rectanglePos.Y + rectangle.SizeRectangele.Height / 2, rectanglePos.Y - rectangle.SizeRectangele.Height / 2);
                    }
                    else
                    {
                        return LOL(Position, rectanglePos.X + rectangle.SizeRectangele.Width / 2, Radius,
                            rectanglePos.Y + rectangle.SizeRectangele.Height / 2, rectanglePos.Y - rectangle.SizeRectangele.Height / 2);
                    }
                }
            }
            else
                return false;
        }

        private bool LOL(Point position, double v1, double radius, double v3, double v2)
        {
            var e = Math.Sqrt(radius * radius - Math.Pow(v1 - position.X, 2));
            return position.Y - e < v2 || position.Y + e > v3;
        }
        #endregion

        public bool IsCollided(CircleGameObject circle)
        {
            var distance = Math.Sqrt(Math.Pow(this.Position.X - circle.Position.X, 2)
                + Math.Pow(this.Position.Y - circle.Position.Y, 2));
            return distance < this.Radius + circle.Radius;
        }
    }
}
