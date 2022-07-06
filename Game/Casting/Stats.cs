namespace Callof2d.Game.Casting
{
    /// <summary>
    /// A thing that participates in the game.
    /// </summary>
    public class Stats : Actor
    {
        private int wave;
        private int lives;
        private int score;

        /// <summary>
        /// Constructs a new instance of Actor.
        /// </summary>
        public Stats(int wave = 1, int lives = 3, int score = 0, 
                bool debug = false)
        {
            this.wave = wave;
            this.lives = lives;
            this.score = score;
        }

        /// <summary>
        /// Adds one wave.
        /// </summary>
        public void AddWave()
        {
            wave++;
        }

        /// <summary>
        /// Adds an extra life.
        /// </summary>
        public void AddLife()
        {
            lives++;
        }

        /// <summary>
        /// Adds the given points to the score.
        /// </summary>
        /// <param name="points">The given points.</param>
        /// <param name="addend">Modifies number of points with addition/subtraction.</param>
        /// <param name="multiplier">Modifies number of points with multiplication.</param>
        public void AddPoints(int points, int addend, int multiplier)
        {
            score = (points + addend) * multiplier;
        }

        /// <summary>
        /// Gets the wave.
        /// </summary>
        /// <returns>The wave.</returns>
        public int GetWave()
        {
            return wave;
        }

        /// <summary>
        /// Gets the lives.
        /// </summary>
        /// <returns>The lives.</returns>
        public int GetLives()
        {
            return lives;
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>The score.</returns>
        public int GetScore()
        {
            return score;
        }

        /// <summary>
        /// Removes a life.
        /// </summary>
        public void RemoveLife()
        {
            lives--;
            if (lives <= 0)
            {
                lives = 0;
            }
        }
        
    }
}