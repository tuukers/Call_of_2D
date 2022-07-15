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
        private Round round =  null;
        private double roundEnd = 0;
        private int roundMultiplier = 0;
        private double lastSpawn = 0;
        private int roundZombies = 0;
        private int spawnedZombies = 0;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public SpawnZombiesAction(Clock clock, Round round)
        {
            this.clock = clock;       
            this.round = round;
        }

        /// <inheritdoc/>

        public void Execute(Cast cast, Script script)
        {
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> walls = cast.GetActors("wall");
            double difference = clock.GetLifeTime() - roundEnd;
            //Console.WriteLine(round.GetRound());

            if (zombies.Count == 0 && difference > 11)
            {
                roundEnd = clock.GetLifeTime();
                round.IncrementRound();
                roundMultiplier = 4 * (round.GetRound());
            }

            if (zombies.Count == 0 && clock.GetLifeTime() - roundEnd >= 10) {
                roundZombies = Program.DEFAULT_ZOMBIES + roundMultiplier;
                spawnedZombies = 0;
            }

            if (spawnedZombies < roundZombies)
            {
                SpawnZombies(cast, walls);
            }
        }

        public void SpawnZombies (Cast cast, List<Actor> walls) 
        {
            if (clock.GetLifeTime() - lastSpawn > 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Wall wall = (Wall)walls[i];

                    Vector2 position = wall.GetCenter(wall.GetPosition());
                    Console.WriteLine(position);

                    int r = 0;//random.Next(0, 256);
                    int g = 255;//random.Next(0, 256);
                    int b = 0;//random.Next(0, 256);
                    Color color = new Color(r, g, b);

                    Zombie zombie = new Zombie(Program.ZOMBIE_HEALTH);
                    zombie.SetColor(color);
                    zombie.SetPosition(position);
                    zombie.SetRadius(Program.ZOMBIE_RADIUS);
                    cast.AddActor("zombie", zombie);
                    lastSpawn = clock.GetLifeTime();
                    spawnedZombies++;
                }
            }
        }
    }
}