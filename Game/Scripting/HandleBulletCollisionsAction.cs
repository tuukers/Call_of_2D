using System;
using System.Numerics;
using System.Collections.Generic;
using System.Data;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;


namespace Callof2d.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleBulletCollisionsAction : Action
    {

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        private ContactService contactService;
        bool collision = false;
        private Stats stats;
        public HandleBulletCollisionsAction(ContactService contactService, Stats stats)
        {
            this.contactService = contactService;
            this.stats =stats;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor player = cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> bullets = cast.GetActors("bullets");
            List<Actor> walls = cast.GetActors("wall");
            HUD hUD = (HUD)cast.GetFirstActor("scoreHUD");
            
            foreach(Zombie zombie in zombies)
            {
                Vector2 zombiePosition=zombie.GetPosition();
                float zombiePosition_X=zombiePosition.X;
                float zombiePosition_Y=zombiePosition.Y;

                foreach(Actor bullet in bullets)
                {
                    Vector2 bulletPosition=bullet.GetPosition();
                    Bullet shot = (Bullet)bullet;
                    float bulletPosition_X=bulletPosition.X;
                    float bulletPosition_Y=bulletPosition.Y;

                    float x_difference= zombiePosition_X-bulletPosition_X;
                    float y_difference= zombiePosition_Y-bulletPosition_Y;
                    float x_difference_abs=Math.Abs(x_difference);
                    float y_difference_abs=Math.Abs(y_difference);

                    if(x_difference_abs<10 && y_difference_abs<10)
                    {
                        // Zombie Hit
                        cast.RemoveActor("bullets",bullet);
                        zombie.TakeDamage(shot.GetBulletDamage());
                        stats.AddPoints(Program.POINTS_PER_HIT,1);

                        if (zombie.GetHealth()<=0){
                            // Zombie Killed

                            // Drop ammo box
                            //AmmoBox.SpawnAmmoBox(zombie.GetPosition());                            
                            Random random = new Random();
                            int ammoRandom = random.Next(0, 11);
                            if (ammoRandom > 9)
                            {
                                AmmoBox ammoBox = new AmmoBox();
                                ammoBox.SetPosition(zombie.GetPosition());
                                ammoBox.SetRadius(Program.ZOMBIE_RADIUS / 2);
                                cast.AddActor("ammoBox", ammoBox);
                            }
                            
                            // Remove zombie and add point
                            cast.RemoveActor("zombie",zombie);
                            stats.AddPoints(Program.POINTS_PER_KILL,1);
                    
                        }
                    }
                }
            }
            
            foreach(Actor bullet in bullets)
            {
                for (int i=0; i<walls.Count-2;i++)
                {
                    Wall wall= (Wall)walls[i];
                    collision=contactService.WallCollision(bullet,wall);
                    if(collision)
                    {
                        cast.RemoveActor("bullets",bullet);
                    }
                }
            }
        }
    }
}