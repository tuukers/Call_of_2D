using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    public class Wall : Actor
    {
        private float height;
        private float width; 
        private bool horizontal;

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
        
        public void SetHorizontal(bool horizontal)
        {
            this.horizontal=horizontal;
        }

        public float GetHeight()
        {
            return this.height;
        }

        public float GetWidth()
        {
            return this.width;
        }

        public float GetTop(Vector2 position)
        {
            float top = position.Y;
            return top;
        }

        public float GetLeft(Vector2 position)
        {
            float left = position.X;
            return left;
        }

        public float GetRight(Vector2 position)
        {
            float right = position.X + this.width;
            return right;
        }

        public float GetBottom(Vector2 position)
        {
            float bottom = position.Y +this.height;
            return bottom;
        }

        public Vector2 GetCenter(Vector2 position)
        {
            Vector2 center = new Vector2(position.X+width/2,position.Y+height/2);
            return center;
        }

        public bool GetHorizontal()
        {
            return horizontal;
        }
    }
}