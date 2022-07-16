using System;

namespace Callof2d.Game.Casting
{
    /// <summary>
    /// A thing that participates in the game.
    /// </summary>
    public class Stats : Actor
    {
        private int round;
        private int score = 0;

        /// <summary>
        /// Constructs a new instance of Actor.
        /// </summary>
        public Stats(int round = 1, int score = 500, 
                bool debug = false)
        {
            this.round = round;
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
        /// Gets the score.
        /// </summary>
        /// <returns>The score.</returns>
        public int GetScore()
        {
            return score;
        }
        
    }
}