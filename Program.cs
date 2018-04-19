using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacMan {
    class Program {
        static void Main(string[] args) {

            new FrameBuffer(0, 0, 31, 150);

            while (RunGame()) {

            }
        }

        private static bool RunGame() {

            Game game = new Game();

            while (game.Update()) {

                Thread.Sleep(10);
            }

            return game.Restart;
        }
    }
}
