using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Snake_Game
{

    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    class Game
    {
        const string path = "snake.txt";
        static readonly int X = 80;
        static readonly int Y = 26;


        static Walls walls;
        static FoodFactory foodFactory;
        static Snake snake;
        static Timer time;

        static void Main(string[] args)
        {

            Console.SetWindowSize(X + 10, Y + 10);
            Console.SetBufferSize(X + 10, Y + 10);

            Console.CursorVisible = false;

            walls = new Walls(X, Y - 1, '#');

            foodFactory = new FoodFactory(X, Y, '@');
            foodFactory.CreateFood();

            snake = new Snake(X / 2, (Y - 1) / 2, 3);
            time = new Timer(Loop, null, 0, 200);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.Rotation(key.Key);
                }
            }


        }
        static void Loop(object obj)
        {
            if (walls.IsHit(snake.GetHead()) || snake.IsHit(snake.GetHead()))
            {
                time.Change(0, Timeout.Infinite);
                Console.SetCursorPosition(X / 2, Y / 2);

                Console.Write("Game over");
               


            }
            else if (snake.Eat(foodFactory.food))
            {
                foodFactory.CreateFood();

            }
            else
            {
                snake.Move();
            }
            Console.SetCursorPosition(1, 30);
            Console.Write(snake.score);
        }
       

    }

    struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char ch { get; set; }

        public static implicit operator Point((int, int, char) value) =>
            new Point { x = value.Item1, y = value.Item2, ch = value.Item3 };

        public static bool operator ==(Point a, Point b) =>
            (a.x == b.x && a.y == b.y) ? true : false;
        public static bool operator !=(Point a, Point b) =>
            (a.x != b.x || a.y != b.y) ? true : false;

        public void Draw()
        {
            DrawPoint(ch);
        }
        public void Clear()
        {
            DrawPoint(' ');
        }
        private void DrawPoint(char _ch)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(_ch);
        }
    }
}
