
namespace PacMan {
    public class Pacman : Character {

        public static Pacman Instance;

        public Vector2 Velocity;

        public Pacman(Chixel chixel, Vector2 pos) {
            Instance = this;

            Chixel = chixel;
            Position = pos;

            Velocity = Vector2.Right;
        }

        public override void Update() {
            base.Update();

            Tile nextTile = Map.Instance.GetTile(Position + Velocity);
            switch (nextTile.Type) {
                case TileType.Wall:
                    Velocity = Vector2.Zero;
                    break;
                case TileType.Door:
                    Velocity = Vector2.Zero;
                    break;
                case TileType.Teleport:
                    Teleport tel = nextTile as Teleport;
                    Position = tel.TeleportTo.Position;
                    break;
            }

            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
