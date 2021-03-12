using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Example1
{
    public class Wall : GameObject
    {
        enum GameLevel
        {
            FIRST,
            SECOND
        }
        GameLevel gameLevel = GameLevel.FIRST;

        public Wall() : base()
        {

        }

        public Wall(char sign, ConsoleColor color) : base(sign, color)
        {
            body = new List<Point>();
        }

        public void LoadLevel()
        {
            body = new List<Point>();
            string levelName = @"Levels/Level1.txt";
            if (gameLevel == GameLevel.SECOND)
            {
                levelName = @"Levels/Level2.txt";
            }  
            using (FileStream fs = new FileStream(levelName, FileMode.Open, FileAccess.Read))
            {
                using StreamReader reader = new StreamReader(fs);
                int rowNumber = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    for (int columnNumber = 0; columnNumber < line.Length; columnNumber++)
                    {
                        if (line[columnNumber] == '#')
                        {
                            body.Add(new Point { X = columnNumber, Y = rowNumber });
                        }
                    }
                    rowNumber++;
                }

            }
            Draw();
        }
        public void NextLevel()
        {
            
            if (gameLevel == GameLevel.FIRST)
            {
                gameLevel = GameLevel.SECOND;
            }
            LoadLevel();
        }

        public void Save(string title)
        {
            if (File.Exists("wall.xml"))
            {
                File.Delete("wall.xml");
            }
            using FileStream fs = new FileStream(title + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Wall));
            xmlSerializer.Serialize(fs, this);
        }

        public static Wall Load(string title)
        {
            using FileStream fs = new FileStream(title + ".xml", FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Wall));
            Wall save = xmlSerializer.Deserialize(fs) as Wall;
            return save;
        }
    }
}
