using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.Snake
{
    public class Food
    {
        int x;
        int y;
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public Ellipse e = new Ellipse();
        Random random = new Random();

        public Food()
        {
            CreateFood();
        }

        public void CreateFood()
        {
            e.Width = Snake.sizeOfSnake;
            e.Height = Snake.sizeOfSnake;
            e.Fill = Brushes.Blue;
            int randomNumber_X = random.Next(0, 500); // 490 = (Width of Bildschirm - 10)
            int randomNumber_Y = random.Next(0, 360); // 340 = (Height of Bildschirm - 10)
            // TODO: Make sure the new random position of food ist not on top of the snake.
            X = randomNumber_X + (20 - (randomNumber_X % 20)) - 20;
            Y = randomNumber_Y + (20 - (randomNumber_Y % 20)) - 20;
            //do
            //{
            //    if ((X % 15) != 0)
            //        X++;
            //    if ((Y % 15) != 0)
            //        Y++;
            //} while ((X % 15) != 0 || (Y % 15) != 0);

            Console.WriteLine("food.X = " + X + " food.Y = " + Y);
            Canvas.SetLeft(e, X);
            Canvas.SetTop(e, Y);
        }
    }
}
