using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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

        Snake snake = new Snake();
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        Food food = new Food();

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1); // Speed of snake
            timer.Tick += UpdateScreen;
            timer2.Interval = TimeSpan.FromSeconds(0.001); // Update time of Input
            timer2.Tick += UpdateInput;


            Bildschrim.Children.Add(food.e); // add food.

            for (int i = 0; i < Snake.lengthOfSnake; i++) // add snake body.
                Bildschrim.Children.Add(snake.body[i]);
            // add snakehead. Execute behind Add(body) because of layered presentation. 
            // so ist der rote kopf immer im vordergrund
            Bildschrim.Children.Add(snake.head);
            timer.Start();
            timer2.Start();
            //addImage();
        }

        private void UpdateInput(object sender, EventArgs e)
        {
            snake.ChangeDirection();
        }

        bool createNewFood;
        private void UpdateScreen(object sender, EventArgs e)
        {
            snake.MoveForward();

            // Spielende bei Kollision mit dem Bildschirmrand.
            if (snake.X < 0 || snake.X >= Bildschrim.ActualWidth || snake.Y >= Bildschrim.ActualHeight || snake.Y < 0)
                Restart();
            // Spielende bei Kollision mit dem Body
            for (int i = 0; i < Snake.lengthOfSnake; i++)
            {
                if (snake.X == snake._x[i] && snake.Y == snake._y[i])
                    Restart();
            }
            // Snake eats food.
            if (snake.X == food.X && snake.Y == food.Y)
            {
                snake.EatAndGrow();
                Bildschrim.Children.Add(snake.body[Snake.lengthOfSnake - 1]); // add new bodypart of snake to the snake.

                do
                {
                    food.CreateFood(); // create new food at a random place.
                    for (int i = 0; i < Snake.lengthOfSnake; i++)
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


                // Update score
                score += 10;
                lblScore.Content = score.ToString();
                if (score >= 4500)
                {
                    lblScore.Content = "WON!";
                    Restart();
                }

            }
            //Canvas.SetLeft(newImage, snake.X);
            //Canvas.SetTop(newImage, snake.Y);
        }

        public void Restart()
        {
            //return; // ------------------- TESTING
            if (score > maxScore)
            {
                maxScore = score;
                lblMaxScore.Content = maxScore.ToString();
            }
            score = 0;
            lblScore.Content = score.ToString();
            Bildschrim.Children.Clear(); // clear Bildschirm
            snake.body.Clear();
            Snake.lengthOfSnake = 5;
            snake.X = 180;
            snake.Y = 160;
            // create body of snake
            for (int i = 0; i < Snake.lengthOfSnake; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = Snake.sizeOfSnake;
                ellipse.Height = Snake.sizeOfSnake;
                //ellipse.Fill = Brushes.Yellow;
                snake.body.Add(ellipse);

                // Draw body at first on the same position as head
                snake._x[i] = snake.X;
                snake._y[i] = snake.Y;
                Canvas.SetLeft(snake.body[i], snake.X);
                Canvas.SetTop(snake.body[i], snake.Y);
            }
            //addImage();
            Bildschrim.Children.Add(snake.head); // add snakehead.
            Bildschrim.Children.Add(food.e); // add food.
            for (int i = 0; i < Snake.lengthOfSnake; i++) // add snake body.
                Bildschrim.Children.Add(snake.body[i]);
        }
    }
}
