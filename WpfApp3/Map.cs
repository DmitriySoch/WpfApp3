using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp3
{
    class Map
    {
        private readonly Dictionary<GameObjects, Point> gameMap;
        Player player;
        Point mapSize;

        public Map(double height, double width)
        {
            mapSize = new Point(height, width);
            gameMap = new Dictionary<GameObjects, Point>();
        }

        public void Add<T>(T value) where T : GameObjects
        {
            player = player ?? value as Player;
            gameMap.Add(value, value.position);
        }

        public IEnumerable<GameObjects> GetUnusedItems() => gameMap.Keys.Where(x => !x.OnHorizontal(mapSize));

        public void UpdateMap()
        {
            foreach (var item in gameMap)
            {
                item.Key.UpdatePosition();
            }
        }
    }
}
