using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Actor is to keep track of its appearance, position and velocity in 2d 
    /// space.
    /// </para>
    /// </summary>
    public class Actor
    {
        private Color color = new Color(255, 255, 255); // white
        private Vector2 position = new Vector2(0, 0);
        private Vector2 velocity = new Vector2(0, 0);
        private float radius;
        private string text;


        /// <summary>
        /// Constructs a new instance of Actor.
        /// </summary>
        public Actor()
        {

        }

        /// <summary>
        /// Gets the actor's color.
        /// </summary>
        /// <returns>The color.</returns>
        public Color GetColor()
        {
            return color;
        }

        /// <summary>
        /// Gets the actor's position.
        /// </summary>
        /// <returns>The position.</returns>
        public Vector2 GetPosition()
        {
            return position;
        }

        /// <summary>
        /// Gets the actor's current velocity.
        /// </summary>
        /// <returns>The velocity.</returns>
        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public float GetRadius() 
        {   
            return radius;
        }

        public string GetText()
        {
            return text;
        }

        /// <summary>
        /// Moves the actor to its next position according to its velocity. Will wrap the position 
        /// from one side of the screen to the other when it reaches the maximum x and y 
        /// values.
        /// </summary>
        /// <param name="maxX">The maximum x value.</param>
        /// <param name="maxY">The maximum y value.</param>
        public void MoveNext()
        {
            // float x = (position.X + velocity.X + maxX) % maxX; 
            // float y = (position.Y + velocity.Y + maxY) % maxY;

            Vector2 newPosition = Vector2.Add(position, velocity);

            this.position = newPosition;
            // new Vector2(x,y)
        }

        public bool isInFrame(int maxX, int maxY) {
            if (position.X < 0 || position.X > maxX || position.Y < 0 || position.Y > maxY)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Sets the actor's color to the given value.
        /// </summary>
        /// <param name="color">The given color.</param>
        /// <exception cref="ArgumentException">When color is null.</exception>
        public void SetColor(Color color)
        {
            if (color == null)
            {
                throw new ArgumentException("color can't be null");
            }
            this.color = color;
        }


        /// <summary>
        /// Sets the actor's position to the given value.
        /// </summary>
        /// <param name="position">The given position.</param>
        /// <exception cref="ArgumentException">When position is null.</exception>
        public void SetPosition(Vector2 position)
        {
            if (position == null)
            {
                throw new ArgumentException("position can't be null");
            }
            this.position = position;
        }

        /// <summary>
        /// Sets the actor's velocity to the given value.
        /// </summary>
        /// <param name="velocity">The given velocity.</param>
        /// <exception cref="ArgumentException">When velocity is null.</exception>
        public void SetVelocity(Vector2 velocity)
        {
            if (velocity == null)
            {
                throw new ArgumentException("velocity can't be null");
            }
            this.velocity = velocity;
        }

        public void SetRadius(float radius) 
        {
            this.radius = radius;
        }

        public void SetText(string text)
        {
            this.text = text;
        }

        
    }
}