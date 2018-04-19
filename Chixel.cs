using System;

namespace PacMan {
    public class Chixel {

        public char Glyph;

        public ConsoleColor ForgroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public bool Dirty { get; set; }

        public Chixel(char glyph, ConsoleColor fgColor = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black) {
            Glyph = glyph;
            ForgroundColor = fgColor;
            BackgroundColor = bgColor;
            Dirty = true;
        }

        public Chixel(Chixel other) {
            Glyph = other.Glyph;
            ForgroundColor = other.ForgroundColor;
            BackgroundColor = other.BackgroundColor;
            Dirty = true;
        }
    }
}
