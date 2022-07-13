using System;

namespace Callof2d.Game.Casting
{
    /// <summary>
    /// A thing that participates in the game.
    /// </summary>
    public class Stats : Actor
    {
        private int round;
        private int lives;
        private int score = 0;

        /// <summary>
        /// Constructs a new instance of Actor.
        /// </summary>
        public Stats(int round = 1, int lives = 3, int score = 0, 
                bool debug = false)
        {
            this.round = round;
            this.lives = lives;
            this.score = score;
        }

        /// <summary>
        /// Adds one round.
        /// </summary>
        public void AddRound()
        {
            round++;
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
        /// <param name="multiplier">Modifies number of points with multiplication.</param>
        public void AddPoints(int points, int multiplier)
        {
            score += points * multiplier;
        }

        public void SpendPoints(int points)
        {
            score -= points;
        }

        /// <summary>
        /// Gets the round.
        /// </summary>
        /// <returns>The round.</returns>
        public int GetRound()
        {
            return round;
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