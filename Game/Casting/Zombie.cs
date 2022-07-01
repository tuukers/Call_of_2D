namespace Callof2d.Game.Casting
{
    /// <summary>
    /// <para>An item of cultural or historical interest.</para>
    /// <para>
    /// The responsibility of an Zombie is to provide a message about itself.
    /// </para>
    /// </summary>
    public class Zombie : Actor
    {
        private string message = "";

        /// <summary>
        /// Constructs a new instance of an Zombie.
        /// </summary>
        public Zombie()
        {
        }

        /// <summary>
        /// Gets the zombie's message.
        /// </summary>
        /// <returns>The message.</returns>
        public string GetMessage()
        {
            return message;
        }

        /// <summary>
        /// Sets the zombie's message to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
        public void SetMessage(string message)
        {
            this.message = message;
        }
    }
}