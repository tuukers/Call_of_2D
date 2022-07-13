using System.Collections.Generic;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;
using System.Numerics;
using Callof2d.Game.Scripting;


namespace Callof2d.Game.Directing
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
        private Clock clock = null;
        private AudioService audioService = null;
        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService, MouseService mouseService, Clock clock)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
            this.mouseService = mouseService;
            this.clock = clock;
            this.audioService = audioService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast, Script script)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                ExecuteActions("inputs", cast, script);
                ExecuteActions("updates", cast, script);
                ExecuteActions("outputs", cast, script);
            }
            videoService.CloseWindow();
        }

        private void ExecuteActions(string group, Cast cast, Script script)
        {
            List<Action> actions = script.GetActions(group);
            foreach(Action action in actions)
            {
                action.Execute(cast, script);
            }
        }

        // /// <summary>
        // /// Gets directional input from the keyboard and applies it to the player.
        // /// </summary>
        // /// <param name="cast">The given cast.</param>
        // private void GetInputs(Cast cast)
        // {
        //     Actor player = cast.GetFirstActor("player");
        //     Vector2 velocity = keyboardService.GetDirection();
        //     player.SetVelocity(velocity);     
            
        //     if(mouseService.IsMousePressed())
        //     {
        //         Vector2 mousePosition = mouseService.GetMousePosition();

        //         Player player = (Player) player;
        //         player.Shoot(cast, mousePosition);
        //     }
        // }

        // /// <summary>
        // /// Updates the player's position and resolves any collisions with artifacts.
        // /// </summary>
        // /// <param name="cast">The given cast.</param>
        // private void DoUpdates(Cast cast)
        // {
        //     Actor banner = cast.GetFirstActor("banner");
        //     Actor player = cast.GetFirstActor("player");
        //     List<Actor> artifacts = cast.GetActors("artifacts");
        //     List<Actor> bullets = cast.GetActors("bullets");

        //     // Get player position x and y
        //     Vector2 playerPosition = player.GetPosition();


        //     // Updates zombie velocity to track player.
        //     foreach (Actor actor in artifacts) {
        //         Vector2 zombiePosition = actor.GetPosition();

        //         int maxX1 = videoService.GetWidth();
        //         int maxY1 = videoService.GetHeight();

        //         // Subtract player position from mouse position.
        //         Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

        //         // Normalize result so contains only direction, not magnitude.
        //         Vector2 normalized = Vector2.Normalize(a);

        //         actor.SetVelocity(normalized);
        //         actor.MoveNext(maxX1, maxY1);
        //     }


        //     banner.SetText("");
        //     int maxX = videoService.GetWidth();
        //     int maxY = videoService.GetHeight();
        //     player.MoveNext(maxX, maxY);

        //     foreach (Actor actor in artifacts)
        //     {
        //         if (player.GetPosition().Equals(actor.GetPosition()))
        //         {
        //             Artifact artifact = (Artifact) actor;
        //             string message = artifact.GetMessage();
        //             banner.SetText(message);
        //         }
        //     } 

        //     foreach (Actor bullet in bullets)
        //     {
        //         bullet.MoveNext(maxX, maxY);
        //         bool isInFrame = bullet.isInFrame(maxX, maxY);
        //         Console.WriteLine(isInFrame);

        //         if (!isInFrame) {
        //             cast.RemoveActor("bullets", bullet);
        //         }
        //     }
        // }

        // /// <summary>
        // /// Draws the actors on the screen.
        // /// </summary>
        // /// <param name="cast">The given cast.</param>
        // public void DoOutputs(Cast cast)
        // {
        //     List<Actor> actors = cast.GetAllActors();
        //     Vector2 mousePosition = mouseService.GetMousePosition();

        //     videoService.ClearBuffer();
        //     videoService.DrawActors(actors);
        //     videoService.DrawPointer(cast.GetFirstActor("player"), mousePosition);
        //     videoService.FlushBuffer();
        // }
    }
}