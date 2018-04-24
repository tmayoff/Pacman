
using System.Threading;

namespace PacMan.Ghosts {
    public class Ghost : Character {

        public Tile Target;
        public Tile CurrentTile;
        public Tile NextTile;

        public Tile ScatterTarget;

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

            if (ScatterTarget != null)
                ScatterTarget.Chixel.BackgroundColor = Chixel.ForgroundColor;

            //Next tile
            NextTile = Map.Instance.GetTile(Position + Velocity);

            CurrentTile = Map.Instance.GetTile(Position);
            if (CurrentTile.Type == TileType.GhostHouse)
                EscapeGhostHouse();
            else
                Scatter();

            Move();
        }

        private void Move() {
            if (CurrentTile.Intersection || NextTile.Type == TileType.Wall) {
                double closest = double.MaxValue;
                Vector2 newDirection = Vector2.Zero;

                for (int i = 0; i < CurrentTile.Neighbors.Length; i++) {
                    Tile tile = CurrentTile.Neighbors[i];
                    if (tile == null) continue;

                    double dis = Vector2.Distance(tile.Position, Target.Position);
                    if ((dis >= closest) || Tile.Directions[i] == Velocity.Opposite) continue;

                    closest = dis;

                    if (Tile.Directions[i] != Velocity.Opposite)
                        newDirection = Tile.Directions[i];
                }

                Velocity = newDirection;
            }
        }

        private void EscapeGhostHouse() {
            bool foundDoor = false;
            foreach (Tile tile in CurrentTile.Neighbors) {
                if (tile?.Type != TileType.Door) continue;
                Target = tile;
                foundDoor = true;
            }

            if (!foundDoor) {
                Target = Map.Instance.GetTile(Position + Vector2.Up);
            }
        }

        private void Chase() { }

        private void Scatter() {
            if (ScatterTarget != null)
                Target = ScatterTarget;
        }

        private void Frightened() { }

        private void Normal() { }
    }
}
