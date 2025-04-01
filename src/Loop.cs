using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


namespace src{
    public enum State
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Multiplayer
    }

    public class Loop{
        private RenderWindow window;
        private Clock clock;

        private bool wasMousePressed = false;

        private Camera camera;
        private State currentState = State.MainMenu;
        private State lastState = State.MainMenu;
        float deltatime;

        private Enemy enemy;
        private GameUI ui;
        private int coins = 0;
        private int enemiesKilled = 0;
        private float bossTimer = 0f;
        private bool inBossFight = false;

        public Loop(){
            window = new(new VideoMode(1280,720),"HackatonIA");
            clock = new();
            camera = new(640,360);
            window.Closed += (sender, e) => window.Close();
            window.Resized += OnWindowResized;
            window.LostFocus += OnLostFocus;
            window.GainedFocus += OnGainedFocus;

            enemy = new Enemy();
            ui = new GameUI();
            currentState = State.Playing;
        }

        private void OnWindowResized(object? sender, SizeEventArgs e)
        {
            camera.Resize(640,360);
            Camera.ViewHeight = camera.GetHeight();
            Camera.ViewWidth = camera.GetWidth();
        }

        private void OnLostFocus(object? sender, EventArgs e)
        {
            lastState = currentState;
            currentState = State.Paused;
        }

        private void OnGainedFocus(object? sender, EventArgs e)
        {
            currentState = lastState;
        }

        public void Run(){
            while (window.IsOpen)
            {
                deltatime = clock.Restart().AsSeconds();
                window.DispatchEvents();
                window.Clear();

                switch (currentState)
                {
                    case State.MainMenu:
                        //run main menu loop
                        break;
                    case State.Playing:
                        HandleGameplay();
                        break;
                    case State.Paused:
                        //pause menu
                        break;
                }

                window.Display();
            }
        }

        private void HandleGameplay()
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

                if (enemy.Shape.GetGlobalBounds().Contains(worldPos.X, worldPos.Y))
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

            enemy.Draw(window);
            ui.Update(coins, inBossFight ? bossTimer : -1f);
            ui.Draw(window);
        }
    }
}