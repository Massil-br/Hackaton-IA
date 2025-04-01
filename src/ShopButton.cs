using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace src
{
    public enum ShopPurchase
    {
        None,
        ClickDamage,
        BeeAlly,
        TreeAlly
    }

    public class ShopButton
    {
        private RectangleShape buttonShape;
        private Text buttonText;
        private Font font;

        private bool isShopOpen = false;

        private List<(string name, int cost)> shopItems = new()
        {
            ("+1 Dégâts", 100),
            ("Abeille (DPS)", 200),
            ("Arbre (DPS)", 300)
        };

        public ShopButton()
        {
            font = new Font("src/assets/fonts/Arimo-Bold.ttf");

            buttonShape = new RectangleShape(new Vector2f(120, 40));
            buttonShape.FillColor = Color.Cyan;
            buttonShape.Position = new Vector2f(1140, 10);

            buttonText = new Text("Shop", font, 20)
            {
                FillColor = Color.Black,
                Position = new Vector2f(1165, 15)
            };
        }

        public void HandleClick(Vector2f mousePos)
        {
            if (buttonShape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y))
            {
                isShopOpen = !isShopOpen;
            }
        }

        public ShopPurchase HandleShopClick(Vector2f mousePos, ref int coins)
        {
            if (!isShopOpen) return ShopPurchase.None;

            float startY = 60;
            for (int i = 0; i < shopItems.Count; i++)
            {
                var rect = new FloatRect(950, startY, 300, 40);
                if (rect.Contains(mousePos.X, mousePos.Y))
                {
                    var (name, cost) = shopItems[i];
                    if (coins >= cost)
                    {
                        coins -= cost;
                        switch (name)
                        {
                            case "+1 Dégâts": return ShopPurchase.ClickDamage;
                            case "Abeille (DPS)": return ShopPurchase.BeeAlly;
                            case "Arbre (DPS)": return ShopPurchase.TreeAlly;
                        }
                    }
                }
                startY += 50;
            }

            return ShopPurchase.None;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(buttonShape);
            window.Draw(buttonText);

            if (isShopOpen)
            {
                float startY = 60;
                foreach (var item in shopItems)
                {
                    var bg = new RectangleShape(new Vector2f(300, 40))
                    {
                        FillColor = new Color(50, 50, 50, 200),
                        Position = new Vector2f(950, startY)
                    };
                    var label = new Text($"{item.name} - {item.cost} 💰", font, 18)
                    {
                        FillColor = Color.White,
                        Position = new Vector2f(960, startY + 8)
                    };

                    window.Draw(bg);
                    window.Draw(label);
                    startY += 50;
                }
            }
        }

        public bool IsOpen => isShopOpen;
    }
}


