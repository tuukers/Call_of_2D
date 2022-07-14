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
            List<Actor> actors = cast.GetAllActors();
            List<Actor> walls = cast.GetActors("wall");
            List<Actor> hUDs = cast.GetActors("HUD");
            List<Actor> weaponBuys = cast.GetActors("box");
            List<Actor> background = cast.GetActors("background");
            Vector2 mousePosition = mouseService.GetMousePosition();

            foreach (Actor actor in actors)
            {
                actor.MoveNext();
            }

            videoService.ClearBuffer();
            videoService.DrawWalls(background);
            videoService.DrawWalls(weaponBuys);
            videoService.DrawActors(actors);
            videoService.DrawWalls(walls);
            videoService.DrawHUDs(hUDs);
            videoService.DrawPointer(cast.GetFirstActor("player"), mousePosition);
            videoService.FlushBuffer();
        }
    }
}