using SFML.Graphics;
using SFML.System;

namespace src
{
    public class GameUI
    {
        public static Font font =new Font("src/assets/fonts/Arimo-Bold.ttf");
        public Text ScoreText;
        public Text TimerText;

        public GameUI()
        {
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
