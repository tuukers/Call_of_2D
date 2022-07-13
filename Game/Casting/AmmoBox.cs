namespace Callof2d.Game.Casting
{
    public class AmmoBox : Actor
    {   
        public AmmoBox()
        {
        }

        // This didn't work, couldn't access cast. Instead, this code will happen in the HandleBulletCollisionsAction where the zombie dies.
        //public static void SpawnAmmoBox(System.Numerics.Vector2 position)
        //{
//
        //    AmmoBox ammoBox = new AmmoBox();
        //            ammoBox.SetColor(color);
        //            ammoBox.SetPosition(position);
        //            ammoBox.SetRadius(Program.ZOMBIE_RADIUS / 2);
        //            cast.AddActor("ammoBox", ammoBox);
        //}

    }
}