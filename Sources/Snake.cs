using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.Snake
{
    public class Snake
    {
        int x;  // x Position of snakehead
        int y;  // y Position of snakehead

        public static int lengthOfSnake = 5;
        public static int sizeOfSnake = 20;

        public Ellipse head;
        public List<Ellipse> body = new();
        // brush snakeColour; // color of snake

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

        public int[] _x = new int[lengthOfSnake + 500];
        public int[] _y = new int[lengthOfSnake + 500];

        public static Direction direction { get; set; }

        public Snake()
        {

            // set initial coordiantes
            X = 180;
            Y = 160;

            // create head of snake
            head = new Ellipse();
            head.Width = sizeOfSnake;
            head.Height = sizeOfSnake;
            head.Fill = Brushes.Red;

            // initial position of Snake
            Canvas.SetLeft(head, X); // 180
            Canvas.SetTop(head, Y);  // 170

            // create body of snake
            for (int i = 0; i < lengthOfSnake; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = sizeOfSnake;
                ellipse.Height = sizeOfSnake;
                // ellipse.Fill = Brushes.Yellow;
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
            for (int i = (lengthOfSnake - 1); i >= 0; i--)
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


            if (direction == Direction.Right)
                X += sizeOfSnake;
            else if (direction == Direction.Up)
                Y -= sizeOfSnake;
            else if (direction == Direction.Left)
                X -= sizeOfSnake;
            else if (direction == Direction.Down)
                Y += sizeOfSnake;

            // set position of head relativ to canvas
            Canvas.SetLeft(head, X); // 180
            Canvas.SetTop(head, Y);  // 170

            // fill the head of the snake with red colour
            head.Fill = Brushes.Red;
        }


        public void ChangeDirection()
        {
            if (Keyboard.IsKeyDown(Key.Right) && (direction != Direction.Right) && (direction != Direction.Left))
                direction = Direction.Right;
            else if (Keyboard.IsKeyDown(Key.Left) && (direction != Direction.Left) && (direction != Direction.Right))
                direction = Direction.Left;
            else if (Keyboard.IsKeyDown(Key.Up) && (direction != Direction.Up) && (direction != Direction.Down))
                direction = Direction.Up;
            else if ((Keyboard.IsKeyDown(Key.Down) && direction != Direction.Down) && (direction != Direction.Up))
                direction = Direction.Down;
        }


        public void EatAndGrow()
        {
            Ellipse ellipse = new Ellipse();

            ellipse.Width = sizeOfSnake;
            ellipse.Height = sizeOfSnake;

            body.Add(ellipse);

            lengthOfSnake++;
        }
    }
}
