using System.Collections.Generic;
using System.Linq;

namespace PacMan {

    public enum TileType { Wall, Space, Door, Teleport }

    public class Tile {

        public static Vector2[] Directions = new Vector2[] { Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left };

        /// <summary>
        /// Up, Right, Down, Left
        /// </summary>
        public Tile[] Neighbors;

        public Chixel Chixel;
        public Vector2 Position;

        public TileType Type;

        public bool Intersection;
        public bool Corner;

        public Tile() { }

        public Tile(Chixel chixel, Vector2 pos, TileType type) {
            Chixel = chixel;
            Position = pos;
            Type = type;
            Neighbors = new Tile[4];
        }

        public bool IsWalkable(object unused) {
            return Type != TileType.Wall;
        }

        public override string ToString() {
            return Type.ToString();
        }
    }
}
