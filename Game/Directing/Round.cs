using System.Collections.Generic;
using System;
using System.Numerics;
using Raylib_cs;


namespace Callof2d.Game.Directing
{
    public class Round
    {

        private int round = 0;

        public Round()
        {
        }

        public void IncrementRound() 
        {
            this.round += 1;
        }

        public int GetRound()
        {
            return this.round;
        }
    }
}