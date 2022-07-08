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
        private float height;
        private float width;
        private Wall wall1;
        private Wall wall2;
        private Wall wall3;
        private Wall wall4;

        public ContactService(Wall wall1, Wall wall2, Wall wall3, Wall wall4)
        {
            this.wall1 = wall1;
            this.wall2 = wall2;
            this.wall3 = wall3;
            this.wall4 = wall4;
        }
        public float Rotation(Vector2 referencePosition, Vector2 otherPosition){
                
                float x_difference = otherPosition.X - referencePosition.X;
                float y_difference = otherPosition.Y - referencePosition.Y;
                float a=0;
                if (x_difference>=0)
                    
                    a=(float)Math.Atan(y_difference/x_difference);
                return a;
            }
        // public bool Collision(Actor actor1, Actor actor2)
        // {
        //     if(!(actor1==null || actor2==null))
        //     {
        //         return false;
        //     }
        //     else
        //     {
        //         float radius1 = actor1.GetRadius();
        //         float radius2 = actor2.GetRadius();

        //         Vector2 actor1Position = actor1.GetPosition();
        //         Vector2 actor2Position = actor2.GetPosition();

        //         Vector2 vector = actor1Position - actor2Position;
        //         float distance = Vector2.Distance(actor1Position,actor2Position);

        //         if(distance <= radius1 + radius2)
        //         {
        //             collision=true;
        //         }
        //         else
        //         {
        //             collision=false;
        //         }
        //         return collision;
        //     }
        // }
        public bool WallCollision(Actor actor, Wall wall)
        {
            float radius = actor.GetRadius();
            float height = wall.GetHeight();
            float width = wall.GetWidth();

            Vector2 actorPosition = actor.GetPosition();
            Vector2 wallPosition = wall.GetPosition();

            float wallLeft = wall.GetLeft(wallPosition);
            float wallRight = wall.GetRight(wallPosition);
            float wallTop = wall.GetTop(wallPosition);
            float wallBottom = wall.GetBottom(wallPosition);

            if((actorPosition.X+radius >= wallLeft & actorPosition.X+radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            {
                collision=true;
            }
            else if((actorPosition.X-radius >= wallLeft & actorPosition.X-radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            {
                collision=true;
            }
            else if((actorPosition.Y+radius <= wallBottom & actorPosition.Y+radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            {
                collision = true;
                
            }
            else if((actorPosition.Y-radius <= wallBottom & actorPosition.Y-radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            {
                collision = true;
                
            }
            else
            {
                collision=false;
            }
            return collision;
        }

        public bool WallCollisionBottom(Actor actor)
        {
            Wall wall = wall3;
            float radius = actor.GetRadius();
            float height = wall.GetHeight();
            float width = wall.GetWidth();

            Vector2 actorPosition = actor.GetPosition();
            Vector2 wallPosition = wall.GetPosition();

            float wallLeft = wall.GetLeft(wallPosition);
            float wallRight = wall.GetRight(wallPosition);
            float wallTop = wall.GetTop(wallPosition);
            float wallBottom = wall.GetBottom(wallPosition);

            // if((actorPosition.X+radius >= wallLeft & actorPosition.X+radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            // {
            //     collision=true;
            // }
            // else if((actorPosition.X-radius >= wallLeft & actorPosition.X-radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            // {
            //     collision=true;
            // }
            if((actorPosition.Y+radius <= wallBottom & actorPosition.Y+radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            {
                collision = true;
                
            }
            // else if((actorPosition.Y-radius <= wallBottom & actorPosition.Y-radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            // {
            //     collision = true;
                
            // }
            else
            {
                collision=false;
            }
            return collision;
        }

        public bool WallCollisionTop(Actor actor)
        {
            // Wall wall = wall2;
            float radius = actor.GetRadius();
            height = wall2.GetHeight();
            float width = wall2.GetWidth();

            Vector2 actorPosition = actor.GetPosition();
            Vector2 wallPosition = wall2.GetPosition();

            float wallLeft = wall2.GetLeft(wallPosition);
            float wallRight = wall2.GetRight(wallPosition);
            float wallTop = wall2.GetTop(wallPosition);
            float wallBottom = wall2.GetBottom(wallPosition);

            // if((actorPosition.X+radius >= wallLeft & actorPosition.X+radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            // {
            //     collision=true;
            // }
            // else if((actorPosition.X-radius >= wallLeft & actorPosition.X-radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            // {
            //     collision=true;
            // }
            // else if((actorPosition.Y+radius <= wallBottom & actorPosition.Y+radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            // {
            //     collision = true;
                
            // }
            if((actorPosition.Y-radius <= wallBottom & actorPosition.Y-radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            {
                collision = true;
                
            }
            else
            {
                collision=false;
            }
            return collision;
        }

        public bool WallCollisionRight(Actor actor)
        {
            Wall wall = wall4;
            float radius = actor.GetRadius();
            float height = wall.GetHeight();
            float width = wall.GetWidth();

            Vector2 actorPosition = actor.GetPosition();
            Vector2 wallPosition = wall.GetPosition();

            float wallLeft = wall.GetLeft(wallPosition);
            float wallRight = wall.GetRight(wallPosition);
            float wallTop = wall.GetTop(wallPosition);
            float wallBottom = wall.GetBottom(wallPosition);

            if((actorPosition.X+radius >= wallLeft & actorPosition.X+radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            {
                collision=true;
            }
            // else if((actorPosition.X-radius >= wallLeft & actorPosition.X-radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            // {
            //     collision=true;
            // }
            // else if((actorPosition.Y+radius <= wallBottom & actorPosition.Y+radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            // {
            //     collision = true;
                
            // }
            // else if((actorPosition.Y-radius <= wallBottom & actorPosition.Y-radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            // {
            //     collision = true;
                
            // }
            else
            {
                collision=false;
            }
            return collision;
        }

        public bool WallCollisionLeft(Actor actor)
        {
            Wall wall = wall1;
            float radius = actor.GetRadius();
            float height = wall.GetHeight();
            float width = wall.GetWidth();

            Vector2 actorPosition = actor.GetPosition();
            Vector2 wallPosition = wall.GetPosition();

            float wallLeft = wall.GetLeft(wallPosition);
            float wallRight = wall.GetRight(wallPosition);
            float wallTop = wall.GetTop(wallPosition);
            float wallBottom = wall.GetBottom(wallPosition);

            // if((actorPosition.X+radius >= wallLeft & actorPosition.X+radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            // {
            //     collision=true;
            // }
            if((actorPosition.X-radius >= wallLeft & actorPosition.X-radius <=wallRight) & actorPosition.Y+radius <=wallBottom & actorPosition.Y-radius >= wallTop)
            {
                collision=true;
            }
            // else if((actorPosition.Y+radius <= wallBottom & actorPosition.Y+radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            // {
            //     collision = true;
                
            // }
            // else if((actorPosition.Y-radius <= wallBottom & actorPosition.Y-radius >= wallTop)& actorPosition.X+radius <= wallRight & actorPosition.X-radius >= wallLeft)
            // {
            //     collision = true;
                
            // }
            else
            {
                collision=false;
            }
            return collision;
        }
    }
}