using System.Collections.Generic;
using System;
using System.Numerics;
using Raylib_cs;


namespace Callof2d.Game.Directing
{
    public class Clock
    {

        public float lifetime;

        public Clock()
        {
        }

        public double GetLifeTime() 
        {
            return Raylib.GetTime();
        }
    }
}