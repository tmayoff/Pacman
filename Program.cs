using System;
using System.Threading;

namespace PacMan {
    class Program {
        static void Main(string[] args) {
            Console.Title = "Pacman";

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
