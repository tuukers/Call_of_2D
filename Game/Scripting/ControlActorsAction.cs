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
        private Point direction = new Point(0,-Program.CELL_SIZE);
        private Point direction2 = new Point(0,-Program.CELL_SIZE);

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService, MouseService mouseService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.mouseService = mouseService;
            this.videoService = videoService;
        }

        /// <inheritdoc/>

        public void Execute(Cast cast, Script script)
        {
            Player player = (Player)cast.GetFirstActor("player");
            Vector2 velocity = keyboardService.GetDirection();
            player.SetVelocity(velocity);     
            
            if(mouseService.IsMousePressed())
            {
                Vector2 mousePosition = mouseService.GetMousePosition();

                player.Shoot(cast, mousePosition);
            }
        }




        // {
        //     Player player = (Player)cast.GetFirstActor("player");
        //     Vector2 velocity = keyboardService.GetDirection();
        //     player.SetVelocity(velocity);     
            
        //     if(mouseService.IsMousePressed())
        //     {
        //         Vector2 mousePosition = mouseService.GetMousePosition();

        //         player.Shoot(cast, mousePosition);
        //     }

        //     List<Actor> zombies = cast.GetActors("zombies");
        //     List<Actor> bullets = cast.GetActors("bullets");

        //     // Get player position x and y
        //     Vector2 playerPosition = player.GetPosition();


        //     // Updates zombie velocity to track player.
        //     foreach (Actor actor in zombies) {
        //         Vector2 zombiePosition = actor.GetPosition();

        //         int maxX1 = videoService.GetWidth();
        //         int maxY1 = videoService.GetHeight();

        //         // Subtract player position from mouse position.
        //         Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

        //         // Normalize result so contains only direction, not magnitude.
        //         Vector2 normalized = Vector2.Normalize(a);

        //         actor.SetVelocity(normalized);
        //     }

        //     foreach (Actor bullet in bullets)
        //     {
        //         int maxX = videoService.GetWidth();
        //         int maxY = videoService.GetHeight();
        //         bullet.MoveNext();
        //         bool isInFrame = bullet.isInFrame(maxX, maxY);
        //         Console.WriteLine(isInFrame);

        //         if (!isInFrame) {
        //             cast.RemoveActor("bullets", bullet);
        //         }
        //     }
        // }
    }
}