using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp3
{
    class Map
    {
        private readonly HashSet<GameObjects> gameMap;
        Player player;
        Point mapSize;

        public Map(double height, double width)
        {
            mapSize = new Point(height, width);
            gameMap = new HashSet<GameObjects>();
        }

        public void Add<T>(T value) where T : GameObjects
        {
            player = player ?? value as Player;
            gameMap.Add(value);
        }

        public IEnumerable<GameObjects> GetUnusedItems()
        {
            foreach(var item in gameMap.Where(x => !x.OnHorizontal(mapSize)))
            {
                gameMap.Remove(item);
                yield return item;
            }
        }

        public void MoveUpPlayer(double offset)
        {
            if (player == null)
                throw new ArgumentException();
            player.position = new Point(player.position.X, player.position.Y - offset);
        }

        public void UpdateMap()
        {
            foreach (var item in gameMap)
            {
                item.UpdatePosition();
            }
        }
    }
}
