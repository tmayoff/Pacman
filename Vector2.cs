using System;

namespace PacMan {
    public class Vector2 {

        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 Zero => new Vector2(0, 0);
        public static Vector2 Up => new Vector2(0, -1);
        public static Vector2 Down => new Vector2(0, 1);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Right => new Vector2(1, 0);

        public int X;
        public int Y;

        public Vector2 Opposite => new Vector2(-X, -Y);

        public Vector2(int x, int y) {
            X = x;
            Y = y;
        }

        public static double Distance(Vector2 a, Vector2 b) {
            a += One;
            b += One;
            double x = Math.Abs((double)b.X - a.X);
            double y = Math.Abs((double)b.Y - a.Y);
            return Math.Sqrt(x + y);
        }

        public override string ToString() {
            return "(" + X + "," + Y + ")";
        }

        public static Vector2 operator *(Vector2 a, int b) {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static bool operator ==(Vector2 a, Vector2 b) {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector2 a, Vector2 b) {
            return a.X != b.X || a.Y != b.Y;
        }
    }
}