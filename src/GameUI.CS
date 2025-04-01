using SFML.Graphics;
using SFML.System;

namespace src
{
    public class GameUI
    {
        private Font font;
        public Text ScoreText;
        public Text TimerText;

        public GameUI()
        {
            font = new Font("Arimo-Regular.ttf"); // Assure-toi d'avoir ce fichier dans ton dossier d'exécution
            ScoreText = new Text("Pièces : 0", font, 24) { Position = new Vector2f(10, 10), FillColor = Color.White };
            TimerText = new Text("", font, 24) { Position = new Vector2f(10, 40), FillColor = Color.Red };
        }

        public void Update(int coins, float bossTimeLeft = -1f)
        {
            ScoreText.DisplayedString = $"Pièces : {coins}";

            if (bossTimeLeft > 0)
                TimerText.DisplayedString = $"Boss : {bossTimeLeft:F1}s";
            else
                TimerText.DisplayedString = "";
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(ScoreText);
            window.Draw(TimerText);
        }
    }
}
