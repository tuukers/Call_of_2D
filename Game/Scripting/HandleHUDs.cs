using System;
using System.Collections.Generic;
using System.Numerics;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;


namespace Callof2d.Game.Scripting
{

    public class HandleHUDs : Action
    {
        public HandleHUDs()
        {

        }

        public void Execute(Cast cast, Script script)
        {
            List<Actor> hUDs =  cast.GetActors("HUD");
            foreach(Actor hUDT in hUDs)
            {
                HUD hUD = (HUD) hUDT;
                hUD.HUDSetup();
            }
        }
    }
}