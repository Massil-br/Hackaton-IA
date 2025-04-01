using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace src
{
    public class Ally
    {
        public RectangleShape Shape { get; private set; }
        private float attackTimer = 0f;
        private const float AttackInterval = 5f; // Attaque toutes les 5 secondes
        private float dps;

        public Ally(Color color, Vector2f position, float dps)
        {
            Shape = new RectangleShape(new Vector2f(30, 30));
            Shape.FillColor = color;
            Shape.Position = position;
            this.dps = dps;
        }

        public void Update(float deltaTime, Enemy enemy)
        {
            attackTimer -= deltaTime;
            if (attackTimer <= 0f)
            {
                // Infliger les dégâts à l'ennemi
                enemy.TakeDamage(dps);
                // Réinitialiser le timer
                attackTimer = AttackInterval;
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}
