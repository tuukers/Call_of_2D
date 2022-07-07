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
        public static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int PLAYER_SPEED = 3;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Call Of 2d";
        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_ZOMBIES = 40;
        private static float PLAYER_RADIUS = 10;
        private static float ZOMBIE_RADIUS = 10;
        public static float BULLET_RADIUS = 2;
        


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
            ContactService contactService = new ContactService();

            // create the cast
            Cast cast = new Cast();

            // create the player
            Player player = new Player();
            player.SetColor(WHITE);
            player.SetPosition(new Vector2(MAX_X / 2, MAX_Y / 2));
            player.SetRadius(PLAYER_RADIUS);
            cast.AddActor("player", player);

            // create the zombies
            Random random = new Random();
            for (int i = 0; i < DEFAULT_ZOMBIES; i++)
            {
                int x = random.Next(1, MAX_X);
                int y = random.Next(1, MAX_Y);
                Vector2 position = new Vector2(x, y);

                int r = 0;//random.Next(0, 256);
                int g = 255;//random.Next(0, 256);
                int b = 0;//random.Next(0, 256);
                Color color = new Color(r, g, b);

                Zombie zombie = new Zombie();
                zombie.SetColor(color);
                zombie.SetPosition(position);
                zombie.SetRadius(ZOMBIE_RADIUS);
                cast.AddActor("zombie", zombie);
            }

            // create script
            
            Script script = new Script();
            script.AddAction("inputs", new ControlActorsAction(keyboardService,mouseService,videoService));
            script.AddAction("updates", new MoveActorsAction(videoService, mouseService));
            script.AddAction("updates", new HandleBulletZombieCollisionsAction());
            script.AddAction("updates", new HandleZombieZombieCollisionsAction(contactService));
            script.AddAction("outputs", new DrawActorsAction(videoService));

            // start the game
            Director director = new Director(keyboardService, videoService, mouseService);
            director.StartGame(cast,script);
        }
    }
}