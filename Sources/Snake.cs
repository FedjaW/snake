using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake
{
    public class SnakeX
    {
        int x;  // x Position of snakehead
        int y;  // y Position of snakehead

        public static int lengthOfSnake = 5;
        public static int sizeOfSnake = 20;

        public Ellipse head;
        public List<Ellipse> body = new List<Ellipse>();
        // Brush snakeColour; // colour of snake

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

        public int[] _x = new int[lengthOfSnake + 500]; //TODO: Need to be dynamically resized.
        public int[] _y = new int[lengthOfSnake + 500]; // 450 punkte insgesamt

        public SnakeX()
        {

            // Set initial coordiantes.
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
                //ellipse.Fill = Brushes.Yellow;
                body.Add(ellipse);

                // Draw body at first on the same position as head
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
                // Draw first body part at old head position
                if (i == 0)
                {
                    _x[0] = X;
                    _y[0] = Y;
                    Canvas.SetLeft(body[0], _x[0]);
                    Canvas.SetTop(body[0], _y[0]);
                    body[0].Fill = Brushes.Black;
                }
                else // Draw rest of body
                {
                    _x[i] = _x[i - 1];
                    _y[i] = _y[i - 1];
                    Canvas.SetLeft(body[i], _x[i]);
                    Canvas.SetTop(body[i], _y[i]);
                    body[i].Fill = Brushes.Black;
                }
            }


            if (Settings.direction == Direction.Right)
                X += sizeOfSnake;
            else if (Settings.direction == Direction.Up)
                Y -= sizeOfSnake;
            else if (Settings.direction == Direction.Left)
                X -= sizeOfSnake;
            else if (Settings.direction == Direction.Down)
                Y += sizeOfSnake;

            // Set position of head relativ to Canvas.
            Canvas.SetLeft(head, X); // 180
            Canvas.SetTop(head, Y);  // 170
            // Fill the head of the snake with red colour.
            head.Fill = Brushes.Red;
        }


        public void ChangeDirection()
        {
            if (Keyboard.IsKeyDown(Key.Right) && (Settings.direction != Direction.Right) && (Settings.direction != Direction.Left))
                Settings.direction = Direction.Right;
            else if (Keyboard.IsKeyDown(Key.Left) && (Settings.direction != Direction.Left) && (Settings.direction != Direction.Right))
                Settings.direction = Direction.Left;
            else if (Keyboard.IsKeyDown(Key.Up) && (Settings.direction != Direction.Up) && (Settings.direction != Direction.Down))
                Settings.direction = Direction.Up;
            else if ((Keyboard.IsKeyDown(Key.Down) && Settings.direction != Direction.Down) && (Settings.direction != Direction.Up))
                Settings.direction = Direction.Down;
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
