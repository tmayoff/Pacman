
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

            foreach (Tile tile in Game.Instance.Tiles) {
                if (tile.Position != (Position + Velocity)) continue;
                switch (tile.Type) {
                    case TileType.Wall:
                        Velocity = Vector2.Zero;
                        break;
                    case TileType.Teleport:
                        Teleport tel = tile as Teleport;
                        Position = tel.TeleportTo.Position;
                        break;
                }
            }

            //if (Velocity == Vector2.Right || Velocity == Vector2.Left)
                //Velocity *= 2;
            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
