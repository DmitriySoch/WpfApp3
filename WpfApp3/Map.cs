using System;

namespace WpfApp3
{
    class Map
    {
        public static double GeneratePosition()
        {
            var rnd = new Random();
            return rnd.Next(200, 800) / 1000.0;
        }
    }
}
