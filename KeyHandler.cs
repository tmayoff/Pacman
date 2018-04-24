using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan {
    public class KeyHandler {

        public static void UpdateKeys() {
            if (!Console.KeyAvailable) return;
            ConsoleKeyInfo key = Console.ReadKey(true);

            //Player Movement
            switch (key.Key) {
                case ConsoleKey.UpArrow:
                    Pacman.Instance.Velocity = Vector2.Up;
                    break;
                case ConsoleKey.DownArrow:
                    Pacman.Instance.Velocity = Vector2.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    Pacman.Instance.Velocity = Vector2.Left;
                    break;
                case ConsoleKey.RightArrow:
                    Pacman.Instance.Velocity = Vector2.Right;
                    break;
            }


            //Reset all keys
            while (Console.KeyAvailable) {
                Console.ReadKey(true);
            }
        }
    }
}
