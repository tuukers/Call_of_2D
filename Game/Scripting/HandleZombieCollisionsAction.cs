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
        public HandleZombieZombieCollisionsAction(ContactService contactService)
        {

        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor player = cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            Vector2 playerPosition = player.GetPosition(); 
            
            for (int i = 0; i < zombies.Count; i++)
            {
                Vector2 zombiePosition = zombies[i].GetPosition();

                for (int i2 = i + 1; i2 < zombies.Count; i2++)
                {
                    //contact=contactService.Collision(zombie,otherZombie);
                    float radius1 = zombies[i].GetRadius();
                    float radius2 = zombies[i2].GetRadius();

                    Vector2 actor1Position = zombies[i].GetPosition();
                    Vector2 actor2Position = zombies[i2].GetPosition();

                    Vector2 vector = actor1Position - actor2Position;
                    float distance = Vector2.Distance(actor1Position,actor2Position);

                    Vector2 otherZombiePosition =  zombies[i2].GetPosition(); //I copied it in and it works. I don't know why

                    if(distance <= radius1 + radius2)
                    {
                        Vector2 movingAway=otherZombiePosition - zombiePosition;
                        zombies[i].SetVelocity(Vector2.Normalize(-movingAway));
                        zombies[i2].SetVelocity(Vector2.Normalize(movingAway));
                    }
                    else{
                        Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

                        // Normalize result so contains only direction, not magnitude.
                        Vector2 normalized = Vector2.Normalize(a);
                        float zombieDivideSpeed = Program.ZOMBIE_NORMAL_SPEED_DIVIDE;
                        Vector2 velocity = Vector2.Divide(normalized,zombieDivideSpeed);

                        zombies[i].SetVelocity(velocity/Program.ZOMBIE_NORMAL_SPEED_DIVIDE);
                    }  
                }
                if (i == zombies.Count - 1)
                {
                    Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

                        // Normalize result so contains only direction, not magnitude.
                        Vector2 normalized = Vector2.Normalize(a);
                        float zombieDivideSpeed = Program.ZOMBIE_NORMAL_SPEED_DIVIDE;
                        Vector2 velocity = Vector2.Divide(normalized,zombieDivideSpeed);

                        zombies[i].SetVelocity(velocity/Program.ZOMBIE_NORMAL_SPEED_DIVIDE);
                }
            }
        }
    }
