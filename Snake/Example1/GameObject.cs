using System;
using System.Collections.Generic;
using System.Linq;

namespace Example1
{
    public abstract class GameObject
    {
        private readonly char _sign;
        private readonly ConsoleColor _color;
        public List<Point> Body;

        protected GameObject()
        {

        }

        protected GameObject(char sign, ConsoleColor color)
        {
            this._sign = sign;
            this._color = color;
            this.Body = new List<Point>();
        }

        protected void Draw()
        {
            Console.ForegroundColor = _color;
            foreach (var t in Body)
            {
                Console.SetCursorPosition(t.X, t.Y);
                Console.Write(_sign);
            }
        }

        public void Clear()
        {
            foreach (var t in Body)
            {
                Console.SetCursorPosition(t.X, t.Y);
                Console.Write(" ");
            }
        }

        public bool IsIntersected(IEnumerable<Point> points)
        {
            return points.Any(p => p.X == Body[0].X && p.Y == Body[0].Y);
        }
    }
}

