
using System;
using System.Collections.Generic;
using System.Numerics;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;



namespace Callof2d.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class MoveActorsAction : Action
    {
        private VideoService videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public MoveActorsAction(VideoService videoService)
        {
            this.videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> bullets = cast.GetActors("bullets");

            // Get player position x and y
            Vector2 playerPosition = player.GetPosition();


            // Updates zombie velocity to track player.
            // foreach (Actor actor in zombies) {
            //     Vector2 zombiePosition = actor.GetPosition();

            //     int maxX1 = videoService.GetWidth();
            //     int maxY1 = videoService.GetHeight();

            //     // Subtract player position from mouse position.
            //     Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

            //     // Normalize result so contains only direction, not magnitude.
            //     Vector2 normalized = Vector2.Normalize(a);

            //     actor.SetVelocity(normalized);


            //     //actor.MoveNext();
            // }

            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            //player.MoveNext();

            foreach (Actor actor in zombies)
            {
                Zombie zombie = (Zombie) actor;
                float health = zombie.GetHealth();
            } 

            foreach (Actor bullet in bullets)
            {
                bullet.MoveNext();
                bool isInFrame = bullet.isInFrame(maxX, maxY);

                if (!isInFrame) {
                    cast.RemoveActor("bullets", bullet);
                }
            }
        }
        
        
        
        
        // {
        //     Player player = (Player)cast.GetFirstActor("player");

            
        //     List<Actor> zombies = cast.GetActors("zombie");
        //     List<Actor> bullets = cast.GetActors("bullets");


            
        //     videoService.ClearBuffer();
        //     videoService.DrawActor(player);
        //     videoService.DrawActors(zombies);
        //     //videoService.DrawActor(winner);
        //     videoService.DrawActors(bullets);
        //     videoService.FlushBuffer();
        // }
    }
}