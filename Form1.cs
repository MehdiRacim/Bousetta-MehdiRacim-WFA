﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Media;

namespace snake
{
    public partial class Form1 : Form
    {
        // Déclarations de variables membres
        private List<SnakePart> Snake = new List<SnakePart>();
        private Circle food = new Circle();
        int maxWidth;
        int maxHeight;
        Random rand = new Random();
        bool goLeft, goRight, GoDown, goUp;
        int score;
        int highscore;
        private string previousDirection;
        private Enemy enemy;
        private SoundPlayer player;
        private SoundPlayer gameOverPlayer;

        public int MaxWidth
        {
            get { return picCanvas.Width / Settings.Width - 1; }
        }

        public int MaxHeight
        {
            get { return picCanvas.Height / Settings.Height - 1; }
        }


        public Form1()
        {
            InitializeComponent();
            new Settings();
            enemy = new Enemy(MaxWidth, MaxHeight);
            player = new SoundPlayer(Properties.Resources.Snake_Baron);
            gameOverPlayer = new SoundPlayer(Properties.Resources.GameOver);



        }


        // Gestionnaires d'événements
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Gestion des touches enfoncées
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
            // Gestion des touches relâchées
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
            // Début du jeu
            difficultyComboBox.Enabled = false;
            RestartGame();
            Difficulty();
            player.Play();
        }

