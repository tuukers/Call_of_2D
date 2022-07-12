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
        public static int MAX_X = 1080;
        public static int MAX_Y = 720;
        public static int CELL_SIZE = 15;
        public static int FONT_SIZE = 25;
        private static int PLAYER_SPEED = 2;
        public static float ZOMBIE_NORMAL_SPEED_DIVIDE = 1;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Call Of 2d";
        private static Color WHITE = new Color(255, 255, 255);
        private static Color GREY = new Color(100,100,100);
        private static Color BROWN = new Color(80,50,0);
        public static int DEFAULT_ZOMBIES = 20;
        private static float PLAYER_RADIUS = 10;
        public static float ZOMBIE_RADIUS = 10;
        public static int ZOMBIE_HEALTH = 100;
        public static float BULLET_SPEED = 12;
        public static float BULLET_RADIUS = 2;
        private static float ROOM1_HEIGHT = 500;
        private static float ROOM1_WIDTH = 700;
        private static float WALL_THICKNESS = 12;
        public static int BASE_SCORE = 10;
        public static int POINTS_PER_HIT = 2;
        public static int POINTS_PER_KILL = 5;

        


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

                        //create bullet types
            Bullet m1Bullet = new Bullet(75);
            Bullet m1911Bullet = new Bullet(20);
            Bullet buckshot = new Bullet(3);
            Bullet machinegunBullet = new Bullet(15);

            //create weapons
            Weapon m1Garand = new Weapon(80,80,8,8,false,false,0,m1Bullet,"m1 garand");
            Weapon m1911 = new Weapon(70,70,7,7,false,false,0,m1911Bullet,"m1911");
            Weapon trenchGun = new Weapon(30,30,6,6,true,false,0,buckshot,"Trench Gun");
            Weapon mg42 = new Weapon(500,500,100,100,false,true,0,machinegunBullet,"MG42");

            // create the player
            Player player = new Player();
            player.SetNewHeldWeapon(m1Garand);
            player.SetNewStoredWeapon(mg42);
            player.SetColor(WHITE);
            player.SetPosition(new Vector2(MAX_X / 2, MAX_Y / 2));
            player.SetRadius(PLAYER_RADIUS);
            cast.AddActor("player", player);

            // create walls
            Wall wall1 = new Wall();
            Wall wall2 = new Wall();
            Wall wall3 = new Wall();
            Wall wall4 = new Wall();
            Wall wall5 = new Wall();
            Wall wall6 = new Wall();

            //left wall
            wall1.SetColor(GREY);
            wall1.SetPosition(new Vector2(MAX_X/10, MAX_Y/10));
            wall1.SetHeight(ROOM1_HEIGHT+WALL_THICKNESS);
            wall1.SetWidth(WALL_THICKNESS);
            wall1.SetHorizontal(false);

            //top wall
            wall2.SetColor(GREY);
            wall2.SetPosition(new Vector2(MAX_X/10, MAX_Y/10));
            wall2.SetHeight(WALL_THICKNESS);
            wall2.SetWidth(ROOM1_WIDTH);
            wall2.SetHorizontal(true);

            //bottom wall
            wall3.SetColor(GREY);
            wall3.SetPosition(new Vector2(MAX_X/10, MAX_Y/10+ROOM1_HEIGHT));
            wall3.SetHeight(WALL_THICKNESS);
            wall3.SetWidth(ROOM1_WIDTH);
            wall3.SetHorizontal(true);

            //right wall
            wall4.SetColor(GREY);
            wall4.SetPosition(new Vector2(MAX_X/10+ROOM1_WIDTH-WALL_THICKNESS, MAX_Y/10));
            wall4.SetHeight(ROOM1_HEIGHT+WALL_THICKNESS);
            wall4.SetWidth(WALL_THICKNESS);
            wall4.SetHorizontal(false);

            //center wall
            wall5.SetColor(BROWN);
            wall5.SetPosition(new Vector2((MAX_X/10+ROOM1_WIDTH-WALL_THICKNESS)*2/5, MAX_Y/10));
            wall5.SetHeight((ROOM1_HEIGHT+WALL_THICKNESS)*3/5);
            wall5.SetWidth(WALL_THICKNESS/2);
            wall5.SetHorizontal(true);

            //center wall bottom
            wall6.SetColor(GREY);
            wall6.SetPosition(new Vector2(MAX_X/10, MAX_Y*2/10));
            wall6.SetHeight(WALL_THICKNESS);
            wall6.SetWidth(ROOM1_WIDTH);
            wall6.SetHorizontal(true);
            
            cast.AddActor("wall",wall1);
            cast.AddActor("wall",wall2);
            cast.AddActor("wall",wall3);
            cast.AddActor("wall",wall4);
            cast.AddActor("wall",wall5);
            cast.AddActor("wall",wall6);


            //create stats
            Stats stats = new Stats();

            // creat HUD
            HUD weaponHUD = new HUD(player, stats);
            weaponHUD.SetColor(WHITE);
            weaponHUD.SetPosition(new Vector2(2*MAX_X/3,4*MAX_Y/5));
            weaponHUD.SetHUDType(0);
            weaponHUD.HUDSetup(0);
            Actor actor = (HUD)weaponHUD;
            cast.AddActor("HUD",actor);

            HUD scoreHUD = new HUD(player,stats);
            scoreHUD.SetColor(WHITE);
            scoreHUD.SetPosition(new Vector2(MAX_X/3,4*MAX_Y/5));
            scoreHUD.SetHUDType(1);
            scoreHUD.HUDSetup(1);
            Actor actor1 = (HUD)scoreHUD;
            cast.AddActor("HUD",actor1);

            Clock clock = new Clock();
            Round round = new Round();

            // create script
            ContactService contactService = new ContactService(wall1, wall2, wall3, wall4);
            
            Script script = new Script();
            script.AddAction("inputs", new ControlActorsAction(keyboardService,mouseService,videoService,contactService));
            script.AddAction("updates", new DrawActorsAction(videoService, mouseService));
            script.AddAction("updates", new HandleBulletCollisionsAction(contactService, stats));
            script.AddAction("updates", new HandleZombieZombieCollisionsAction(contactService));
            script.AddAction("outputs", new MoveActorsAction(videoService));
            script.AddAction("outputs", new HandleHUDs());
            script.AddAction("updates", new SpawnZombiesAction(clock, round));

            // start the game
            
            Director director = new Director(keyboardService, videoService, mouseService, clock);
            director.StartGame(cast,script);
        }
    }
}