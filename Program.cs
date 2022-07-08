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
        private static int FRAME_RATE = 45;
        private static int MAX_X = 1080;
        private static int MAX_Y = 720;
        public static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int PLAYER_SPEED = 2;
        public static float ZOMBIE_NORMAL_SPEED_DIVIDE = 1;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Call Of 2d";
        private static Color WHITE = new Color(255, 255, 255);
        private static Color GREY = new Color(100,100,100);
        private static int DEFAULT_ZOMBIES = 20;
        private static float PLAYER_RADIUS = 10;
        private static float ZOMBIE_RADIUS = 10;
        private static int ZOMBIE_HEALTH =2;
        public static float BULLET_SPEED = 9;
        public static float BULLET_RADIUS = 3;
        private static float ROOM1_HEIGHT = 400;
        private static float ROOM1_WIDTH = 600;
        private static float WALL_THICKNESS = 12;
        


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create survices
            KeyboardService keyboardService = new KeyboardService(PLAYER_SPEED);
            VideoService videoService = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            MouseService mouseService = new MouseService();


            // create the player
            Player player = new Player();
            player.SetColor(WHITE);
            player.SetPosition(new Vector2(MAX_X / 2, MAX_Y / 2));
            player.SetRadius(PLAYER_RADIUS);
            cast.AddActor("player", player);

            // create walls
            Wall wall1 = new Wall();
            Wall wall2 = new Wall();
            Wall wall3 = new Wall();
            Wall wall4 = new Wall();

            //left wall
            wall1.SetColor(GREY);
            wall1.SetPosition(new Vector2(MAX_X/3, MAX_Y/5));
            wall1.SetHeight(ROOM1_HEIGHT+WALL_THICKNESS);
            wall1.SetWidth(WALL_THICKNESS);
            wall1.SetHorizontal(false);

            //top wall
            wall2.SetColor(GREY);
            wall2.SetPosition(new Vector2(MAX_X/3, MAX_Y/5));
            wall2.SetHeight(WALL_THICKNESS);
            wall2.SetWidth(ROOM1_WIDTH);
            wall2.SetHorizontal(true);

            //bottom wall
            wall3.SetColor(GREY);
            wall3.SetPosition(new Vector2(MAX_X/3, MAX_Y/5+ROOM1_HEIGHT));
            wall3.SetHeight(WALL_THICKNESS);
            wall3.SetWidth(ROOM1_WIDTH);
            wall3.SetHorizontal(true);

            //right wall
            wall4.SetColor(GREY);
            wall4.SetPosition(new Vector2(MAX_X/3+ROOM1_WIDTH-WALL_THICKNESS, MAX_Y/5));
            wall4.SetHeight(ROOM1_HEIGHT+WALL_THICKNESS);
            wall4.SetWidth(WALL_THICKNESS);
            wall4.SetHorizontal(false);
            
            cast.AddActor("wall",wall1);
            cast.AddActor("wall",wall2);
            cast.AddActor("wall",wall3);
            cast.AddActor("wall",wall4);



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

                Zombie zombie = new Zombie(ZOMBIE_HEALTH);
                zombie.SetColor(color);
                zombie.SetPosition(position);
                zombie.SetRadius(ZOMBIE_RADIUS);
                cast.AddActor("zombie", zombie);
            }

            // create script
            ContactService contactService = new ContactService(wall1, wall2, wall3, wall4);
            
            Script script = new Script();
            script.AddAction("inputs", new ControlActorsAction(keyboardService,mouseService,videoService,contactService));
            script.AddAction("updates", new MoveActorsAction(videoService, mouseService));
            script.AddAction("updates", new HandleBulletCollisionsAction(contactService));
            script.AddAction("updates", new HandleZombieZombieCollisionsAction(contactService));
            script.AddAction("outputs", new DrawActorsAction(videoService));

            // start the game
            
            Director director = new Director(keyboardService, videoService, mouseService);
            director.StartGame(cast,script);
        }
    }
}