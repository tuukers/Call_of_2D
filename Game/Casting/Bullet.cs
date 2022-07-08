using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    public class Bullet : Actor
    {

        int bulletDamage=1;
        public Bullet(int bulletDamage) 
        { 
            this.bulletDamage=bulletDamage;
        }

        public void SetBulletDamage(int bulletDamage)
        {
            this.bulletDamage= bulletDamage;
        }
        public int GetBulletDamage(){
            return bulletDamage;
        }
    }
}