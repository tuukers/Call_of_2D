using System;
using System.Numerics;
using System.Collections.Generic;
using System.Data;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;
using System.Threading.Tasks;


namespace Callof2d.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when a bullet 
    /// collides with it a zombie or wall.
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
            this.stats = stats;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Actor player = cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            List<Actor> bullets = cast.GetActors("bullets");
            List<Actor> walls = cast.GetActors("wall");
            HUD hUD = (HUD)cast.GetFirstActor("scoreHUD");


            foreach (Zombie zombie in zombies)
            {
                Vector2 zombiePosition = zombie.GetPosition();
                float zombiePosition_X = zombiePosition.X;
                float zombiePosition_Y = zombiePosition.Y;

                foreach (Actor bullet in bullets)
                {
                    Vector2 bulletPosition = bullet.GetPosition();
                    Bullet shot = (Bullet)bullet;
                    float bulletPosition_X = bulletPosition.X;
                    float bulletPosition_Y = bulletPosition.Y;

                    float x_difference = zombiePosition_X - bulletPosition_X;
                    float y_difference = zombiePosition_Y - bulletPosition_Y;
                    float x_difference_abs = Math.Abs(x_difference);
                    float y_difference_abs = Math.Abs(y_difference);

                    if (x_difference_abs < 10 && y_difference_abs < 10)
                    {
                        // Zombie Hit
                        cast.RemoveActor("bullets", bullet);
                        zombie.TakeDamage(shot.GetBulletDamage());
                        stats.AddPoints(Program.POINTS_PER_HIT, 1);

                        // Blood spray
                        Random random = new Random();

                        BloodSpray bloodSpray = new BloodSpray();
                        bloodSpray.SetColor(Program.RED);
                        bloodSpray.SetPosition(zombie.GetPosition());
                        bloodSpray.SetRadius(Program.ZOMBIE_RADIUS / random.Next(2, 5));
                        cast.AddActor("bloodSpray", bloodSpray);
                        Vector2 sprayStart = bloodSpray.GetPosition();
                        float sprayEndX = zombiePosition_X + random.Next(-10, 11);
                        float sprayEndY = zombiePosition_Y + random.Next(-10, 11);
                        Vector2 sprayEnd = new Vector2(sprayEndX, sprayEndY);

                        Vector2 a = Vector2.Subtract(sprayEnd, sprayStart);

                        // Normalize result so contains only direction, not magnitude.
                        Vector2 normalized = Vector2.Normalize(a);
                        BloodSpray bloodSprays = (BloodSpray)bloodSpray;

                        bloodSpray.SetVelocity(normalized * 3);
                        SprayStop(bloodSpray, cast);
                    }

                    if (zombie.GetHealth() <= 0)
                    {
                        // Zombie Killed

                        // Drop ammo box
                        //AmmoBox.SpawnAmmoBox(zombie.GetPosition());                            
                        Random ammoChance = new Random();
                        int ammoRandom = ammoChance.Next(0, 11);
                        if (ammoRandom > 9)
                        {
                            AmmoBox ammoBox = new AmmoBox();
                            ammoBox.SetPosition(zombie.GetPosition());
                            ammoBox.SetRadius(Program.ZOMBIE_RADIUS / 2);
                            cast.AddActor("ammoBox", ammoBox);
                        }

                        // Remove zombie and add point
                        cast.RemoveActor("zombie", zombie);
                        stats.AddPoints(Program.POINTS_PER_KILL, 1);

                    }

                }
            }

            foreach (Actor bullet in bullets)
            {
                foreach (Actor wallT in walls)
                {
                    Wall wall = (Wall)wallT;
                    collision = contactService.WallCollision(bullet, wall);
                    if (collision)
                    {
                        cast.RemoveActor("bullets", bullet);
                    }
                }
            }
        }

        public async void SprayStop(BloodSpray bloodSpray, Cast cast)
        {
            // After 0.2 seconds, make the blood stop in it's place
            await Task.Delay(200);
            bloodSpray.SetVelocity(bloodSpray.GetVelocity() - bloodSpray.GetVelocity());
            bloodSpray.SetColor(Program.DARK_RED);
            Random random = new Random();

            await Task.Delay(random.Next(20000, 30001));
            bloodSpray.SetColor(Program.DARKER_RED);
            await Task.Delay(random.Next(20000, 30001));
            bloodSpray.SetColor(Program.DARKEST_RED);

            // After between 3 and 45 seconds, remove the blood splatter.
            // Note: There is an issue in the following commented out code when removing too many actors at the same time where the cast seems to become a null value and it crashes the game.
            // This mostly happens when using the MG42 and trench guns, since they get so many hits in a short period of time.
            // The high variance in blood despawn time actually mitigates this, but the crash seems to be inevitable.
            // Unless a permanent fix is found, the blood will have to stay.
            
            
            // int stayTime = random.Next(3000, 45001);
            // await Task.Delay(stayTime);
            // cast.RemoveActor("bloodSpray", bloodSpray);
        }
    }
}