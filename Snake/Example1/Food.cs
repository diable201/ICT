using System;
using System.Collections.Generic;

namespace Example1
{
    class Food : GameObject
    {
        readonly Random random = new Random();
        public Food(char sign, ConsoleColor color) : base(sign, color)
        {
            Point location = new Point { X = random.Next(1, Game.Width), Y = random.Next(1, Game.Height) };
            body.Add(location);
            Draw();
        }

        public void GenerateLocation(List<Point> wormBody, List<Point> wallBody)
        {
            body.Clear();
            Random random = new Random();

            Point p = new Point { X = random.Next(1, 38), Y = random.Next(1, 38) };

            while (!IsAvailablePoint(p, wormBody) || !IsAvailablePoint(p, wallBody))
            {
                p = new Point { X = random.Next(1, 38), Y = random.Next(1, 38) };
            }
            body.Add(p);
            Draw();
        }

        bool IsAvailablePoint(Point p, List<Point> points)
        {
            bool res = true;

            foreach (Point t in points)
            {
                if (p.X == t.X || p.Y == t.Y)
                {
                    res = false;
                    break;
                }
            }

            return res;
        }
    }
}
