using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.Snake
{
    public class Snake
    {
        public Ellipse head;
        public List<Ellipse> body = new();

        public static int LengthOfSnake { get; set; } = 5;
        public static int SizeOfSnake { get; set; } = 20;

        public int X { get; set; }
        public int Y { get; set; }

        public int[] _x = new int[LengthOfSnake + 500];
        public int[] _y = new int[LengthOfSnake + 500];

        public static Direction Direction { get; set; }

        public Snake()
        {
            // set initial coordiantes
            X = 180;
            Y = 160;

            // create head of snake
            head = new Ellipse
            {
                Width = SizeOfSnake,
                Height = SizeOfSnake,
                Fill = Brushes.Red
            };

            // initial position of Snake
            Canvas.SetLeft(head, X); // 180
            Canvas.SetTop(head, Y);  // 170

            // create body of snake
            for (int i = 0; i < LengthOfSnake; i++)
            {
                var ellipse = new Ellipse()
                {
                    Width = SizeOfSnake,
                    Height = SizeOfSnake
                    // Fill = Brushes.Yellow;
                };
                body.Add(ellipse);

                // draw body at first on the same position as head
                _x[i] = X;
                _y[i] = Y;
                Canvas.SetLeft(body[i], X);
                Canvas.SetTop(body[i], Y);
            }
        }


        public void MoveForward()
        {
            // body
            for (int i = (LengthOfSnake - 1); i >= 0; i--)
            {
                // draw first body part at old head position
                if (i == 0)
                {
                    _x[0] = X;
                    _y[0] = Y;
                    Canvas.SetLeft(body[0], _x[0]);
                    Canvas.SetTop(body[0], _y[0]);
                    body[0].Fill = Brushes.Black;
                }
                else // draw rest of body
                {
                    _x[i] = _x[i - 1];
                    _y[i] = _y[i - 1];
                    Canvas.SetLeft(body[i], _x[i]);
                    Canvas.SetTop(body[i], _y[i]);
                    body[i].Fill = Brushes.Black;
                }
            }


            if (Direction == Direction.Right)
                X += SizeOfSnake;
            else if (Direction == Direction.Up)
                Y -= SizeOfSnake;
            else if (Direction == Direction.Left)
                X -= SizeOfSnake;
            else if (Direction == Direction.Down)
                Y += SizeOfSnake;

            // set position of head relativ to canvas
            Canvas.SetLeft(head, X); // 180
            Canvas.SetTop(head, Y);  // 170

            // fill the head of the snake with red colour
            head.Fill = Brushes.Red;
        }


        public static void ChangeDirection()
        {
            if (Keyboard.IsKeyDown(Key.Right) && (Direction != Direction.Right) && (Direction != Direction.Left))
            {
                Direction = Direction.Right;
            }
            else if (Keyboard.IsKeyDown(Key.Left) && (Direction != Direction.Left) && (Direction != Direction.Right))
            {
                Direction = Direction.Left;
            }
            else if (Keyboard.IsKeyDown(Key.Up) && (Direction != Direction.Up) && (Direction != Direction.Down))
            {
                Direction = Direction.Up;
            }
            else if ((Keyboard.IsKeyDown(Key.Down) && Direction != Direction.Down) && (Direction != Direction.Up))
            {
                Direction = Direction.Down;
            }
        }


        public void EatAndGrow()
        {
            var ellipse = new Ellipse
            {
                Width = SizeOfSnake,
                Height = SizeOfSnake
            };

            body.Add(ellipse);

            LengthOfSnake++;
        }
    }
}
