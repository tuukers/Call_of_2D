using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;
using System;
using System.Numerics;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        private MouseService mouseService = null;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService, MouseService mouseService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
            this.mouseService = mouseService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Vector2 velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);     
            
            if(mouseService.IsMousePressed())
            {
                Vector2 mousePosition = mouseService.GetMousePosition();

                Player player = (Player) robot;
                player.Shoot(cast, mousePosition);
            }
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> artifacts = cast.GetActors("artifacts");
            List<Actor> bullets = cast.GetActors("bullets");

            // Get robot position x and y
            Vector2 robotPosition = robot.GetPosition();


            // Updates zombie velocity to track player.
            foreach (Actor actor in artifacts) {
                Vector2 zombiePosition = actor.GetPosition();

                int maxX1 = videoService.GetWidth();
                int maxY1 = videoService.GetHeight();

                // Subtract player position from mouse position.
                Vector2 a = Vector2.Subtract(robotPosition, zombiePosition);

                // Normalize result so contains only direction, not magnitude.
                Vector2 normalized = Vector2.Normalize(a);

                actor.SetVelocity(normalized);
                actor.MoveNext(maxX1, maxY1);
            }


            banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in artifacts)
            {
                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                    Artifact artifact = (Artifact) actor;
                    string message = artifact.GetMessage();
                    banner.SetText(message);
                }
            } 

            foreach (Actor bullet in bullets)
            {
                bullet.MoveNext(maxX, maxY);
                bool isInFrame = bullet.isInFrame(maxX, maxY);
                Console.WriteLine(isInFrame);

                if (!isInFrame) {
                    cast.RemoveActor("bullets", bullet);
                }
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            Vector2 mousePosition = mouseService.GetMousePosition();

            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.DrawPointer(cast.GetFirstActor("robot"), mousePosition);
            videoService.FlushBuffer();
        }
    }
}