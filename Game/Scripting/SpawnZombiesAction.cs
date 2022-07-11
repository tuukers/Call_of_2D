using System;
using System.Collections.Generic;
using System.Numerics;
using Callof2d.Game.Casting;
using Callof2d.Game.Directing;
using Callof2d.Game.Services;

namespace Callof2d.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the snake.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the snake's head.
    /// </para>
    /// </summary>
    public class SpawnZombiesAction : Action
    {
        private Clock clock = null;
        private double roundEnd = 0;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public SpawnZombiesAction(Clock clock)
        {
            this.clock = clock;          
        }

        /// <inheritdoc/>

        public void Execute(Cast cast, Script script)
        {
            List<Actor> zombies = cast.GetActors("zombie");
            double difference = clock.GetLifeTime() - roundEnd;

            if (zombies.Count == 0 && difference > 11)
            {
                roundEnd = clock.GetLifeTime();
            }

            if (zombies.Count == 0 && clock.GetLifeTime() - roundEnd >= 10) {
                Random random = new Random();
                for (int i = 0; i<Program.DEFAULT_ZOMBIES; i++) {
                    int x = random.Next(1, Program.MAX_X);
                    int y = random.Next(1, Program.MAX_Y);
                    Vector2 position = new Vector2(x, y);

                    int r = 0;//random.Next(0, 256);
                    int g = 255;//random.Next(0, 256);
                    int b = 0;//random.Next(0, 256);
                    Color color = new Color(r, g, b);

                    Zombie zombie = new Zombie(Program.ZOMBIE_HEALTH);
                    zombie.SetColor(color);
                    zombie.SetPosition(position);
                    zombie.SetRadius(Program.ZOMBIE_RADIUS);
                    cast.AddActor("zombie", zombie);
                }
            }
        }
    }
}