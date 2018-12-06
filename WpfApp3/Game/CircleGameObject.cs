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
            var rectSize = rectangle.Size;
            if (Position.X - Radius <= rectanglePos.X + rectSize.Width / 2 ||
                Position.X + Radius >= rectanglePos.X - rectSize.Width / 2)
            //Проверили, что круг теоритически может касаться прямоугольника
            {
                return CheckForTouch(rectanglePos, rectSize);
            }
            return false;
        }

        private bool CheckForTouch(Point rectanglePos, Size rectSize)
        {
            if (Position.X >= rectanglePos.X - rectSize.Width / 2 &&
                 Position.X <= rectanglePos.X + rectSize.Width / 2)
            //Проверили на то что центр круга лежит в том же горизонтальном интервале что и прямоугольник
            {
                return Position.Y - Radius >= rectanglePos.Y - rectSize.Height / 2
               || Position.Y + Radius <= rectanglePos.Y + rectSize.Height / 2
               || (Position.Y < rectanglePos.Y + rectSize.Height / 2 && Position.Y > rectanglePos.Y - rectSize.Height / 2);
                //true если крайняя верхняя или нижняя точка круга принадлежит прямоугольнику
            }
            else
            {
                if (Position.X + Radius - rectanglePos.X - rectSize.Width / 2 > 0)
                //Проверили что круг лежит по левую сторону прямоугольника
                {
                    return LOL(Position, rectanglePos.X - rectSize.Width / 2, Radius,
                        rectanglePos.Y + rectSize.Height / 2, rectanglePos.Y - rectSize.Height / 2);
                }
                else
                {
                    return LOL(Position, rectanglePos.X + rectSize.Width / 2, Radius,
                        rectanglePos.Y + rectSize.Height / 2, rectanglePos.Y - rectSize.Height / 2);
                }
            }
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