        private void TakeSnapshot(object sender, EventArgs e)
        {
            // Prise de capture d'écran du jeu
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

            // Gestion du déplacement du serpent
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

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    // Déplacement de la tête du serpent
                    switch (Settings.directions)
                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                    }
                    // Gestion des bords de l'écran
                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxWidth;
                    }
                    if (Snake[i].X > maxWidth)
                    {
                        Snake[i].X = 0;
                    }
                    if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxHeight;
                    }
                    if (Snake[i].Y > maxHeight)
                    {
                        Snake[i].Y = 0;
                    }

                    // Gestion de la collision avec la nourriture
                    if (Snake[i].X == food.x && Snake[i].Y == food.y)
                    {
                        EatFood();
                    }

                    if (Snake[i].X == enemy.A && Snake[i].Y == enemy.B)
                    {
                        GameOver(); // Appel à la fonction GameOver en cas de collision avec l'ennemi
                        return; // Sortez de la boucle, la partie est terminée
                    }

                    // Gestion de la collision avec le corps du serpent
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }
                    }
                }
                else
                {
                    // Déplacement du corps du serpent
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
            if (CheckWinCondition())
            {
                // Le joueur a gagné
                GameWin();
                return; // Sortez de la boucle, la partie est terminée
            }





            picCanvas.Invalidate();
        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            // Mise à jour de l'affichage du jeu
            Graphics canvas = e.Graphics;
            Image snakeBody = null;
            Image snakeHead = null;
            Image snakeTail = null;
            Image apple = Properties.Resources.apple;
            Image enemyImage = Properties.Resources.enemy2;

            for (int i = 0; i < Snake.Count; i++)
            {
                if (i == 0)
                {
                    // Affichage de la tête du serpent
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
                        Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height
                    ));
                }
                else if (i < Snake.Count - 1)
                {
                    // Affichage du corps du serpent
                    if (previousDirection == "left")
                    {
                        snakeBody = Properties.Resources.body_horizontal;
                    }
                    if (previousDirection == "right")
                    {
                        snakeBody = Properties.Resources.body_horizontal;
                    }
                    if (previousDirection == "up")
                    {
                        snakeBody = Properties.Resources.body_vertical;
                    }
                    if (previousDirection == "down")
                    {
                        snakeBody = Properties.Resources.body_vertical;
                    }

                    canvas.DrawImage(snakeBody, new Rectangle(
                        Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height
                    ));
                }
                else if (i == Snake.Count - 1)
                {
                    // Affichage de la queue du serpent
                    switch (previousDirection)
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
                        Snake[i].X * Settings.Width,
                        Snake[i].Y * Settings.Height,
                        Settings.Width, Settings.Height
                    ));
                }
            }

            // Affichage de la nourriture
            canvas.DrawImage(apple, new Rectangle(
                food.x * Settings.Width,
                food.y * Settings.Height,
                Settings.Width, Settings.Height
            ));
            canvas.DrawImage(enemyImage, new Rectangle(
                enemy.A * Settings.Width,
                enemy.B * Settings.Height,
                Settings.Width, Settings.Height
            ));
        }


        private void RestartGame()
        {
            // Réinitialisation du jeu
            maxWidth = picCanvas.Width / Settings.Width - 1;
            maxHeight = picCanvas.Height / Settings.Height - 1;

            Snake.Clear();

            StartButton.Enabled = false;
            SnapButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score: " + score;
            SnakePart head = new SnakePart
            {
                X = 10,
                Y = 5,
                Direction = "right", // Définissez la direction initiale
                Image = Properties.Resources.head_right // Remplacez par l'image de la tête orientée vers la droite
            };
            Snake.Add(head);
            for (int i = 0; i < 10; i++)
            {
                SnakePart body = new SnakePart();
                Snake.Add(body);
                food = new Circle { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };
                gameTimer.Start();
            }
            enemy.InitializePositionAndDirection();
            gameOverPlayer.Stop();
        }
        private int applesEatenSinceLastPassage = 0;

        private void EatFood()
        {
            do
            {
                // Générez une nouvelle position pour la pomme
                food.x = rand.Next(2, maxWidth);
                food.y = rand.Next(2, maxHeight);
            } while (IsPositionOccupiedBySnake(food.x, food.y));

            // Gestion de la collision avec la nourriture
            score += 1;
            txtScore.Text = "Score: " + score;

            // Créez un nouveau segment du corps en utilisant la classe SnakePart
            SnakePart body = new SnakePart
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y,
                Direction = Snake[Snake.Count - 1].Direction, // Copiez la direction du dernier segment
                Image = Properties.Resources.body_horizontal // Remplacez par l'image appropriée pour le corps
            };
            Snake.Add(body);
            applesEatenSinceLastPassage++;

            // Vérifiez si 5 pommes ont été mangées
            if (applesEatenSinceLastPassage >= 5)
            {
                // Réinitialisez la position et la direction de l'ennemi
                GenerateEnemyPosition();

                // Réinitialisez le compteur de pommes mangées
                applesEatenSinceLastPassage = 0;
            }
        }

        private void GameOver()
        {
            // Gestion de la fin de partie
            gameTimer.Stop();
            StartButton.Enabled = true;
            SnapButton.Enabled = true;
            applesEatenSinceLastPassage = 0;


            difficultyComboBox.Enabled = true;

            if (score > highscore)
            {
                highscore = score;
                txtHighScore.Text = "highscore :" + Environment.NewLine + score;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;


                gameOverPlayer.Play();
            }
            MessageBox.Show("Game Over! Your Score: " + score, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void Difficulty()
        {
            // Gestion de la difficulté du jeu
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
        private bool CheckWinCondition()
        {
            // Calculez le nombre total de cases sur l'écran
            int totalGridCells = (maxWidth + 1) * (maxHeight + 1);

            // Calculez le nombre de cases occupées par le serpent, la nourriture et l'ennemi
            int occupiedCells = Snake.Count + 1; // Compte les cases du serpent et de la nourriture
            occupiedCells += 1; // Ajoutez une case pour l'ennemi (ou plus si nécessaire)

            // Vérifiez si le nombre de cases occupées est égal au nombre total de cases
            return occupiedCells >= totalGridCells;
        }
        private void GameWin()
        {
            gameTimer.Stop();
            StartButton.Enabled = true;
            SnapButton.Enabled = true;
            difficultyComboBox.Enabled = true;

            MessageBox.Show("Congratulations! You won the game!", "Game Won", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool IsPositionOccupiedBySnake(int x, int y)
        {
            foreach (var part in Snake)
            {
                if (part.X == x && part.Y == y)
                {
                    return true; // La position est occupée par le serpent
                }
              
            }
            return false; // La position n'est pas occupée par le serpent
        }
        private void GenerateEnemyPosition()
        {
            do
            {
                // Générez une nouvelle position pour l'ennemi
                enemy.A = rand.Next(2, maxWidth);
                enemy.B = rand.Next(2, maxHeight);
            } while (IsPositionOccupiedBySnake(enemy.A, enemy.B));
        }




    }
}
