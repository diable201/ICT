using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Example1
{
    public class Worm : GameObject
    {
        public int lengthOfWorm;
        public int pointsOfWorm;

        public int Dx { get; set; }
        public int Dy { get; set; }

        public Worm() : base()
        {

        }

        public Worm(char sign, ConsoleColor color) : base(sign, color)
        {
            Point head = new Point { X = 20, Y = 20 };
            body = new List<Point>();
            body.Add(head);
            Draw();
            lengthOfWorm = 1;
            pointsOfWorm = 0;
        }

        public void ChangeDirection(int dx, int dy)
        {
            this.Dx = dx;
            this.Dy = dy;
        }

        public void Move()
        {
            Clear();
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].X = body[i - 1].X;
                body[i].Y = body[i - 1].Y;
            }

            body[0].X += Dx;
            body[0].Y += Dy;
            Draw();
        }

        public void Increase(Point point)
        {
            body.Add(new Point { X = point.X, Y = point.Y });
            lengthOfWorm++;
            pointsOfWorm++;
        }

        public int LengthOfWorm
        {
            get
            {
                return lengthOfWorm;
            }
        }

        public int CountOfPoints
        {
            get
            {
                return pointsOfWorm;
            }
        }

        public void Save(string title)
        {
            if (File.Exists("save.xml"))
            {
                File.Delete("save.xml");
            }
            using FileStream fs = new FileStream(title + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worm));
            xmlSerializer.Serialize(fs, this);
        }

        public static Worm Load(string title)
        {
            using FileStream fs = new FileStream(title + ".xml", FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worm));
            Worm save = xmlSerializer.Deserialize(fs) as Worm;
            return save;
        }
    }
}
