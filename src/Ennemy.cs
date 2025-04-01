using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace src
{
    public class Enemy
    {
        public RectangleShape Shape;
        public float MaxHealth;
        public float CurrentHealth;
        public bool IsBoss;

        public Enemy(bool isBoss = false)
        {
            IsBoss = isBoss;
            Shape = new RectangleShape(new Vector2f(200, 200));
            Shape.FillColor = IsBoss ? Color.Red : Color.Green;
            Shape.Position = new Vector2f(540, 260);

            MaxHealth = IsBoss ? 300f : 100f;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float dmg)
        {
            CurrentHealth -= dmg;
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}

