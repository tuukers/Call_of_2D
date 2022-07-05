using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using Callof2d.Game.Casting;

namespace Callof2d.Game.Scripting;
public class HandleZombieZombieCollisionsAction : Action
    {

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleZombieZombieCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor player = cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> bullets = cast.GetActors("bullets");
            Vector2 playerPosition = player.GetPosition();
            
            foreach(Zombie zombie in zombies)
            {
                Vector2 zombiePosition=zombie.GetPosition();
                float zombiePosition_X=zombiePosition.X;
                float zombiePosition_Y=zombiePosition.Y;

                foreach(Actor otherZombie in zombies)
                {
                    Vector2 otherZombiePosition=otherZombie.GetPosition();
                    if (!(otherZombiePosition==zombiePosition)){
                        float otherZombiePosition_X=otherZombiePosition.X;
                        float otherZombiePosition_Y=otherZombiePosition.Y;

                        float x_difference= zombiePosition_X-otherZombiePosition_X;
                        float y_difference= zombiePosition_Y-otherZombiePosition_Y;
                        float x_difference_abs=Math.Abs(x_difference);
                        float y_difference_abs=Math.Abs(y_difference);

                        if(x_difference_abs<10 && y_difference_abs<10)
                        {
                            Vector2 movingAway=otherZombiePosition - zombiePosition;
                            zombie.SetVelocity(Vector2.Normalize(-movingAway));
                            otherZombie.SetVelocity(Vector2.Normalize(movingAway));
                        }
                        else{
                            Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

                            // Normalize result so contains only direction, not magnitude.
                            Vector2 normalized = Vector2.Normalize(a);

                            zombie.SetVelocity(normalized);
                        }
                    }
                        
                }
            }

        }
    }
