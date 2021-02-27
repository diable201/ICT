using System;
using System.Collections.Generic;
using System.Text;

namespace Example1
{
    class Game
    {
        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }

        Worm worm = new Worm('*', ConsoleColor.Green);
        Food food = new Food('$', ConsoleColor.Red);
        Wall wall = new Wall('#', ConsoleColor.DarkBlue);
        public bool IsRunning { get; set; }
        public Game()
        {
            wall.LoadLevel();
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Width);
            Console.SetBufferSize(40, 40);

        }

        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    worm.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    worm.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    worm.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    worm.Move(1, 0);
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    break;
            }

            if (worm.IsIntersected(food.body))
            {
                worm.Increase(worm.body[0]);
                food.GenerateLocation(worm.body, wall.body);
            }

            if (worm.IsIntersected(wall.body))
            {
                Console.Clear();
                while (pressedKey.Key != ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(15, 20);
                    Console.Write("Game over ;(");
                }
            }
            if (worm.LengthOfWorm == 5)
            {
                wall.NextLevel();
            }
        }
    }
}
