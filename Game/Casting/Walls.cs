using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    public class Wall : Actor
    {
        private float height;
        private float width; 

        public Wall()
        {

        }

        public void SetHeight(float height)
        {
            this.height=height;
        }

        public void SetWidth(float width)
        {
            this.width=width;
        }

        public float GetHeight()
        {
            return this.height;
        }

        public float GetWidth()
        {
            return this.width;
        }
    }
}