
namespace PacMan.Ghosts {
    public class Clyde : Ghost {
        public Clyde(Chixel chixel, Vector2 pos) {
            Chixel = chixel;
            Position = pos;
            Velocity = Vector2.Up;
            
            Game.Instance.Characters.Add(this);

            ScatterTarget = Map.Instance.GetTile(new Vector2(3, Map.Instance.MapSize.Y - 2));
        }

        public override void Update() {
            base.Update();

            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
