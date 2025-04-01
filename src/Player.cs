using SFML.Graphics;
using SFML.System;
using Shared;

namespace src
{
    public class Player
    {
        public CircleShape Shape;
        private float baseAttackTimer;
        private bool isAttacking;
        private Vector2f baseAttack;

        private const float AttackDuration = 0.1f; // Durée de l'attaque en secondes

        private Sprite sprite;

        private Texture[] palyerTexList;


    

        public Player()
        {
            Shape = new CircleShape(50f); // rayon 50
            Shape.FillColor = Color.Blue;
            baseAttack = new Vector2f(590, 600); // Bas de l'écran
            Shape.Position = baseAttack;
            sprite = new();
            float scaleX = 64f / sprite.TextureRect.Width;
            float scaleY = 64f / sprite.TextureRect.Height;
            sprite.Scale = new Vector2f(Math.Abs(scaleX), scaleY); 


            palyerTexList = [
                TextureManager.GetTexture("src/assets/Player/PosePlayer.png"),
                TextureManager.GetTexture("src/assets/Player/AttackPlayer.png")
            ];
            sprite.Texture = palyerTexList[0];

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
                PlayAttackAnimation();
                // Fin de l'attaque
                if (baseAttackTimer <= 0)
                {
                    isAttacking = false;
                    Shape.Position = baseAttack; // Retour à la position initiale
                    PlayIdleAnimation();
                }
            }

            UpdatePlayerSpritePosition();


        }

        private void PlayAttackAnimation(){
            sprite.Texture = palyerTexList[1];
        }
        private void PlayIdleAnimation(){
            sprite.Texture = palyerTexList[0];
        }

        private void UpdatePlayerSpritePosition(){
            sprite.Position = Shape.Position;
        }

        public void Draw(RenderWindow window)
        {
           // window.Draw(Shape); // a la place du draw shape tu draw Sprite texture a la position de shape
            

            window.Draw(sprite);
        }
    }
}
