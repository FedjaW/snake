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

        readonly Random random = new ();

        public static Ellipse UIElement = new ()
        { 
            Width = Snake.SizeOfSnake,
            Height = Snake.SizeOfSnake,
            Fill = Brushes.Blue
        };

        public Food()
        {
            CreateFood();
        }

        public void CreateFood()
        {
            int randomNumber_X = random.Next(0, 500); // 490 = (width of screen - 10)
            int randomNumber_Y = random.Next(0, 360); // 340 = (height of screen - 10)

            X = randomNumber_X + (20 - (randomNumber_X % 20)) - 20;
            Y = randomNumber_Y + (20 - (randomNumber_Y % 20)) - 20;

            Canvas.SetLeft(UIElement, X);
            Canvas.SetTop(UIElement, Y);
        }
    }
}
