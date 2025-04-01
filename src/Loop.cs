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

        

        private Player player;

        private RenderWindow window;
        private Clock clock;

       

        private State currentState = State.MainMenu;
        private State lastState = State.MainMenu;
        float deltatime;

        private GameLoop gameloop;

       
        public Loop(){
            
            window = new(new VideoMode(1280,720),"HackatonIA");
            clock = new();
            window.Closed += (sender, e) => window.Close();
            window.Resized += OnWindowResized;
            window.LostFocus += OnLostFocus;
            window.GainedFocus += OnGainedFocus;
            player = new Player();
            gameloop = new();


            
            currentState = State.Playing;
        }

        private void OnWindowResized(object? sender, SizeEventArgs e)
        {
            
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
                        currentState = gameloop.Run(deltatime, window,player) ;
                        break;
                    case State.Paused:
                        //pause menu
                        break;
                }
                
                window.Display();
            }
        }

        
    }
}