using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan {
    public class Character {

        public Chixel Chixel;

        public Vector2 Position;

        public Character() {
            Game.Instance.Characters.Add(this);
        }

        public Character(Chixel chixel, Vector2 pos) {
            Chixel = chixel;
            Position = pos;
            Game.Instance.Characters.Add(this);
        }

        public virtual void Update() {
            FrameBuffer.Instance.RemoveChixel(Position, FrameBuffer.BufferLayers.Characters);
        }
    }
}
