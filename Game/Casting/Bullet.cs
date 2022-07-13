using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    public class Bullet : Actor
    {

        
        private int bulletDamage=1;
        private int buckShot;
        public Bullet(int bulletDamage) 
        { 
            this.bulletDamage=bulletDamage;
        }

        public void SetBulletDamage(int bulletDamage)
        {
            this.bulletDamage= bulletDamage;
        }

        public void SetBuckShot(int buckShot)
        {
            this.buckShot = buckShot;
        }
        public int GetBulletDamage(){
            return bulletDamage;
        }

        public int GetBuckShot()
        {
            return buckShot;
        }
    }
}