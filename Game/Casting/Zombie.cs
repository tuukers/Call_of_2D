using System;

namespace Callof2d.Game.Casting
{
    /// <summary>
    /// <para>The main enemy of the game. They will eat your brains!</para>
    /// <para>
    /// The responsibility of an Zombie is to chase the player.
    /// </para>
    /// </summary>
    public class Zombie : Actor
    {
        private float health;
        private Cast cast;
        private int speed;

        /// <summary>
        /// Constructs a new instance of an Zombie.
        /// </summary>
        public Zombie(int health)
        {
            this.health=health;
        }

        public void TakeDamage(int damage)
        {
            this.health -= damage;
        }

        public float GetHealth()
        {
            return this.health;
        }

        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }

        public int GetSpeed()
        {
            return this.speed;
        }

        public static void BasicAttack()
        {
            Console.WriteLine("BasicAttack performed");
        }
    }

    public class BloodSpray : Actor
    {
        public BloodSpray()
        {
        }
    }
}