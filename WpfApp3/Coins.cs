using System;
using System.Windows;

namespace WpfApp3
{
    public class Coins : CircleGameObject
    {
        public Coins(Point dimensions)
        {
            this.Position = GenerateHorizontalPosition(dimensions);  
        }         
    }
}
