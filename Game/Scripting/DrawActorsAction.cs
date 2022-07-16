using System;
using System.Collections.Generic;
using System.Numerics;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;


namespace Callof2d.Game.Scripting
{
    /// <summary>
    /// <para>An update action that moves all the actors.</para>
    /// <para>
    /// The responsibility of MoveActorsAction is to move all the actors.
    /// </para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService videoService;
        private MouseService mouseService;

        /// <summary>
        /// Constructs a new instance of MoveActorsAction.
        /// </summary>
        public DrawActorsAction(VideoService videoService,MouseService mouseService)
        {
            this.videoService = videoService;
            this.mouseService = mouseService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            // Step 1: Create a list for each desired "layer" to be drawn.
            List<Actor> actors = cast.GetAllActors();
            List<Actor> bloodSprays = cast.GetActors("bloodSpray");
            List<Actor> ammoBoxes = cast.GetActors("ammoBox");
            List<Actor> walls = cast.GetActors("wall");
            List<Actor> player = cast.GetActors("player");
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> bullets = cast.GetActors("bullets");
            List<Actor> hUDs = cast.GetActors("HUD");
            List<Actor> weaponBuys = cast.GetActors("box");
            List<Actor> background = cast.GetActors("background");
            Actor healthbar = cast.GetFirstActor("healthbar");
            Vector2 mousePosition = mouseService.GetMousePosition();
            

            foreach (Actor actor in actors)
            {
                actor.MoveNext();
            }


            // Step 2: Draw each list of actors in the desired order. Lists drawn last will show on top of the preceeding list.
            videoService.ClearBuffer();
            videoService.DrawBackground();
            // videoService.DrawWalls(background);
            videoService.DrawActors(bloodSprays);
            videoService.DrawWalls(weaponBuys);
            videoService.DrawActors(ammoBoxes);
            //videoService.DrawActors(actors);// draws all actors. Disabled because it doesn't draw them in the desired order.
            videoService.DrawActors(bullets);
            videoService.DrawActors(zombies);
            videoService.DrawActors(player);
            videoService.DrawWalls(walls);
            videoService.DrawHUDs(hUDs);
            videoService.DrawWall(healthbar);
            
            videoService.DrawPointer(cast.GetFirstActor("player"), mousePosition);
            videoService.FlushBuffer();
        }
    }
}