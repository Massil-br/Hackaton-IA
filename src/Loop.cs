




using SFML.Graphics;
using SFML.System;
using SFML.Window;

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

        private Camera camera;
         private State currentState = State.MainMenu;
        private State lastState = State.MainMenu;
        float deltatime;



        public Loop(){
            window = new(new VideoMode(1280,720),"HackatonIA");
            clock = new();
            camera = new(640,360);
            window.Closed += (sender, e) => window.Close();
            window.Resized += OnWindowResized;
            window.LostFocus += OnLostFocus;
            window.GainedFocus += OnGainedFocus;

            
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

        public  void Run(){
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
                        //run GameLoop
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