

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

        private Upgrades upgrades = new Upgrades();
        private List<Ally> allies = new List<Ally>(); // Liste des allié

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

                if (shopButton.IsOpen)
                {
                    var purchase = shopButton.HandleShopClick(worldPos, ref coins);
                    switch (purchase)
                    {
                        case ShopPurchase.ClickDamage:
                            upgrades.BuyClickDamage();
                            break;
                        case ShopPurchase.BeeAlly:
                            upgrades.BuyAllyDPS(5f);
                            AddAlly(Color.Yellow); // Ajoute l'abeille
                            break;
                        case ShopPurchase.TreeAlly:
                            upgrades.BuyAllyDPS(10f);
                            AddAlly(Color.Green); // Ajoute l'arbre
                            break;
                    }
                }
                else if (enemy.Shape.GetGlobalBounds().Contains(worldPos.X, worldPos.Y))
                {
                    float damage = upgrades.GetClickDamage(10f);
                    enemy.TakeDamage(damage);
                     player.Attack();
                }
            }


            wasMousePressed = isMousePressed;


            // Met à jour chaque allié et applique ses dégâts
            foreach (var ally in allies)
            {
                ally.Update(deltatime, enemy); // Attaque de l'allié
                ally.Draw(window); // Dessine l'allié
            }


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
            enemy.Update(deltatime);
            enemy.Draw(window);
            ui.Update(coins, inBossFight ? bossTimer : -1f);
            ui.Draw(window);
            player.Update(deltatime);
            player.Draw(window);
            shopButton.Draw(window);
            

            return State.Playing;
        }
        private void AddAlly(Color color)
        {
            float allyDPS = upgrades.GetAllyDPS();
            var ally = new Ally(color, new Vector2f(50 + allies.Count * 40, 650), allyDPS); // Positionner à côté du joueur
            allies.Add(ally);
        }



    }
}

