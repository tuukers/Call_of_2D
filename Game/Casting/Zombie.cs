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
        private float health = 5;
        private Cast cast;

        /// <summary>
        /// Constructs a new instance of an Zombie.
        /// </summary>
        public Zombie()
        {
            this.health=5;
        }

        public void TakeDamage(int bulletDamage)
        {
            this.health-=1;
        }

        public float GetHealth()
        {
            return this.health;
        }
    }
}