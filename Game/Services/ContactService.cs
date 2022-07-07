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
        public bool Collision(Actor actor1, Actor actor2)
        {
            if(!(actor1==null || actor2==null))
            {
                return false;
            }
            else
            {
                float radius1 = actor1.GetRadius();
                float radius2 = actor2.GetRadius();

                Vector2 actor1Position = actor1.GetPosition();
                Vector2 actor2Position = actor2.GetPosition();

                Vector2 vector = actor1Position - actor2Position;
                float distance = Vector2.Distance(actor1Position,actor2Position);

                if(distance <= radius1 + radius2)
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
}