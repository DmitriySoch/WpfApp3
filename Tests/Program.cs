using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp3;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    public static class TestsCreateObjects
    {
        [Test]
        public static void TestPlayerCreatAndGravityAction()
        {
            var map = new Map(1000, 1000);
            map.CreatePlayer();
            var res = new HashSet<GameObjects>(map.UpdateMap());
            Assert.AreEqual(1, res.Count);
            map.MoveUpPlayer(10);
            var item = map.UpdateMap().First();
            item.UpdatePosition();
            Assert.AreEqual(map.mapSize.Y / 2 - 10 + 0.98, item.Position.Y);
        }

        [Test]
        public static void Test_RemoveNotUsedObjects()
        {
            var map = new Map(1000, 1000);
            var Player = new Player(new Point(-100, 100), Size.Empty, new Vector(), 0);
            map.Add(Player);
            var unUsed = new HashSet<object>(map.GetUnusedItems());
            var res = new HashSet<GameObjects>(map.UpdateMap());
            Assert.AreEqual(0, res.Count);
        }

    }

    [TestFixture]
    public static class Test_Interplay
    {
        [TestCase(100, 100, 100, 100, 120, true)]
        [TestCase(5, 100, 100, 100, 120, false)]
        [TestCase(100, 100, 100, 100, 100, true)]
        [TestCase(100, 100, 100, 100, 1200, false)]
        [TestCase(100, 100, 100, 100, -120, false)]
        [TestCase(100, 100, -100, 100, -120, true)]
        public static void Test_TwoCircleIsTouch(int h, int x1, int y1, int x2, int y2, bool result)
        {
            var firstCircle = new CircleGameObject(new Size(h, h)) { Position = new Point(x1, y1) };
            var secondCircle = new CircleGameObject(new Size(h, h)) { Position = new Point(x2, y2) };
            Assert.AreEqual(result, firstCircle.IsCollided(secondCircle));
        }

        [TestCase(50, 50, 100, 100, 20, 100, 100, true)]
        [TestCase(100, 100, 1000, 100, 20, 100, 100, false)]
        [TestCase(100, 100, 100, 100, 2, 110, 110, true)]
        [TestCase(100, 100, 100, 100, 200, 100, 100, true)]
        [TestCase(10, 10, 100, 100, 100, 100, 100, true)]
        [TestCase(100,100,100,100,50,100,80,true)]
        [TestCase(10,10,100,100,10,90,90,false)]
        [TestCase(10,10,100,100,10,111,110,false)]
        public static void Test_RectangleTouchCircle(int w, int h, int x1, int y1, int r, int x2, int y2, bool result)
        {
            var rect = new RectangleGameObject() { Size = new Size(w, h), Position = new Point(x1, y1) };
            var circle = new CircleGameObject(new Size(r, r)) { Position = new Point(x2, y2) };
            Assert.AreEqual(result, circle.IsCollided(rect));
        }
    }
}
