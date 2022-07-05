using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    public class Bullet : Actor
    {

        int bulletDamage=1;
        public Bullet() 
        { 
        }

        public int GetBulletDamage(){
            return bulletDamage;
        }
    }
}