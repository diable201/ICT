using System;
using System.Collections.Generic;
using System.Text;

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
/*        public void Generate()
        {
            body[0].X = random.Next(1, 39);
            body[0].Y = random.Next(1, 39);
            Draw();
        }*/

        public void GenerateLocation(List<Point> wormBody, List<Point> wallBody)
        {
            body.Clear();
            Random random = new Random();

            Point p = new Point { X = random.Next(0, 39), Y = random.Next(0, 39) };

            while (!IsAvailablePoint(p, wormBody) || !IsAvailablePoint(p, wallBody))
            {
                p = new Point { X = random.Next(0, 39), Y = random.Next(0, 39) };
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
