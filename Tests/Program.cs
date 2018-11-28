using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    public static class Tests
    {
        [Test]
        public static void Test_1()
        {
            var map = new WpfApp3.Map(1000, 1000);
            var Player = new WpfApp3.Player(null, new Point(100, 100), 100);
            map.Add(Player);
            var res = new HashSet<WpfApp3.GameObjects>(map.UpdateMap());
            Assert.AreEqual(1, res.Count);
            map.MoveUpPlayer(10);
            Assert.AreEqual(90, map.UpdateMap().First().Position.Y);
        }

        [Test]
        public static void Test_2()
        {
            var map = new WpfApp3.Map(1000, 1000);
            var Player = new WpfApp3.Player(null, new Point(-100, 100), 100);
            map.Add(Player);
            var unUsed = new HashSet<object>(map.GetUnusedItems());
            var res = new HashSet<WpfApp3.GameObjects>(map.UpdateMap());
            Assert.AreEqual(0, res.Count);
        }

        [Test]
        public static void Test_3()
        {
            var map = new WpfApp3.Map(1000, 1000);
            var Player = new WpfApp3.Player(null, new Point(100, 100), 100);
            map.Add(Player);
            var coin = new WpfApp3.Coins(new Point(10, 10));
        }
    }

}
