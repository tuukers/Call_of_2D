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
        public static int FONT_SIZE_SMALL = 15;
        public static int PLAYER_SPEED_DIVIDER = 2;
        public static float ZOMBIE_NORMAL_SPEED_DIVIDE = 1;
        public static int ZOMBIE_SPREAD =50;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Call Of 2d";
        private static Color WHITE = new Color(255, 255, 255);
        public static Color BLACK = new Color(0,0,0);
        private static Color GREY = new Color(100,100,100);
        private static Color LIGHT_GREY = new Color(75,75,75);
        private static Color BROWN = new Color(80,50,0);
        private static Color YELLOW = new Color(100,100,0);
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
            KeyboardService keyboardService = new KeyboardService();
            VideoService videoService = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            MouseService mouseService = new MouseService();

                        //create bullet types
            Bullet rifleBullet = new Bullet(75);
            Bullet refleBullet2 = new Bullet(120);
            Bullet pistolBullet = new Bullet(40);
            Bullet buckshot = new Bullet(30);
            buckshot.SetBuckShot(8);
            Bullet superBuckShot = new Bullet(30);
            superBuckShot.SetBuckShot(16);
            Bullet machinegunBullet = new Bullet(25);

            rifleBullet.SetColor(LIGHT_GREY);
            refleBullet2.SetColor(LIGHT_GREY);
            pistolBullet.SetColor(LIGHT_GREY);
            buckshot.SetColor(LIGHT_GREY);
            superBuckShot.SetColor(LIGHT_GREY);
            machinegunBullet.SetColor(LIGHT_GREY);
            

            //create weapons
            Weapon m1Garand = new Weapon(80,80,8,8,false,false,4,2,rifleBullet,"m1 garand",2);
            Weapon kar98 = new Weapon(75,75,5,5,false,false,2,0,refleBullet2,"Kar98",3);
            Weapon m1911 = new Weapon(70,70,7,7,false,false,5,3,pistolBullet,"m1911",3);
            Weapon Mouserc96 = new Weapon(80,80,10,10,false,false,6,4,pistolBullet,"Mouser C96",5);
            Weapon trenchGun = new Weapon(30,30,6,6,true,false,2,20,buckshot,"Trench Gun",0);
            Weapon trenchSweaper = new Weapon(60,60,12,12,true,true,3,30,superBuckShot, "Trench Sweaper",0);
            Weapon mg42 = new Weapon(500,500,100,100,false,true,25,10,machinegunBullet,"MG42",6);

            cast.AddActor("weapon",m1Garand);
            cast.AddActor("weapon",kar98);
            cast.AddActor("weapon",m1911);
            cast.AddActor("weapon",Mouserc96);
            cast.AddActor("weapon",trenchGun);
            cast.AddActor("weapon",trenchSweaper);
            cast.AddActor("weapon",mg42);

            // create the player
            Player player = new Player();
            player.SetNewHeldWeapon(trenchSweaper);
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
            wall5.SetPosition(new Vector2((MAX_X/10+ROOM1_WIDTH-WALL_THICKNESS)*2/5, MAX_Y/10+(ROOM1_HEIGHT+WALL_THICKNESS)*3/5));
            wall5.SetHeight((ROOM1_HEIGHT+WALL_THICKNESS)*2/5);
            wall5.SetWidth(WALL_THICKNESS/2);
            wall5.SetHorizontal(true);

            
            cast.AddActor("wall",wall1);
            cast.AddActor("wall",wall2);
            cast.AddActor("wall",wall3);
            cast.AddActor("wall",wall4);
            cast.AddActor("wall",wall5);

            //creating mistery box
            Wall misteryBox = new Wall();
            //misteryBox.SetRadius(50);
            misteryBox.SetWidth(70);
            misteryBox.SetHeight(40);
            misteryBox.SetPosition(new Vector2(MAX_X/10 + 70,MAX_Y/10+ROOM1_HEIGHT-20-WALL_THICKNESS));
            misteryBox.SetColor(YELLOW);

            cast.AddActor("box",misteryBox);


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
            
            HUD promptHUD = new HUD(player,stats);
            promptHUD.SetColor(WHITE);
            promptHUD.SetPosition(new Vector2(MAX_X/2,2*MAX_Y/3));
            promptHUD.SetHUDType(1);
            promptHUD.HUDSetup(1);
            
            Actor actor2 = (HUD)promptHUD;
            cast.AddActor("HUD",actor2);

            Clock clock = new Clock();
            Round round = new Round();

            // create script
            ContactService contactService = new ContactService(wall1, wall2, wall3, wall4);
            // AudioService audioService = new AudioService();
            
            Script script = new Script();
            script.AddAction("inputs", new ControlActorsAction(keyboardService,mouseService,videoService,contactService,stats));
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