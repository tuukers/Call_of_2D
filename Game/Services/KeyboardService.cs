using Raylib_cs;
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
    public class KeyboardService
    {
        private int playerSpeed = 0;


        /// <summary>
        /// Constructs a new instance of KeyboardService using the given cell size.
        /// </summary>
        /// <param name="playerSpeed">The cell size (in pixels).</param>
        public KeyboardService()
        {

        }

        /// <summary>
        /// Gets the selected direction based on the currently pressed keys.
        /// </summary>
        /// <returns>The direction as an instance of Point.</returns>
        public Vector2 GetDirection(bool topCollision, bool leftCollision, bool rightCollision, bool bottomCollision)
        {

            int dx = 0;
            int dy = 0;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) & !leftCollision)
            {
                dx = -1;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_D) & !rightCollision)
            {
                dx = 1;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) & !topCollision)
            {
                dy = -1;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) & !bottomCollision)
            {
                dy = 1;
            }
            
            Vector2 direction = new Vector2(dx,dy);
            direction = Vector2.Multiply(direction, 7);

            return direction;

            
        }

        public bool RKeyPressed()
        {
            bool pressed = false;
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
            {
                pressed = true;
            }
            return pressed;
        }

        public bool VKeyPressed()
        {
            bool pressed = false;
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_V))
            {
                pressed = true;
            }
            return pressed;
        }

        public bool EKeyPressed()
        {
            bool pressed = false;
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_E))
            {
                pressed = true;
            }
            return pressed;
        }

        // public Vector2 GetDirectionTopCollision()
        // {

        //     int dx = 0;
        //     int dy = 0;

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        //     {
        //         dx = -1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        //     {
        //         dx = 1;
        //     }

        //     // if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        //     // {
        //     //     dy = -1;
        //     // }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        //     {
        //         dy = 1;
        //     }
            
        //     Vector2 direction = new Vector2(dx,dy);
        //     direction = Vector2.Multiply(direction, 7);

        //     return direction;
        // }

        // public Vector2 GetDirectionBottomCollision()
        // {

        //     int dx = 0;
        //     int dy = 0;

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        //     {
        //         dx = -1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        //     {
        //         dx = 1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        //     {
        //         dy = -1;
        //     }

        //     // if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        //     // {
        //     //     dy = 1;
        //     // }
            
        //     Vector2 direction = new Vector2(dx,dy);
        //     direction = Vector2.Multiply(direction, 7);

        //     return direction;

            
        // }

        // public Vector2 GetDirectionLeftCollision()
        // {

        //     int dx = 0;
        //     int dy = 0;

        //     // if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        //     // {
        //     //     dx = -1;
        //     // }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        //     {
        //         dx = 1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        //     {
        //         dy = -1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        //     {
        //         dy = 1;
        //     }
            
        //     Vector2 direction = new Vector2(dx,dy);
        //     direction = Vector2.Multiply(direction, 7);

        //     return direction;

            
        // }

        // public Vector2 GetDirectionRightCollision()
        // {

        //     int dx = 0;
        //     int dy = 0;

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        //     {
        //         dx = -1;
        //     }

        //     // if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        //     // {
        //     //     dx = 1;
        //     // }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        //     {
        //         dy = -1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        //     {
        //         dy = 1;
        //     }
            
        //     Vector2 direction = new Vector2(dx,dy);
        //     direction = Vector2.Multiply(direction, 7);

        //     return direction;

            
        // }

        // public Vector2 GetDirectionTopRightCollision()
        // {

        //     int dx = 0;
        //     int dy = 0;

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        //     {
        //         dx = -1;
        //     }

        //     // if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        //     // {
        //     //     dx = 1;
        //     // }

        //     // if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        //     // {
        //     //     dy = -1;
        //     // }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        //     {
        //         dy = 1;
        //     }
            
        //     Vector2 direction = new Vector2(dx,dy);
        //     direction = Vector2.Multiply(direction, 7);

        //     return direction;

            
        // }

        // public Vector2 GetDirectionTopLeftCollision()
        // {

        //     int dx = 0;
        //     int dy = 0;

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        //     {
        //         dx = -1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        //     {
        //         dx = 1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        //     {
        //         dy = -1;
        //     }

        //     if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        //     {
        //         dy = 1;
        //     }
            
        //     Vector2 direction = new Vector2(dx,dy);
        //     direction = Vector2.Multiply(direction, 7);

        //     return direction;

            
        // }
    }
}