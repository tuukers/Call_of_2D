using Raylib_cs;
using static Raylib_cs.MouseButton;
using Callof2d.Game.Casting;
using System;
using System.Numerics;

namespace Callof2d.Game.Services
{
    /// <summary>
    /// <para>Detects player input.</para>
    /// <para>
    /// The responsibility of a KeyboardService is to detect player key presses and translate them 
    /// into a point representing a direction.
    /// </para>
    /// </summary>
    public class MouseService
    {
        
        public MouseService()
        {
        }

        /// <summary>
        /// Gets the selected direction based on the currently pressed keys.
        /// </summary>
        /// <returns>The direction as an instance of Point.</returns>
        public Vector2 GetMousePosition()
        {

            Vector2 mousePosition = Raylib.GetMousePosition();
            
            return mousePosition;
            
        }

        public bool IsMousePressed()
        {
            if (Raylib.IsMouseButtonPressed(MOUSE_LEFT_BUTTON))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRightMousePressed()
        {
            if (Raylib.IsMouseButtonPressed(MOUSE_RIGHT_BUTTON))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsScrolled()
        {
            if (Raylib.GetMouseWheelMove()!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsMouseDown()
        {
            if (Raylib.IsMouseButtonDown(MOUSE_LEFT_BUTTON))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}