using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game.Snake
{
    public class Food
    {
        public int X { get; set; }
        public int Y { get; set; }

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
            int randomNumber_X = random.Next(0, 500);
            int randomNumber_Y = random.Next(0, 360);

            // generate a step function
            // plot f(x) to see the output (e.g. here: https://www.wolframalpha.com/)
            // f(x) = x - (x mod 20)
            X = randomNumber_X - (randomNumber_X % Snake.SizeOfSnake);
            Y = randomNumber_Y - (randomNumber_Y % Snake.SizeOfSnake);

            Canvas.SetLeft(UIElement, X);
            Canvas.SetTop(UIElement, Y);
        }
    }
}
