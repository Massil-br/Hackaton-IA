

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace src{



    public class GameLoop{

        private Enemy enemy;
        private GameUI ui;
        private int coins = 0;
        private int enemiesKilled = 0;
        private float bossTimer = 0f;
        private bool inBossFight = false;
        private bool wasMousePressed = false;
        private ShopButton shopButton;

        public GameLoop(){
            shopButton = new ShopButton();
            enemy = new Enemy();
            ui = new GameUI();
        }


        public State Run(float deltatime, RenderWindow window, Player player)
        {
            if (inBossFight)
            {
                bossTimer -= deltatime;
                if (bossTimer <= 0)
                {
                    // Temps écoulé, boss non battu
                    inBossFight = false;
                    enemiesKilled = 0;
                    enemy = new Enemy(); // Recommencer la boucle
                }
            }

            bool isMousePressed = Mouse.IsButtonPressed(Mouse.Button.Left);

            if (isMousePressed && !wasMousePressed)
            {
                Vector2i mousePos = Mouse.GetPosition(window);
                Vector2f worldPos = window.MapPixelToCoords(mousePos);

                shopButton.HandleClick(worldPos);

                if (enemy.Shape.GetGlobalBounds().Contains(worldPos.X, worldPos.Y) && !shopButton.IsOpen)
                {
                    enemy.TakeDamage(10f);
                }
            }


            wasMousePressed = isMousePressed;


            if (enemy.IsDead())
            {
                coins += enemy.IsBoss ? 50 : 10;
                enemiesKilled++;

                if (enemy.IsBoss)
                {
                    inBossFight = false;
                    enemiesKilled = 0;
                }

                if (enemiesKilled >= 8)
                {
                    inBossFight = true;
                    bossTimer = 30f;
                    enemy = new Enemy(isBoss: true);
                }
                else
                {
                    enemy = new Enemy();
                }
            }
            enemy.Update();
            enemy.Draw(window);
            ui.Update(coins, inBossFight ? bossTimer : -1f);
            ui.Draw(window);
            player.Draw(window);
            shopButton.Draw(window);

            return State.Playing;
        }



    }
}