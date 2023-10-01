// Créez une nouvelle classe Enemy.cs dans votre projet
using System;
using System.Drawing;

namespace snake
{

    public class Enemy
    {
        public int A { get; set; }
        public int B { get; set; }
        public string Direction { get; set; }
        public Image Image { get; set; }


        private int MaxWidth;
        private int MaxHeight;
        public Enemy(int maxWidth, int maxHeight)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }

        public void InitializePositionAndDirection()
        {
            // Initialisez la position et la direction de l'ennemi ici
            Random rand = new Random();
            A = rand.Next(0, MaxWidth); 
            B = rand.Next(0, MaxHeight);
            Direction = "right";
        }
    }
}
