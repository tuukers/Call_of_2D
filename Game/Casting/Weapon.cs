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
        private bool fullAuto;
        private int fireRate;
        private Bullet bulletType;
        private string weaponName;

        public Weapon(int maxAmmo,int ammoCount,int magazineCapacity,int magazineCount,bool fullAuto,int fireRate,Bullet bulletType,string weaponName)
        {
            this.maxAmmo=maxAmmo;
            this.ammoCount=ammoCount;
            this.magazineCapacity=magazineCapacity;
            this.magazineCount=magazineCount;
            this.fullAuto=fullAuto;
            this.fireRate=fireRate;
            this.bulletType=bulletType;
            this.weaponName=weaponName;
        }

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

        public Bullet GetBulletType()
        {
            return this.bulletType;
        }

        public string GetWeaponName()
        {
            return this.weaponName;
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

        public void reload()
        {
            this.magazineCount = this.magazineCapacity;
            this.ammoCount -= this.magazineCapacity-this.magazineCount;
        }
    }
}
