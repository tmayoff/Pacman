using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Ghosts {
    public class Blinky : Ghost {

        public Blinky(Chixel chixel, Vector2 pos) {
            Chixel = chixel;
            Position = pos;
            Velocity = Vector2.Left;

            Game.Instance.Characters.Add(this);
        }

        public override void Update() {
            base.Update();


            Target = Map.Instance.GetTile(new Vector2(3, 0));
            
            //Next tile
            NextTile = Map.Instance.GetTile(Position + Velocity);
            if (NextTile.Type == TileType.Wall)
                Velocity = Vector2.Zero;
            
            //Current tile
            CurrentTile = Map.Instance.GetTile(Position);
            if (CurrentTile.Intersection) {
                Velocity = Vector2.Zero;
            }

            Position += Velocity;

            FrameBuffer.Instance.SetChixel(Position, Chixel, FrameBuffer.BufferLayers.Characters);
        }
    }
}
