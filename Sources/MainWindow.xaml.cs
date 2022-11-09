using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game.Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int score = 0;
        int maxScore = 0;

        Snake snake = new ();
        DispatcherTimer timer = new ();
        DispatcherTimer timer2 = new ();
        Food food = new ();

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1); // speed of snake
            timer.Tick += UpdateScreen;

            timer2.Interval = TimeSpan.FromSeconds(0.001); // update time of Input
            timer2.Tick += UpdateInput;

            Screen.Children.Add(Food.UIElement); // add food

            for (int i = 0; i < Snake.LengthOfSnake; i++) // add snake body
            {
                Screen.Children.Add(snake.body[i]);
            }

            // add snakehead. Execute behind Add(body) because of layered presentation
            // so the red head will always be in the foreground
            Screen.Children.Add(snake.head);
            timer.Start();
            timer2.Start();
        }

        private void UpdateInput(object sender, EventArgs e)
        {
            Snake.ChangeDirection();
        }

        bool createNewFood;
        private void UpdateScreen(object sender, EventArgs e)
        {
            snake.MoveForward();

            // game over when colliding with the edge of the screen
            if (snake.X < 0 || snake.X >= Screen.ActualWidth || snake.Y >= Screen.ActualHeight || snake.Y < 0)
            {
                Restart();
            }

            // game over when colliding with the snake body
            for (int i = 0; i < Snake.LengthOfSnake; i++)
            {
                if (snake.X == snake._x[i] && snake.Y == snake._y[i])
                {
                    Restart();
                }
            }

            // snake eats food
            if (snake.X == food.X && snake.Y == food.Y)
            {
                snake.EatAndGrow();
                Screen.Children.Add(snake.body[Snake.LengthOfSnake - 1]); // add new bodypart of snake to the snake

                do
                {
                    food.CreateFood(); // create new food at a random place
                    for (int i = 0; i < Snake.LengthOfSnake; i++)
                    {
                        if ((food.X == snake._x[i] && food.Y == snake._y[i]) || (food.X == snake.X && food.Y == snake.Y))
                        {
                            createNewFood = true;
                            break;
                        }
                        else
                            createNewFood = false;
                    }
                } while (createNewFood);


                // update score
                score += 10;
                lblScore.Content = score.ToString();
                if (score >= 4500)
                {
                    lblScore.Content = "WON!";
                    Restart();
                }

            }
        }

        public void Restart()
        {
            if (score > maxScore)
            {
                maxScore = score;
                lblMaxScore.Content = maxScore.ToString();
            }

            score = 0;
            lblScore.Content = score.ToString();
            Screen.Children.Clear();
            snake.body.Clear();

            Snake.LengthOfSnake = 5;
            snake.X = 180;
            snake.Y = 160;

            // create body of snake
            for (int i = 0; i < Snake.LengthOfSnake; i++)
            {
                Ellipse ellipse = new ()
                {
                    Width = Snake.SizeOfSnake,
                    Height = Snake.SizeOfSnake
                    // Fill = Brushes.Yellow;
                };
                snake.body.Add(ellipse);

                // draw body at first on the same position as head
                snake._x[i] = snake.X;
                snake._y[i] = snake.Y;
                Canvas.SetLeft(snake.body[i], snake.X);
                Canvas.SetTop(snake.body[i], snake.Y);
            }

            Screen.Children.Add(snake.head); // add snakehead
            Screen.Children.Add(Food.UIElement); // add food

            for (int i = 0; i < Snake.LengthOfSnake; i++) // add snake body
            {
                Screen.Children.Add(snake.body[i]);
            }
        }
    }
}
