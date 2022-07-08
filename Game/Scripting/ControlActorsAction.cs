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
        private bool topCollision = false;
        private bool leftCollision = false;
        private bool rightCollision = false;
        private bool bottomCollision = false;


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
            Vector2 playerPosition = player.GetPosition();
            Vector2 velocity = keyboardService.GetDirection(false,false,false,false);
            player.SetVelocity(velocity);
            
            
            if(mouseService.IsMousePressed())
            {
                Vector2 mousePosition = mouseService.GetMousePosition();

                player.Shoot(cast, mousePosition);
            }
            
            foreach (Actor wallT in walls)
            {
                Wall wall = (Wall) wallT;
                collision = contactService.WallCollision(player,wall);
                if(collision)
                {
                    topCollision = contactService.WallCollisionTop((Actor)player);
                    leftCollision = contactService.WallCollisionLeft((Actor)player);
                    rightCollision = contactService.WallCollisionRight((Actor)player);
                    bottomCollision = contactService.WallCollisionTop((Actor)player);
                    velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
                    player.SetVelocity(velocity);
                }
            }

            // foreach (Actor wallT in walls)
            //         {
            //             Wall wall= (Wall)wallT;
            //             bool topCollision = contactService.WallCollisionTop(player);
            //             if(topCollision)
            //             {
            //                 Console.WriteLine("colided");
            //             }
            //             bool leftCollision = contactService.WallCollisionLeft(player);
            //             bool rightCollision = contactService.WallCollisionRight(player);
            //             bool bottomCollision = contactService.WallCollisionTop(player);
            //             // if(collision)
            //             // {
            //             //     velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
            //             //     player.SetVelocity(velocity);
            //             // }



            //             Vector2 wallPosition = wall.GetPosition(); 
            //             Vector2 wallCenter = wall.GetCenter(wallPosition);
            //             collision=contactService.WallCollision(player,wall);
            //             bool collisionTopRight=contactService.WallCollisionTop(player) & contactService.WallCollisionRight(player);
            //             bool collisionTopLeft=contactService.WallCollisionTop(player) & contactService.WallCollisionLeft(player);
            //             bool collisionBottomRight=contactService.WallCollisionBottom(player) & contactService.WallCollisionRight(player);
            //             bool collisionBottomLeft=contactService.WallCollisionBottom(player) & contactService.WallCollisionLeft(player);
            //             if(collision)
            //             {
            //                 // Console.WriteLine("colided"); //collision testing
            //                 if(wall.GetHorizontal())
            //                 {
            //                     if(playerPosition.Y>wallCenter.Y)
            //                     {
            //                         velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
            //                         // if()
            //                         // {

            //                         // }
            //                         player.SetVelocity(velocity);
            //                     }
            //                     else
            //                     {
            //                         velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
            //                         player.SetVelocity(velocity);
            //                     }
            //                 }
            //                 else
            //                 {
            //                     if(playerPosition.X > wallCenter.X)
            //                     {
            //                         velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
            //                         player.SetVelocity(velocity);
            //                     }
            //                     else
            //                     {
            //                         velocity = keyboardService.GetDirection(topCollision,leftCollision,rightCollision,bottomCollision);
            //                         player.SetVelocity(velocity);
            //                     }
            //                 }
            //             }
            //         }
        }

    }
}