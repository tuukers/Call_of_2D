using System;
using System.Numerics;

namespace Callof2d.Game.Casting
{
    public class Weapon: Actor
    {
        private int maxAmmo;
        private int ammoCount;
        private int magazineCapacity;
        private int magazineCount;
        private bool isShotgun;
        private bool fullAuto;
        private int fireRate;
        private int weaponSpread;
        private Bullet bulletType;
        private string weaponName;
        private int reloadTime;
        private int reloading;



        public Weapon(int maxAmmo,int ammoCount,int magazineCapacity,int magazineCount,bool isShotgun,bool fullAuto,int fireRate,int weaponSpread,Bullet bulletType,string weaponName,int reloadTime)
        {
            this.maxAmmo=maxAmmo;
            this.ammoCount=ammoCount;
            this.magazineCapacity=magazineCapacity;
            this.magazineCount=magazineCount;
            this.isShotgun=isShotgun;
            this.fullAuto=fullAuto;
            this.fireRate=fireRate;
            this.weaponSpread = weaponSpread;
            this.bulletType=bulletType;
            this.weaponName=weaponName;
            this.reloadTime=reloadTime;
        }
        Random random = new Random();
        DateTime dateTime = new DateTime();

        //Getters and Setters
        public void SetMaxAmmo(int maxAmmo)
        {
            this.maxAmmo=maxAmmo;
        }

        public void SetAmmoCount(int ammoCount)
        {
            this.ammoCount=ammoCount;
        }

        public void SetMagazineCapacity(int magazineCapacity)
        {
            this.magazineCapacity=magazineCapacity;
        }

        public void SetMagazineCount(int magazineCount)
        {
            this.magazineCount=magazineCount;
        }

        public void SetFireRate(int fireRate)
        {
            this.fireRate=fireRate;
        }

        public void SetWeaponSpread(int weaponSpread)
        {
            this.weaponSpread = weaponSpread;
        }

        public void SetFullAuto(bool fullAuto)
        {
            this.fullAuto=fullAuto;
        }

        public void SetBulletType(Bullet bulletType)
        {
            this.bulletType=bulletType;
        }

        public int GetMaxAmmo()
        {
            return this.maxAmmo;
        }

        public int GetAmmoCount()
        {
            return this.ammoCount;
        }

        public int GetMagazineCapacity()
        {
            return this.magazineCapacity;
        }

        public int GetMagazineCount()
        {
            return this.magazineCount;
        }

        public int GetFireRate()
        {
            return this.fireRate;
        }

        public bool GetFullAuto()
        {
            return this.fullAuto;
        }

        public int GetWeaponSpread()
        {
            return this.weaponSpread;
        }

        public Bullet GetBulletType()
        {
            return this.bulletType;
        }

        public string GetWeaponName()
        {
            return this.weaponName;
        }

        public int GetReloadTime()
        {
            return this.reloadTime;
        }

        //weapon action
        public bool Shoot()
        {
            
            if (this.magazineCount>0)
            {
                this.magazineCount-=1;
                return true;                
            }
            else
            {
                return false;
            }
        }

        public Vector2 Spread(Vector2 aimVector)
        {
            Vector2 longVector = aimVector * 60;
            int xShift = random.Next(0,weaponSpread+1);
            int yShift = random.Next(0,weaponSpread+1);
            int xPosition = (int)longVector.X;
            int yPosition = (int)longVector.Y;
            int x = xPosition-weaponSpread/2 +xShift;
            int y = yPosition-weaponSpread/2 +yShift;
            Vector2 bulletLongVector = new Vector2(x,y);
            Vector2 bulletVector = Vector2.Normalize(bulletLongVector);
            return bulletVector;
        }

        public void reload()
        { 
            if(this.ammoCount>=this.magazineCapacity)
            {
                this.reloading = this.magazineCapacity - this.magazineCount;
                this.ammoCount -= this.reloading;
                this.magazineCount = this.magazineCapacity;
            }
            else if(this.ammoCount>0 & this.ammoCount<this.magazineCapacity)
            {
                if(ammoCount + magazineCount <magazineCapacity)
                {
                    this.magazineCount = this.ammoCount+this.magazineCount;
                    this.ammoCount = 0;
                }
                else
                {
                    this.reloading = this.magazineCapacity - this.magazineCount;
                    this.ammoCount -= this.reloading;
                    this.magazineCount = this.magazineCapacity; 
                }
            }
            else
            {
                
            }
        }
    }
}
