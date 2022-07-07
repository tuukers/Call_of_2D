using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;

namespace Callof2d.Game.Scripting;
public class HandleZombieZombieCollisionsAction : Action
    {

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        private ContactService contactService;
        private bool contact = false;
        public HandleZombieZombieCollisionsAction(ContactService contactService)
        {

        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor player = cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> bullets = cast.GetActors("bullets");
            Vector2 playerPosition = player.GetPosition(); 
            
            foreach(Actor zombie in zombies)
            {
                Vector2 zombiePosition = zombie.GetPosition();

                foreach(Actor otherZombie in zombies)
                {
<<<<<<< HEAD
                    contact=false;
                    contact=contactService.Collision(zombie,otherZombie);
                    Vector2 otherZombiePosition =  otherZombie.GetPosition();
                        if(contact)
=======
                    Vector2 otherZombiePosition=otherZombie.GetPosition();
                    if (!(otherZombiePosition==zombiePosition)){
                        float otherZombiePosition_X=otherZombiePosition.X;
                        float otherZombiePosition_Y=otherZombiePosition.Y;

                        float x_difference= zombiePosition_X-otherZombiePosition_X;
                        float y_difference= zombiePosition_Y-otherZombiePosition_Y;
                        float x_difference_abs=Math.Abs(x_difference);
                        float y_difference_abs=Math.Abs(y_difference);

                        if(x_difference_abs<20 && y_difference_abs<20)
>>>>>>> febcc86ba3afd88d5a07fca8aa5443895535b036
                        {
                            // Vector2 movingAway=otherZombiePosition - zombiePosition;
                            // zombie.SetVelocity(Vector2.Normalize(-movingAway));
                            // otherZombie.SetVelocity(Vector2.Normalize(movingAway));
                        }
                        else
                        {
                            Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

                            // Normalize result so contains only direction, not magnitude.
                            Vector2 normalized = Vector2.Normalize(a);

                            zombie.SetVelocity(normalized);
                        }
                    
                        
                }
            }

        }
    }
