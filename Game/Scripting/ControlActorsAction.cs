using System;
using System.Collections.Generic;
using System.Numerics;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;



namespace Callof2d.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the snake.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the snake's head.
    /// </para>
    /// </summary>
    public class ControlActorsAction : Action
    {
        private KeyboardService keyboardService;
        private MouseService mouseService;
        private VideoService videoService;
        private ContactService contactService;
        private Stats stats;
        private Point direction = new Point(0,-Program.CELL_SIZE);
        private Point direction2 = new Point(0,-Program.CELL_SIZE);
        private bool collision = false;
        private DateTime lastShot;
        private bool reload = false;
        private DateTime reloadTimeInit;
        private int reloadTime;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService, MouseService mouseService, VideoService videoService, ContactService contactService,Stats stats)
        {
            this.keyboardService = keyboardService;
            this.mouseService = mouseService;
            this.videoService = videoService;
            this.contactService = contactService; 
            this.stats = stats;
            this.lastShot=DateTime.Now;           
        }

        /// <inheritdoc/>

        public void Execute(Cast cast, Script script)
        {
            Player player = (Player)cast.GetFirstActor("player");
            List<Actor> walls = cast.GetActors("wall");
            List<Actor> hUDs= cast.GetActors("HUD");
            Vector2 playerPosition = player.GetPosition();
            Weapon weapon1 = player.GetHeldWeapon();
            Weapon weapon2 = player.GetStoredWeapon();
            bool fullAuto = weapon1.GetFullAuto();
            bool isShotgun = weapon1.GetIsShotGun();
            
            
            TimeSpan timeSinceLastShot = DateTime.Now - this.lastShot;
            if((float)timeSinceLastShot.TotalMilliseconds >= 1000/weapon1.GetFireRate())
            {
                if(mouseService.IsMousePressed()&!fullAuto)
                {
                    Vector2 mousePosition = mouseService.GetMousePosition();
                    if(!isShotgun)
                    {
                        player.Shoot(cast, mousePosition);
                        this.lastShot = DateTime.Now;
                    }
                    else
                    {
                        for(int i = 0; i<weapon1.GetBulletType().GetBuckShot() + 1; i++)
                        {
                            player.Shoot(cast, mousePosition);
                        }
                        this.lastShot = DateTime.Now;
                    }
                }
                else if(mouseService.IsMouseDown()&fullAuto)
                {
                    Vector2 mousePosition = mouseService.GetMousePosition();
                    if(!isShotgun)
                    {
                        player.Shoot(cast, mousePosition);
                        this.lastShot = DateTime.Now;
                    }
                    else
                    {
                        for(int i = 0; i<weapon1.GetBulletType().GetBuckShot() + 1; i++)
                        {
                            player.Shoot(cast, mousePosition);
                            this.lastShot = DateTime.Now;
                        }
                    }
                }
            }

            if(keyboardService.RKeyPressed()&& !reload)
            {
                reloadTimeInit=DateTime.Now;
                Weapon weapon = player.GetHeldWeapon();
                this.reloadTime = weapon.GetReloadTime();
                this.reload = true;
            }

            TimeSpan reloadDuration = DateTime.Now - reloadTimeInit;

            if (reloadDuration.Seconds * 4>= this.reloadTime && this.reload)
            {
                if(!isShotgun)
                {
                    player.PlayerReload();
                }
                else
                {
                    player.PlayerShotgunReload();
                }
                this.reload = false;
            }

            if(keyboardService.VKeyPressed())
            {
                player.SetHeldWeapon(weapon2);
                player.SetStoredWeapon(weapon1);
                this.reload=false;                
            }
            
            bool topCollision = false;
            bool leftCollision = false;
            bool rightCollision = false;
            bool bottomCollision = false;

            foreach (Actor wallT in walls)
            {
                Wall wall = (Wall) wallT;
                collision = contactService.WallCollision(player,wall);
                if(collision)
                {
                    // topCollision = contactService.WallCollisionTop((Actor)player);
                    // leftCollision = contactService.WallCollisionLeft((Actor)player);
                    // rightCollision = contactService.WallCollisionRight((Actor)player);
                    // bottomCollision = contactService.WallCollisionTop((Actor)player);
                    // velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
                    // player.SetVelocity(velocity);
                    
                    if(contactService.WallCollisionLeft(player, wall))
                    {
                        leftCollision = true;
                    }
                    if(contactService.WallCollisionRight(player, wall))
                    {
                        rightCollision=true;
                    }
                    if(contactService.WallCollisionBottom(player, wall))
                    {
                        bottomCollision=true;
                    }
                    if(contactService.WallCollisionTop(player, wall))
                    {
                        topCollision=true;
                        // if(contactService.WallCollisionRight(player,wall))
                        // {
                        //     velocity = keyboardService.GetDirection(true,false,true,false);
                        //     player.SetVelocity(velocity);
                        // }
                        // else if(contactService.WallCollisionLeft(player,wall))
                        // {
                        //     velocity = keyboardService.GetDirection(true,true,false,false);
                        //     player.SetVelocity(velocity);
                        // }
                        // else
                        // {
                        //     velocity = keyboardService.GetDirection(true,false,false,false);
                        //     player.SetVelocity(velocity);
                        // }
                    }
                }
            }

            Vector2 velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
            player.SetVelocity(velocity/Program.PLAYER_SPEED_DIVIDER);

            Wall misteryBox = (Wall)cast.GetFirstActor("box");
            Vector2 position = misteryBox.GetPosition();
            Vector2 center = misteryBox.GetCenter(position);
            Random random = new Random();
            List<Actor> weapons = cast.GetActors("weapon");
            
            HUD promptHUD =(HUD) hUDs[2];

            float distance = Vector2.Distance(center, playerPosition);
            if (distance<50)
            {
                promptHUD.SetText("E for Weapon 950");
                if(keyboardService.EKeyPressed())
                {
                    Weapon mysteryWeapon = weapon1;
                    while(mysteryWeapon == weapon1 || mysteryWeapon == weapon2)
                    {
                        int randomInt=random.Next(0,weapons.Count);
                        mysteryWeapon = (Weapon) weapons[randomInt];
                    }
                    player.SetNewHeldWeapon(mysteryWeapon);
                    stats.SpendPoints(950);
                }
            }
            else
            {
                promptHUD.SetText("");
            }
        }

    }
}