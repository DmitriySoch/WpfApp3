using System;
using System.Windows;

namespace WpfApp3
{
    class Coins : CircleGameObject
    {
        public Coins(Point dimensions)
        {
            this.Position = GenerateHorizontalPosition(dimensions);  
        }         
    }
}
