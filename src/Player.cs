using SFML.Graphics;
using SFML.System;

namespace src
{
    public class Player
    {
        public CircleShape Shape;

        public Player()
        {
            Shape = new CircleShape(50f); // rayon 50
            Shape.FillColor = Color.Blue;
            Shape.Position = new Vector2f(590, 600); // Bas de l'écran
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Shape);
        }
    }
}
