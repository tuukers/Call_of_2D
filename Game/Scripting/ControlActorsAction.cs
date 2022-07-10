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
        private Point direction = new Point(0,-Program.CELL_SIZE);
        private Point direction2 = new Point(0,-Program.CELL_SIZE);
        private bool collision = false;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService, MouseService mouseService, VideoService videoService, ContactService contactService)
        {
            this.keyboardService = keyboardService;
            this.mouseService = mouseService;
            this.videoService = videoService;
            this.contactService = contactService;            
        }

        /// <inheritdoc/>

        public void Execute(Cast cast, Script script)
        {
            Player player = (Player)cast.GetFirstActor("player");
            List<Actor> walls = cast.GetActors("wall");
            HUD hud = (HUD)cast.GetFirstActor("HUD");
            Vector2 playerPosition = player.GetPosition();
            
            
            
            if(mouseService.IsMousePressed())
            {
                Vector2 mousePosition = mouseService.GetMousePosition();

                player.Shoot(cast, mousePosition);
                hud.WeaponHUD();
            }

            if(keyboardService.RKeyPressed())
            {
                Weapon weapon = player.GetHeldWeapon();
                player.PlayerReload();
                hud.WeaponHUD();
            }

            if(keyboardService.VKeyPressed())
            {
                Weapon weapon1 = player.GetHeldWeapon();
                Weapon weapon2 = player.GetStoredWeapon();
                player.SetHeldWeapon(weapon2);
                player.SetStoredWeapon(weapon1);
                hud.WeaponHUD();
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
                    else if(contactService.WallCollisionRight(player, wall))
                    {
                        rightCollision=true;
                    }
                    else if(contactService.WallCollisionBottom(player, wall))
                    {
                        bottomCollision=true;
                    }
                    else if(contactService.WallCollisionTop(player, wall))
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
            player.SetVelocity(velocity);
        }

    }
}