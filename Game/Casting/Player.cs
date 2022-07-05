using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    public class Player : Actor
    {

        public Player() 
        { 
        }

        public void Shoot(Cast cast, Vector2 mousePosition)
        {
            // Construct bullet.
            Bullet bullet = new Bullet();

            // Set bullet position to player position.
            bullet.SetPosition(this.GetPosition());

            // Get player postion to calculate firing direction.
            Vector2 pointPosition = this.GetPosition();

            // Subtract player position from mouse position.
            Vector2 a = Vector2.Subtract(mousePosition, pointPosition);

            // Normalize result so contains only direction, not magnitude.
            Vector2 normalized = Vector2.Normalize(a);
            normalized = Vector2.Multiply(normalized, 10);

            // Finish bullet.
            bullet.SetVelocity(normalized);
            bullet.SetText("-");
            bullet.SetColor(this.GetColor());

            // Add bullet to actors so it is displayed.
            cast.AddActor("bullets", bullet);

        }
    }
}