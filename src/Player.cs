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

        private Texture[] playerTexList;

        public Player()
        {
            Shape = new CircleShape(50f);
            Shape.FillColor = Color.Blue;

            baseAttack = new Vector2f(590, 600); // Position de base du joueur
            Shape.Position = baseAttack;

            playerTexList = new Texture[]
            {
                TextureManager.GetTexture("src/assets/Player/PosePlayer.png"),
                TextureManager.GetTexture("src/assets/Player/AttackPlayer.png")
            };

            sprite = new Sprite(playerTexList[0]);

            // Mise à l’échelle (si nécessaire)
            float scaleX = 128f / sprite.TextureRect.Width;
            float scaleY = 128f / sprite.TextureRect.Height;
            sprite.Scale = new Vector2f(Math.Abs(scaleX), scaleY);

            sprite.Position = baseAttack;
        }

        public void Attack()
        {
            if(!isAttacking){
                isAttacking = true;
                baseAttackTimer = AttackDuration;
            }
            
        }

        public void Update(float deltaTime)
        {
            if (isAttacking)
            {
                baseAttackTimer -= deltaTime;
                float progress = 1 - (baseAttackTimer / AttackDuration); // De 0 à 1
                Shape.Position = baseAttack * progress;
                PlayAttackAnimation();
                

                if (baseAttackTimer <= 0)
                {
                    isAttacking = false;
                    PlayIdleAnimation();
                }
            }

            UpdatePlayerSpritePosition();
        }

        private void PlayAttackAnimation()
        {   
            UpdatePlayerSpritePosition();
            sprite.Texture = playerTexList[1];
        }

        private void PlayIdleAnimation()
        {
            sprite.Texture = playerTexList[0];
        }

        private void UpdatePlayerSpritePosition()
        {
            sprite.Position = baseAttack;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(sprite);
        }
    }
}