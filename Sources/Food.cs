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
            int randomNumber_X = random.Next(0, 500); // 490 = (width of screen - 10)
            int randomNumber_Y = random.Next(0, 360); // 340 = (height of screen - 10)

            X = randomNumber_X + (20 - (randomNumber_X % 20)) - 20;
            Y = randomNumber_Y + (20 - (randomNumber_Y % 20)) - 20;

            Canvas.SetLeft(e, X);
            Canvas.SetTop(e, Y);
        }
    }
}
