using System;
using System.Timers;

namespace Example1
{
    class Game
    {
        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }

        Worm worm = new Worm('*', ConsoleColor.Green);
        Wall wall = new Wall('#', ConsoleColor.Blue);
        Food food = new Food('$', ConsoleColor.Red);
        Timer wormTimer = new Timer(150);
        Timer wallTimer = new Timer(150);
        Timer gameTimer = new Timer(1000);
        bool pause;
        public bool IsRunning { get; set; }

       
        public Game()
        {
            wormTimer.Elapsed += MoveWorm;
            wormTimer.Start();
            gameTimer.Elapsed += GameTimerElapsed;
            gameTimer.Start();           
            wallTimer.Start();
            wall.LoadLevel();
            pause = false;
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Width);
            Console.SetBufferSize(40, 40);     
        }

        private void GameTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = DateTime.Now.ToLongTimeString() + " Scores: " + worm.CountOfPoints;
            //Console.Title = ("Eatten Food: " + worm.CountOfPoints);
        }

        public void UpdatePoints(object sender, ElapsedEventArgs e)
        {    
            Console.SetCursorPosition(25, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Eatten Food: {0}", worm.CountOfPoints, Console.ForegroundColor);
        }

        void MoveWorm(object sender, ElapsedEventArgs e)
        {
            worm.Move();

            if (worm.IsIntersected(wall.body))
            {
                Console.Clear();
                Console.SetCursorPosition(15, 20);
                Console.Write("Game over ;(");
                Console.SetCursorPosition(14, 23);
                Console.Write("Your Scores: {0}", worm.CountOfPoints);
                Console.SetCursorPosition(10, 26);
                Console.Write("Load Saved Game? Press L");
                wormTimer.Stop();
                pause = true;
            }

            if (worm.IsIntersected(food.body))
            {
                worm.Increase(worm.body[0]);
                food.GenerateLocation(worm.body, wall.body);
            }

            if (worm.LengthOfWorm == 4)
            {
                //food.Clear();
                //food = new Food('$', ConsoleColor.Yellow);
                wall.NextLevel();
            }
        }


        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    worm.ChangeDirection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    worm.ChangeDirection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    worm.ChangeDirection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    worm.ChangeDirection(1, 0);
                    break;
                case ConsoleKey.S:
                    worm.Save("save");
                    wall.Save("wall");
                    break;
                case ConsoleKey.L:
                    Console.Clear();
                    wormTimer.Stop();
                    wallTimer.Stop();
                    wall.Clear();
                    worm.Clear();
                    food.Clear();
                    food = new Food('$', ConsoleColor.Red);
                    worm = Worm.Load("save");
                    wall = Wall.Load("wall");
                    if (worm.CountOfPoints >= 3)
                    {
                        wall.NextLevel();
                    }            
                    else
                    {
                        wall.LoadLevel();
                    }
                    wormTimer.Start();
                    wallTimer.Start();
                    break;
                case ConsoleKey.Spacebar:
                    if (!pause)
                    {
                        wormTimer.Stop();
                        pause = true;
                    }
                    else
                    {
                        wormTimer.Start();
                        pause = false;
                    }
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    break;
            }           
        }
    }
}
