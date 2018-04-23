using System.Collections.Generic;
using System.Linq;

namespace PacMan {

    public enum TileType { Wall, Space, Door, Teleport }

    public class Tile {

        public static Vector2[] Directions = new Vector2[] { Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left };

        public Tile[] Neighbors;

        public Chixel Chixel;
        public Vector2 Position;

        public TileType Type;

        public bool Intersection;


        public Tile() { }

        public Tile(Chixel chixel, Vector2 pos, TileType type) {
            Chixel = chixel;
            Position = pos;
            Type = type;
        }

        public void GetNeighbors(Tile[,] tiles) {
            Neighbors = new Tile[4];

            if (Type == TileType.Wall) return;

            int neighbors = 0;

            for (int i = 0; i < Directions.Length; i++) {
                Vector2 tilePos = new Vector2(Position.X + Directions[i].X, Position.Y + Directions[i].Y);
                Tile tile = tiles[Position.X + Directions[i].X, Position.Y + Directions[i].Y];

                if (tile.Type == TileType.Wall) continue;
                //tile.Chixel.BackgroundColor = System.ConsoleColor.Green;
                Neighbors[i] = tiles[tilePos.X, tilePos.Y];
                neighbors++;
            }

            Intersection = neighbors > 2;
        }

        public bool IsWalkable(object unused) {
            return Type != TileType.Wall;
        }
    }
}
