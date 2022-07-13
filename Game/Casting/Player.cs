using System;
using System.Numerics;
using Callof2d.Game;


namespace Callof2d.Game.Casting
{
    public class Player : Actor
    {
        public static int playerHealth = 100;
        private Weapon heldWeapon;
        private Weapon storedWeapon;

        public Player() 
        { 
        }
        

        public void SetHeldWeapon(Weapon weapon)
        {
            this.heldWeapon= weapon;
        }

        public void SetStoredWeapon(Weapon weapon)
        {
            this.storedWeapon= weapon;
        }

        public void SetNewHeldWeapon(Weapon weapon)
        {
            Weapon newWeapon = weapon;
            this.heldWeapon= newWeapon;
        }

        public void SetNewStoredWeapon(Weapon weapon)
        {
            Weapon newWeapon = weapon;
            this.storedWeapon= weapon;
        }

        public Weapon GetHeldWeapon()
        {
            return this.heldWeapon;
        }

        public Weapon GetStoredWeapon()
        {
            return this.storedWeapon;
        }
        

        public void Shoot(Cast cast, Vector2 mousePosition)
        {
            bool shot = heldWeapon.Shoot();
            if(shot)
            {
                // Construct bullet.
                Bullet bulletType = heldWeapon.GetBulletType();
                Bullet bullet = new Bullet(bulletType.GetBulletDamage());

                // Set bullet position to player position.
                bullet.SetPosition(this.GetPosition());

                // Get player postion to calculate firing direction.
                Vector2 pointPosition = this.GetPosition();

                // Subtract player position from mouse position.
                Vector2 a = Vector2.Subtract(mousePosition, pointPosition);

                // Normalize result so contains only direction, not magnitude.
                Vector2 normalized = Vector2.Normalize(a);
                Vector2 bulletDirection =heldWeapon.Spread(normalized);
                Vector2 bulletVector = Vector2.Multiply(bulletDirection, Program.BULLET_SPEED);

                // Finish bullet.
                bullet.SetVelocity(bulletVector);
                bullet.SetRadius(Program.BULLET_RADIUS);
                bullet.SetColor(this.GetColor());

                // Add bullet to actors so it is displayed.
                cast.AddActor("bullets", bullet);
            }
        }

        public void PlayerPickupAmmo()
        {
            this.heldWeapon.addAmmo();
        }
        
        public void PlayerReload()
        {
            this.heldWeapon.reload();
        }

        public static void PlayerTakeDamage(int damage)
        {
            playerHealth -= 20;
            Console.WriteLine($"Player took {damage} damage. Player Health: {playerHealth}");
        }
    }
}