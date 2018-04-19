
namespace PacMan {

    public enum TileType { Wall, Teleport }

    public class Tile {

        public Chixel Chixel;
        public Vector2 Position;

        public TileType Type;

        public Tile() { }

        public Tile(Chixel chixel, Vector2 pos, TileType type) {
            Chixel = chixel;
            Position = pos;
            Type = type;
        }
    }
}
