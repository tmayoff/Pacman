namespace PacMan.Ghosts {

    public class Blinky : Ghost {

        public Blinky(Chixel chixel, Vector2 pos) {
            Chixel = chixel;
            Position = pos;
            Velocity = Vector2.Right;

            Game.Instance.Characters.Add(this);

            ScatterTarget = Map.Instance.GetTile(new Vector2(Map.Instance.MapSize.X - 4, 0));
        }

        public override void Update() {
            base.Update();

            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
