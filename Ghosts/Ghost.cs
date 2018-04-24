
namespace PacMan.Ghosts {
    public class Ghost : Character {

        public Tile Target;
        public Tile CurrentTile;
        public Tile NextTile;

        public Vector2 Velocity;

        public Ghost() { }

        public Ghost(Chixel chixel, Vector2 pos) {
            Game.Instance.Characters.Add(this);
            Chixel = chixel;
            Position = pos;
            Velocity = Vector2.Up;
        }

        public override void Update() {
            base.Update();

            if (Target == null) return;

            Target.Chixel.BackgroundColor = Chixel.ForgroundColor;
            Scatter();
        }

        private void Chase() { }

        private void Scatter() {
            //Next tile
            NextTile = Map.Instance.GetTile(Position + Velocity);

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
                    if (!(dis < closest) || Tile.Directions[i] == Velocity.Opposite) continue;

                    closest = dis;
                    if (Tile.Directions[i] != Velocity.Opposite)
                        newDirection = Tile.Directions[i];
                }

                Velocity = newDirection;
            }
        }

        private void Frightened() { }

        private void Normal() { }
    }
}
