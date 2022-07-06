using Raylib_cs;
using Callof2d.Game.Casting;
using System;
using System.Numerics;
using System.Data;
using System.Collections.Generic;

namespace Callof2d.Game.Services
{

    public class ContactService
    {
        public bool collision=false;
        public ContactService()
        {
            
        }
        public float Rotation(Vector2 referencePosition, Vector2 otherPosition){
                
                float x_difference = otherPosition.X - referencePosition.X;
                float y_difference = otherPosition.Y - referencePosition.Y;
                float a=0;
                if (x_difference>=0)
                    
                    a=(float)Math.Atan(y_difference/x_difference);
                return a;
            }
        public bool ListListCollision(Actor actor1, Actor actor2)
        {

            Vector2 actor1Position=actor1.GetPosition();
            float actor1Position_X=actor1Position.X;
            float actor1Position_Y=actor1Position.Y;

            Vector2 actor2Position=actor2.GetPosition();
            Bullet shot = (Bullet)actor2;
            float actor2Position_X=actor2Position.X;
            float actor2Position_Y=actor2Position.Y;

            float x_difference= actor1Position_X-actor2Position_X;
            float y_difference= actor1Position_Y-actor2Position_Y;
            float x_difference_abs=Math.Abs(x_difference);
            float y_difference_abs=Math.Abs(y_difference);

            if(x_difference_abs<10 && y_difference_abs<10)
            {
                collision=true;
            }
            else
            {
                collision=false;
            }
            return collision;
        }
    }
}