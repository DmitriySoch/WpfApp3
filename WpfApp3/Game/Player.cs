using System.Windows;

namespace WpfApp3
{
    public class Player : CircleGameObject
    {
        protected const double constGravity = 0.98;
        public Player(Point position,Size size,Vector velocity,int id)
        { 
            Position = position;
            Velocity = velocity;
            Size = size;
            Id = id;
            Boost = new Vector(0,constGravity);
        }

        public void Jump(double heightJump) => Velocity = new Vector(0,-heightJump);

        public override bool OnVertical(Point mapSize) => Position.Y + Radius > 0 && Position.Y - Radius < mapSize.Y;
    }
}
