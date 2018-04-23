
using System.Collections.Generic;

namespace PacMan {
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

            if (Target != null)
                Target.Chixel.BackgroundColor = Chixel.ForgroundColor;
        }

        private void Chase() { }

        private void Scatter() { }

        private void Frightened() { }

        private void Normal() { }
    }
}
