using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp3
{
    public class Map
    {
        private readonly HashSet<GameObjects> gameMap;
        Player player;
        public Point mapSize;
        HashSet<int> allObjectId;

        public Map(double width, double height)
        {
            mapSize = new Point(height, width);
            gameMap = new HashSet<GameObjects>();
            allObjectId = new HashSet<int>();
        }

        public Player CreatePlayer()
        {
            var pos = new Point((mapSize.X - 600) / 4 + 300, mapSize.Y / 2);
            player = new Player(pos, new Size(100, 100), new Vector(0, 0), GetId());
            gameMap.Add(player);
            return player;
        }

        public void Add<T>(T value) where T : GameObjects
        {
            value.Id = GetId();
            gameMap.Add(value);
        }

        public int GetId()
        {
            var rnd = new Random();
            while (true)
            {
                var newId = rnd.Next(100000);
                if (!allObjectId.Contains(newId))
                {
                    allObjectId.Add(newId);
                    return newId;
                }
            }
        }

        public IEnumerable<GameObjects> GetUnusedItems()
        {
            var collection = gameMap.Where(x => !x.OnHorizontal(mapSize)).ToArray();
            foreach (var item in collection)
            {
                gameMap.Remove(item);
                allObjectId.Remove(item.Id);
                yield return item;
            }
        }

        public int CheckCollised()
        {
            var count = 0;
            foreach (var item in gameMap)
            {
                if (item is Coins)
                {
                    var circle = item as CircleGameObject;
                    if (player.IsCollided(circle))
                        count++;
                }
                else if (item is SaveZona)
                {
                    var rectangle = item as SaveZona;
                    if (player.IsCollided(rectangle))
                        throw new Exception("Произошло столкновение с трубой");
                }
            }
            return count;
        }

        public void MoveUpPlayer(double offset)
        {
            if (player == null)
                throw new ArgumentException();
            player.Jump(offset);
        }

        public GameObjects GetPlayerIfHeDead() => !player.OnVertical(mapSize) ? player : null;       

        public IEnumerable<GameObjects> UpdateMap()
        {
            foreach (var item in gameMap)
            {
                item.UpdateSpeed();
                yield return item;
            }
        }
    }
}
