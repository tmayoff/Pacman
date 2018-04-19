using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan {
    public class Ghost : Character {

        public Ghost(Chixel chixel, Vector2 pos) {
            Game.Instance.Characters.Add(this);
            Chixel = chixel;
            Position = pos;
        }

        public override void Update() {
            base.Update();

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
