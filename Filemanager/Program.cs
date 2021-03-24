using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager
{
    internal class Layer
    {
        private DirectoryInfo Dir { get; }

        private int Pos { get; set; }

        private List<FileSystemInfo> Content { get; }

        public Layer(DirectoryInfo dir, int pos)
        {
            this.Dir = dir;
            this.Pos = pos;
            this.Content = new List<FileSystemInfo>();
            Content.AddRange(dir.GetDirectories());
            Content.AddRange(dir.GetFiles());
        }

        
        public void OpenFile()      
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            var txt = File.ReadAllText(Content[Pos].FullName);
            Console.WriteLine(txt);
            Console.ReadKey();
        }

        public void Delete() 
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Content[Pos].Delete();
            Console.WriteLine("Successfully deleted!");
            Console.ReadKey();
        }

        public void Write()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Input: ");
            var text = Console.ReadLine();
            using StreamWriter sw = new StreamWriter(Content[Pos].FullName);
            sw.WriteLine(text);
        }

        public void Append()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Append: ");
            var text = Console.ReadLine();
            using StreamWriter sw = File.AppendText(Content[Pos].FullName);
            sw.WriteLine(text);
        }

        public void Rename()
        {
            var directoryInfo = new DirectoryInfo(Content[Pos].FullName).Parent;
            if (directoryInfo == null) return;
            var parent = directoryInfo.FullName;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Write new name: ");
            var name = Console.ReadLine();
            if (Content[Pos] is DirectoryInfo)
            {
                Directory.Move(Content[Pos].FullName, parent + '/' + name);
            }
            else
            {
                File.Move(Content[Pos].FullName, parent + '/' + name);
            }
        }

        public static void CreateFolder()
        {
            const string path = "";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Write new name for folder: ");
            var name = Console.ReadLine();
            var folder = Path.Combine(path, name ?? string.Empty);
            Directory.CreateDirectory(folder);
        }

        public static void CreateFile()
        {
            const string path = "";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Write new name for file: ");
            var filename = Console.ReadLine();
            var file = Path.Combine(path, filename ?? string.Empty);
            File.Create(file).Dispose();
        }

        public void GetSize()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            FileInfo fi = new FileInfo(Content[Pos].FullName);
            var size = fi.Length;
            Console.WriteLine("File Size in Bytes: {0}", size);
        }

        private static long DirSize(DirectoryInfo d)
        {
            FileInfo[] fis = d.GetFiles();
            var size = fis.Sum(fi => fi.Length);
            DirectoryInfo[] dis = d.GetDirectories();
            size += dis.Sum(DirSize);
            return size;
        }

        public void PrintInfo()
        {
            const string path = "";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome To File Manager!");
            Console.WriteLine("Open: Enter | Rename: TAB | Delete: D | Write: W | Append: A | Back: BackSpace | Close: ESC");
            Console.WriteLine("Create File: Y | Create Folder: T");
            var fCount = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
            var dCount = Directory.GetDirectories(path, "*", SearchOption.AllDirectories).Length;
            Console.WriteLine("Date: " + DateTime.Now);
            Console.WriteLine("Number of Files " + fCount);
            Console.WriteLine("Number of Directories " + dCount);
            Console.WriteLine("List of files:\n");
           
            var cnt = 0;
            
            foreach (DirectoryInfo d in Dir.GetDirectories())
            {
                if (cnt == Pos)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(d.Name + " Size: " + DirSize(d) + " Bytes");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(d.Name);
                }              
                cnt++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            foreach (FileInfo f in Dir.GetFiles())
            {
                if (cnt == Pos)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(f.Name + " Size: " + f.Length + " Bytes");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(f.Name);
                }
                
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
            }
            else if (Pos < 0)
            {
                Pos = Content.Count - 1;
            }
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            ManagerStart();
        }

        private static void ManagerStart()
        {
            Stack<Layer> history = new Stack<Layer>();
            history.Push(new Layer(new DirectoryInfo
                (""), 0));
            Console.CursorVisible = false;
            var escape = false;
            
            while (!escape)
            {
                Console.Clear();
                try
                {         
                    history.Peek().PrintInfo();
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;                            
                            if (history.Peek().GetCurrentObjet() is DirectoryInfo)
                            {
                                history.Push(new Layer(history.Peek().GetCurrentObjet()
                                    as DirectoryInfo, 0));                              
                            }
                            else
                            {
                                history.Peek().OpenFile();
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            history.Peek().SetNewPosition(-1);
                            break;
                        case ConsoleKey.DownArrow:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            history.Peek().SetNewPosition(1);
                            break;
                        case ConsoleKey.D:
                            history.Peek().Delete();
                            break;
                        case ConsoleKey.Tab:
                            history.Peek().Rename();
                            history.Push(new Layer(new DirectoryInfo
                                (""), 0));
                            break;
                        case ConsoleKey.Backspace:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            history.Pop();
                            break;
                        case ConsoleKey.W:
                            history.Peek().Write();
                            break;
                        case ConsoleKey.A:
                            history.Peek().Append();
                            break;                       
                        case ConsoleKey.T:
                            Layer.CreateFolder();
                            history.Push(new Layer(new DirectoryInfo
                                (""), 0));
                            break;
                        case ConsoleKey.Y:
                            Layer.CreateFile();
                            history.Push(new Layer(new DirectoryInfo
                                (""), 0));
                            break;
                        case ConsoleKey.Escape:
                            escape = true;
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Good Bye!");
                            break;
                    }
                } 
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;                    
                    history.Push(new Layer(new DirectoryInfo
                        (""), 0));
                }
            }
        }
    }
}

