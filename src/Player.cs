using SFML.Graphics;
using SFML.System;

namespace src
{
    public class Player
    {
        public CircleShape Shape;
        private float baseAttackTimer;
        private bool isAttacking;
        private Vector2f baseAttack;

        private const float AttackDuration = 0.1f; // Durée de l'attaque en secondes
    

        public Player()
        {
            Shape = new CircleShape(50f); // rayon 50
            Shape.FillColor = Color.Blue;
            baseAttack = new Vector2f(590, 600); // Bas de l'écran
            Shape.Position = baseAttack;
        }

        public void Attack(){
            isAttacking = true;
            baseAttackTimer = AttackDuration;
        }

        public void Update(float deltaTime)
        {
            if (isAttacking)
            {
                baseAttackTimer -= deltaTime;

                // Déplacement vers l'avant pendant l'attaque
                float progress = 1 - (baseAttackTimer / AttackDuration); // De 0 à 1
                Shape.Position = baseAttack * progress;
                // Fin de l'attaque
                if (baseAttackTimer <= 0)
                {
                    isAttacking = false;
                    Shape.Position = baseAttack; // Retour à la position initiale
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}
