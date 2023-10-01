
using System;
using System.Drawing;

namespace snake
{

    public class Enemy
    {
        public int A { get; set; } // Coordonnée X de l'ennemi
        public int B { get; set; } // Coordonnée Y de l'ennemi
        public string Direction { get; set; } // Direction de l'ennemi
        public Image Image { get; set; } // Image de l'ennemi 


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
