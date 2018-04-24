namespace PacMan.Ghosts {
    public class Blinky : Ghost {

        public Blinky(Chixel chixel, Vector2 pos) {
            Chixel = chixel;
            Position = pos;
            Velocity = Vector2.Left;

            Game.Instance.Characters.Add(this);
            Target = Map.Instance.GetTile(new Vector2(3, 0));
        }

        public override void Update() {
            base.Update();

            //Next tile
            NextTile = Map.Instance.GetTile(Position + Velocity);
            //if (NextTile.Type == TileType.Wall)
            //    Velocity = Vector2.Zero;

            //Current tile
            CurrentTile = Map.Instance.GetTile(Position);
            if (CurrentTile.Intersection || NextTile.Type == TileType.Wall) {

                int r;
                if (Position.Y == 2)
                    r = 0;

                double closest = double.MaxValue;
                Vector2 newDirection = Vector2.Zero;

                for (int i = 0; i < CurrentTile.Neighbors.Length; i++) {
                    Tile tile = CurrentTile.Neighbors[i];
                    if (tile == null) continue;

                    double dis = Vector2.Distance(tile.Position, Target.Position);
                    if (!(dis < closest)) continue;

                    closest = dis;
                    if (Tile.Directions[i] != Velocity.Opposite)
                        newDirection = Tile.Directions[i];
                }

                Velocity = newDirection;
            }

            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
