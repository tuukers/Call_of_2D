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
    public class MoveActorsAction : Action
    {
        private VideoService videoService;
        private MouseService mouseService;

        /// <summary>
        /// Constructs a new instance of MoveActorsAction.
        /// </summary>
        public MoveActorsAction(VideoService videoService,MouseService mouseService)
        {
            this.videoService = videoService;
            this.mouseService = mouseService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            List<Actor> actors = cast.GetAllActors();
            List<Actor> walls = cast.GetActors("wall");
            Vector2 mousePosition = mouseService.GetMousePosition();

            foreach (Actor actor in actors)
            {
                actor.MoveNext();
            }

            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.DrawWalls(walls);
            videoService.DrawPointer(cast.GetFirstActor("player"), mousePosition);
            videoService.FlushBuffer();
        }
    }
}