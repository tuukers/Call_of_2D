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
            Vector2 playerPosition = player.GetPosition(); 
            
            foreach(Actor zombie in zombies)
            {
                Vector2 zombiePosition = zombie.GetPosition();

                foreach(Actor otherZombie in zombies)
                {
                        contact=false;
                        contact=contactService.Collision(zombie,otherZombie);
                        Vector2 otherZombiePosition =  otherZombie.GetPosition();
                            if(contact)
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
