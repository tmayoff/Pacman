namespace PacMan.Ghosts {

    public class Pinky : Ghost {

        public Pinky(Chixel chixel, Vector2 pos) {
            Chixel = chixel;
            Position = pos;
            Velocity = Vector2.Up;

            Game.Instance.Characters.Add(this);

            Target = Map.Instance.GetTile(new Vector2(3, 0));
        }

        public override void Update() {
            base.Update();

            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
