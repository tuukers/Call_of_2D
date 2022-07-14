using System;
using System.Numerics;
using Callof2d.Game;


namespace Callof2d.Game.Casting
{
    public class Player : Actor
    {
        private int playerMaxHealth=100;
        private int playerHealth=100;
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
            newWeapon.SetAmmoCount(newWeapon.GetMaxAmmo());
            newWeapon.SetMagazineCount(newWeapon.GetMagazineCapacity());
            this.heldWeapon= newWeapon;
        }

        public void SetNewStoredWeapon(Weapon weapon)
        {
            Weapon newWeapon = weapon;
            newWeapon.SetAmmoCount(newWeapon.GetMaxAmmo());
            this.storedWeapon= newWeapon;
        }

        public void SetPlayerMaxHealth(int playerMaxHealth)
        {
            this.playerMaxHealth = playerMaxHealth;
        }

        public Weapon GetHeldWeapon()
        {
            return this.heldWeapon;
        }

        public Weapon GetStoredWeapon()
        {
            return this.storedWeapon;
        }

        public int GetPlayerHealth()
        {
            return this.playerHealth;
        }

        public int GetPlayerMaxHealth()
        {
            return this.playerMaxHealth;
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

        public void PlayerShotgunReload()
        {
            this.heldWeapon.ShotgunReload();
        }

        public void PlayerTakeDamage()
        {
            this.playerHealth -= 30;
            Console.WriteLine($"Player took 30 damage. Player Health: {this.playerHealth}");
        }

        public void PlayerRegen()
        {
            if (this.playerHealth < this.playerMaxHealth)
            {
                this.playerHealth +=1;
            }
        }
    }
}