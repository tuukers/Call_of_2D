using System;

namespace Callof2d.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
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
            Player.PlayerTakeDamage(20);
            Console.WriteLine("BasicAttack performed");
        }
    }
}