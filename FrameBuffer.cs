using System;
using System.Threading;

namespace PacMan {

    public class FrameBuffer {

        public enum BufferLayers { Obstacles, Characters }

        public static FrameBuffer Instance {
            get { return _instance; }
        }

        private static FrameBuffer _instance;

        public int Height;
        public int Width;

        private readonly int _top;
        private readonly int _left;

        private Chixel[][,] _chixels;

        private int layerCount = Enum.GetNames(typeof(BufferLayers)).Length;

        public FrameBuffer(int top, int left, int height, int width) {
            _instance = this;

            Height = height;
            Width = width;
            _left = left;
            _top = top;

            Clear();
        }

        public void Clear() {
            Console.Clear();

            _chixels = new Chixel[layerCount][,];
            for (int i = 0; i < _chixels.Length; i++) {
                _chixels[i] = new Chixel[Width, Height];
            }
        }

        public void DrawFrame() {

            for (int i = 0; i < _chixels.Length; i++) {


                for (int x = 0; x < Width; x++) {
                    //if (x < 45) continue;
                    if (x >= Console.WindowWidth)
                        continue;

                    for (int y = 0; y < Height; y++) {
                        if (y >= Console.WindowHeight)
                            continue;

                        Chixel ch = _chixels[i][x, y];

                        if (ch != null && ch.Dirty) {
                            Console.SetCursorPosition(x + _left, y + _top);
                            ConsoleColor lastFGColor = ch.ForgroundColor;
                            ConsoleColor lastBGColor = ch.BackgroundColor;

                            if (Console.BackgroundColor != lastBGColor)
                                Console.BackgroundColor = ch.BackgroundColor;

                            if (Console.ForegroundColor != lastFGColor)
                                Console.ForegroundColor = ch.ForgroundColor;

                            Console.Write(ch.Glyph);
                            ch.Dirty = false;

                            //Thread.Sleep(50);
                        }
                    }
                }
            }
        }

        public void RemoveChixel(Vector2 pos, BufferLayers layer) {
            SetChixel(pos, '\0', layer);
        }

        public Chixel GetChixel(Vector2 pos, BufferLayers layer) {
            pos.X -= _left;
            pos.Y -= _top;
            if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height) {
                //throw new Exception();
                return null;
            }

            return _chixels[(int)layer][pos.X, pos.Y];
        }

        public void SetChixel(Vector2 pos, Chixel chixel, BufferLayers layer) {
            SetChixel(pos, chixel.Glyph, layer, chixel.ForgroundColor, chixel.BackgroundColor);
        }

        public void SetChixel(Vector2 pos, char c, BufferLayers layer, ConsoleColor fgColor = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black) {
            pos.X -= _left;
            pos.Y -= _top;

            if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height)
                return;

            Chixel ch = _chixels[(int)layer][pos.X, pos.Y];
            if (ch != null && ch.Glyph == c && ch.ForgroundColor == fgColor && ch.BackgroundColor == bgColor)
                return;

            _chixels[(int)layer][pos.X, pos.Y] = new Chixel(c, fgColor, bgColor);
        }

        public void Write(Vector2 pos, string s, BufferLayers layer, ConsoleColor fgColor = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black) {
            int initX = pos.X;

            for (int i = 0; i < s.Length; i++) {
                if (s[i] == '\n') {
                    pos.X = initX;
                    pos.Y++;
                    continue;
                }

                SetChixel(pos, s[i], layer, fgColor, bgColor);
                pos.X++;
            }
        }
    }
}
