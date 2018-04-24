using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Ghosts {
    public class Inky : Ghost {


        public Inky(Chixel chixel, Vector2 pos) {
            Chixel = chixel;
            Position = pos;
            Velocity = Vector2.Up;

            Game.Instance.Characters.Add(this);

            ScatterTarget = Map.Instance.GetTile(new Vector2(Map.Instance.MapSize.X - 4, Map.Instance.MapSize.Y - 2));
        }

        public override void Update() {
            base.Update();

            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
