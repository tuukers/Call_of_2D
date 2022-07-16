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
        /// Constructs a new instance of HandleZombieCollisionsAction.
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
            int i = 0;
            
            foreach (Actor zombie in zombies)
            {
                Vector2 zombiePosition = zombie.GetPosition();
                i+=1;

                foreach (Actor zombie2 in zombies)
                {
                    //contact=contactService.Collision(zombie,otherZombie);
                    float radius1 = zombie.GetRadius();
                    float radius2 = zombie2.GetRadius();

                    Vector2 actor1Position = zombie.GetPosition();
                    Vector2 actor2Position = zombie2.GetPosition();
                    float distance = Vector2.Distance(actor1Position,actor2Position);

                    if(distance <= radius1 + radius2 && zombie!=zombie2)
                    {
                        Vector2 movingAway=actor2Position - actor1Position;
                        zombie.SetVelocity(Vector2.Normalize(-movingAway));
                        zombie2.SetVelocity(Vector2.Normalize(movingAway));
                    }
                    else{
                        Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

                        // Normalize result so contains only direction, not magnitude.
                        Vector2 normalized = Vector2.Normalize(a);
                        Zombie zombiez = (Zombie) zombie;

                        zombie.SetVelocity(normalized * zombiez.GetSpeed());
                    }  
                }
                if (i == zombies.Count - 1)
                {
                    Vector2 a = Vector2.Subtract(playerPosition, zombiePosition);

                    // Normalize result so contains only direction, not magnitude.
                    Vector2 normalized = Vector2.Normalize(a);
                    Zombie zombie1 = (Zombie) zombie;
                    zombie1.SetVelocity(normalized*zombie1.GetSpeed());
                }
            }
        }
        Random random = new Random();
        int zombieSpread = Program.ZOMBIE_SPREAD;
        public Vector2 Spread(Vector2 aimVector)
        {
            Vector2 longVector = aimVector * 60;
            int xShift = random.Next(0,zombieSpread+1);
            int yShift = random.Next(0,zombieSpread+1);
            int xPosition = (int)longVector.X;
            int yPosition = (int)longVector.Y;
            int x = xPosition-zombieSpread/2 +xShift;
            int y = yPosition-zombieSpread/2 +yShift;
            Vector2 bulletLongVector = new Vector2(x,y);
            Vector2 bulletVector = Vector2.Normalize(bulletLongVector);
            return bulletVector;
        }
    }
