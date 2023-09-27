using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace snake
{
    public partial class Form1 : Form
    {

        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        int maxWidth;
        int maxHeight;

        Random rand = new Random();

        bool goLeft, goRight, GoDown, goUp;

        int score;
        int highscore;
        private string previousDirection;











        public Form1()
        {
            InitializeComponent();


            new Settings();
        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Settings.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && Settings.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && Settings.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && Settings.directions != "up")
            {
                GoDown = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                GoDown = false;
            }

        }

        private void StartGame(object sender, EventArgs e)
        {
            difficultyComboBox.Enabled = false;
            RestartGame();
            Difficulty();

        }

        private void TakeSnapshot(object sender, EventArgs e)
        {
            Label caption = new Label();
            caption.Text = " i scored" + score + "and my highscore :" + highscore + "on the snake game";
            caption.Font = new Font("Ariel", 12, FontStyle.Bold);
            caption.ForeColor = Color.LightBlue;
            caption.AutoSize = false;
            caption.Width = picCanvas.Width;
            caption.Height = 30;
            caption.TextAlign = ContentAlignment.MiddleCenter;
            picCanvas.Controls.Add(caption);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "snake game snapshot";
            dialog.DefaultExt = "jpg";
            dialog.Filter = "JPG File |*.jpg";
            dialog.ValidateNames = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int width = Convert.ToInt32(picCanvas.Width);
                int height = Convert.ToInt32(picCanvas.Height);
                Bitmap bmp = new Bitmap(width, height);
                picCanvas.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                picCanvas.Controls.Remove(caption);
            }

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            previousDirection = Settings.directions;
            //setting the directions
            if (goLeft)
            {
                Settings.directions = "left";
            }
            if (goRight)
            {
                Settings.directions = "right";
            }
            if (GoDown)
            {
                Settings.directions = "down";
            }
            if (goUp)
            {
                Settings.directions = "up";
      
            }
            
            // end of directions

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.directions)
                    {
                        case "left":
                            Snake[i].x--;
                            break;
                        case "right":
                            Snake[i].x++;
                            break;
                        case "down":
                            Snake[i].y++;
                            break;
                        case "up":
                            Snake[i].y--;
                            break;
                    }
                    if (Snake[i].x < 0)
                    {
                        Snake[i].x = maxWidth;
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        Snake[i].x = 0;
                    }
                    if (Snake[i].y < 0)
                    {
                        Snake[i].y = maxHeight;
                    }
                    if (Snake[i].y > maxHeight)
                    {
                        Snake[i].y = 0;
                    }

                    if (Snake[i].x == food.x && Snake[i].y == food.y)
                    {
                        EatFood();
                    }
                    

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].x == Snake[j].x && Snake[i].y == Snake[j].y)
                        {
                            GameOver();

                        }
                    }

                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }
            }

            picCanvas.Invalidate();
        }
        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Image snakeBody = null;
            Image snakeHead = null;
            Image snakeTail = null;
            Image apple = Properties.Resources.apple;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    if (Settings.directions == "right")
                    {
                        snakeHead = Properties.Resources.head_right;
                    }
                    if (Settings.directions == "left")
                    {
                        snakeHead = Properties.Resources.head_left;
                    }
                    if (Settings.directions == "up")
                    {
                        snakeHead = Properties.Resources.head_up;
                    }
                    if (Settings.directions == "down")
                    {
                        snakeHead = Properties.Resources.head_down;
                    }

                    canvas.DrawImage(snakeHead, new Rectangle(
                        Snake[i].x * Settings.Width,
                        Snake[i].y * Settings.Height,
                        Settings.Width, Settings.Height
                    ));
                }
                else if (i < Snake.Count - 1)
                {
                    if (Settings.directions == "left")
                    {
                        snakeBody = Properties.Resources.body_horizontal;
                    }
                    if (Settings.directions == "right")
                    {
                        snakeBody = Properties.Resources.body_horizontal;
                    }
                    if (Settings.directions == "up")
                    {
                        snakeBody = Properties.Resources.body_vertical;
                    }
                    if (Settings.directions == "down")
                    {
                        snakeBody = Properties.Resources.body_vertical;
                    }

                    canvas.DrawImage(snakeBody, new Rectangle(
                        Snake[i].x * Settings.Width,
                        Snake[i].y * Settings.Height,
                        Settings.Width, Settings.Height
                    ));
                }
                else if (i == Snake.Count - 1)
                {
                    switch (Settings.directions)
                    {
                        case "left":
                            snakeTail = Properties.Resources.tail_right;
                            break;
                        case "right":
                            snakeTail = Properties.Resources.tail_left;
                            break;
                        case "up":
                            snakeTail = Properties.Resources.tail_down;
                            break;
                        case "down":
                            snakeTail = Properties.Resources.tail_up;
                            break;
                    }

                    canvas.DrawImage(snakeTail, new Rectangle(
                        Snake[i].x * Settings.Width,
                        Snake[i].y * Settings.Height,
                        Settings.Width, Settings.Height
                    ));
                }
            }

            canvas.DrawImage(apple, new Rectangle(
                food.x * Settings.Width,
                food.y * Settings.Height,
                Settings.Width, Settings.Height
            ));
        }







        private void RestartGame()
        {
            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;

            Snake.Clear();

            StartButton.Enabled = false;
            SnapButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score: " + score;
            Circle head = new Circle { x = 10, y = 5 };
            Snake.Add(head);
            for (int i = 0; i < 10; i++)
            {
                Circle body = new Circle();
                Snake.Add(body);
                food = new Circle { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };
                gameTimer.Start();
            }
        }
        private void EatFood()
        {
            score += 1;

            txtScore.Text = "Score: " + score;
            Circle body = new Circle
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y
            };
            Snake.Add(body);

            food = new Circle { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };




        }

        private void GameOver()
        {
            gameTimer.Stop();
            StartButton.Enabled = true;
            SnapButton.Enabled = true;

            difficultyComboBox.Enabled = true;

            if (score > highscore)
            {
                highscore = score;
                txtHighScore.Text = "highscore :" + Environment.NewLine + score;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }



        }
        private void Difficulty()
        {
            if (difficultyComboBox.SelectedIndex == 0)
            {
                gameTimer.Interval = 50;

            }
            if (difficultyComboBox.SelectedIndex == 1)
            {
                gameTimer.Interval = 30;

            }
            if (difficultyComboBox.SelectedIndex == 2)
            {
                gameTimer.Interval = 10;

            }
        }


    }
}