
namespace PacMan {
    public class Teleport : Tile {

        public Tile TeleportTo;

        public Teleport(Chixel chixel, Vector2 pos, TileType type) {
            Chixel = chixel;
            Position = pos;
            Type = type;
        }
    }
}
