using System;
using System.Collections.Generic;

namespace Example1
{
    public abstract class GameObject
    {
        public char sign;
        public ConsoleColor color;
        public List<Point> body;

        public GameObject()
        {

        }

        public GameObject(char sign, ConsoleColor color)
        {
            this.sign = sign;
            this.color = color;
            this.body = new List<Point>();
        }

        protected void Draw()
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < body.Count; i++)
            {
                Console.SetCursorPosition(body[i].X, body[i].Y);
                Console.Write(sign);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < body.Count; i++)
            {
                Console.SetCursorPosition(body[i].X, body[i].Y);
                Console.Write(' ');
            }
        }

        public bool IsIntersected(List<Point> points)
        {
            bool res = false;
            foreach (Point p in points)
            {
                if (p.X == body[0].X && p.Y == body[0].Y)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
    }
}
