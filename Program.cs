using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Callof2d.Game.Casting;
using Callof2d.Game.Directing;
using Callof2d.Game.Services;
using Callof2d.Game.Scripting;
using System.Numerics;


namespace Callof2d
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 60;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int PLAYER_SPEED = 3;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Call Of 2d";
        private static string DATA_PATH = "Data/messages.txt";
        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_ZOMBIES = 40;
        


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create survices
            KeyboardService keyboardService = new KeyboardService(PLAYER_SPEED);
            VideoService videoService = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            MouseService mouseService = new MouseService();

            // create the cast
            Cast cast = new Cast();
            

            // create the banner
            Actor banner = new Actor();
            banner.SetText("");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Vector2(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the player
            Player player = new Player();
            player.SetText("#");
            player.SetFontSize(FONT_SIZE);
            player.SetColor(WHITE);
            player.SetPosition(new Vector2(MAX_X / 2, MAX_Y / 2));
            cast.AddActor("player", player);

            // load the messages
            List<string> messages = File.ReadAllLines(DATA_PATH).ToList<string>();

            // create the zombies
            Random random = new Random();
            for (int i = 0; i < DEFAULT_ZOMBIES; i++)
            {
                string text = "Z";
                    // (char)random.Next(33, 126)).ToString();
                string message = messages[i];

                int x = random.Next(1, MAX_X);
                int y = random.Next(1, MAX_Y);
                Vector2 position = new Vector2(x, y);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Zombie zombie = new Zombie();
                zombie.SetText(text);
                zombie.SetFontSize(FONT_SIZE);
                zombie.SetColor(color);
                zombie.SetPosition(position);
                zombie.SetMessage(message);
                cast.AddActor("zombie", zombie);
                
            }

            // create script
            
            Script script = new Script();
            script.AddAction("inputs", new ControlActorsAction(keyboardService,mouseService,videoService));
            script.AddAction("updates", new MoveActorsAction(videoService, mouseService));
            script.AddAction("updates", new HandleCollisionsAction());
            script.AddAction("outputs", new DrawActorsAction(videoService));




            // start the game
            Director director = new Director(keyboardService, videoService, mouseService);
            director.StartGame(cast,script);
        }
    }
}