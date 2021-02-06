using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    internal class Layer
    {
        public DirectoryInfo Dir
        {
            get;
            set;
        }

        public int Pos
        {
            get;
            set;
        }

        public List<FileSystemInfo> Content
        {
            get;
            set;
        }

        public Layer(DirectoryInfo dir, int pos)
        {
            this.Dir = dir;
            this.Pos = pos;
            this.Content = new List<FileSystemInfo>();
            Content.AddRange(dir.GetDirectories());
            Content.AddRange(dir.GetFiles());
        }

        public void PrintInfo()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            int cnt = 0;
            foreach (DirectoryInfo d in Dir.GetDirectories())
            {
                if (cnt == Pos)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }

                Console.WriteLine(d.Name);
                cnt++;
            }
            Console.ForegroundColor = ConsoleColor.Black;
            foreach (FileInfo f in Dir.GetFiles())
            {
                if (cnt == Pos)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }

                Console.WriteLine(f.Name);
                cnt++;
            }
        }

        public FileSystemInfo GetCurrentObjet()
        {
            return Content[Pos];
        }

        public void SetNewPosition(int d)
        {
            if (d > 0)
            {
                Pos++;
            }
            else
            {
                Pos--;
            }
            
            if (Pos >= Content.Count)
            {
                Pos = 0;
            } else if (Pos < 0)
            {
                Pos = Content.Count - 1;
            }
        }
    }

    internal static class Program
    {
        private static void Main(string[] args)
        {
            F3();
        }

        private static void PrintFolderInfo(string path, int pos)
        {
            // Console.BackgroundColor = ConsoleColor.Blue;
            DirectoryInfo dir = new DirectoryInfo(path);
            Console.ForegroundColor = ConsoleColor.White;
            int cnt = 0;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (cnt == pos)
                    Console.BackgroundColor = ConsoleColor.Cyan;
                else
                    Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(d.Name);
                cnt++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (FileInfo d in dir.GetFiles())
            {
                Console.WriteLine(d.Name);
            }
        }
        
        private static void F3()
        {
            Stack<Layer> history = new Stack<Layer>();
            history.Push(new Layer(new DirectoryInfo
                ("/Users/sanzhar/Documents/"),0));
            // int pos = 0;
            bool escape = false;

            // DirectoryInfo dir = new DirectoryInfo("/Users/sanzhar/Downloads/");
            // List<FileSystemInfo> fileSystemInfo = new List<FileSystemInfo>();
            // PrintFolderInfo("/Users/sanzhar/Downloads/");    
            
            // fileSystemInfo.AddRange(history.Peek().dir.GetDirectories());
            // fileSystemInfo.AddRange(history.Peek().dir.GetFiles());
            
            while (!escape)
            {
                Console.Clear();
                // PrintFolderInfo(history.Peek().dir.FullName, pos);
                history.Peek().PrintInfo();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (history.Peek().GetCurrentObjet() is DirectoryInfo)
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObjet() 
                                as DirectoryInfo, 0));
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        history.Peek().SetNewPosition(-1);
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().SetNewPosition(1);
                        break;
                    case ConsoleKey.Escape:
                        escape = true;
                        break;
                }
                // if (consoleKeyInfo.Key == ConsoleKey.Escape) break;
                // Console.WriteLine(consoleKeyInfo.KeyChar);    
            }
        }

        private static void F2()
        {
            while (true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.Key == ConsoleKey.Escape) break;
                Console.WriteLine(consoleKeyInfo.KeyChar);    
            }
        }

        private static void F1()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.WriteLine(consoleKeyInfo);
        }
        
    }
}