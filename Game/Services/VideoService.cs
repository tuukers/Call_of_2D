using System.Collections.Generic;
using Raylib_cs;
using Callof2d.Game.Casting;
using System;
using System.Numerics;


namespace Callof2d.Game.Services
{
    /// <summary>
    /// <para>Outputs the game state.</para>
    /// <para>
    /// The responsibility of the class of objects is to draw the game state on the screen. 
    /// </para>
    /// </summary>
    public class VideoService
    {
        private int cellSize = 15;
        private string caption = "";
        private int width = 640;
        private int height = 480;
        private int frameRate = 0;
        private bool debug = false;
        private Texture2D background;

        /// <summary>
        /// Constructs a new instance of KeyboardService using the given cell size.
        /// </summary>
        /// <param name="cellSize">The cell size (in pixels).</param>
        public VideoService(string caption, int width, int height, int cellSize, int frameRate, 
                bool debug)
        {
            this.caption = caption;
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.frameRate = frameRate;
            this.debug = debug;
        }

        /// <summary>
        /// Closes the window and releases all resources.
        /// </summary>
        public void CloseWindow()
        {
            Raylib.CloseWindow();
            Raylib.UnloadTexture(background);
        }

        /// <summary>
        /// Clears the buffer in preparation for the next rendering. This method should be called at
        /// the beginning of the game's output phase.
        /// </summary>
        public void ClearBuffer()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.BLACK);
            if (debug)
            {
                DrawGrid();
            }
        }

        /// <summary>
        /// Draws the given actor's text on the screen.
        /// </summary>
        /// <param name="actor">The actor to draw.</param>
        public void DrawActor(Actor actor)
        {
            Vector2 position = actor.GetPosition();
            Casting.Color c = actor.GetColor();
            float radius = actor.GetRadius();
            Raylib_cs.Color color = ToRaylibColor(c);
            Raylib.DrawCircleV(position, radius, color);
        }

        public void DrawWall(Actor wallIn)
        {
            Wall wall = (Wall) wallIn;
            Vector2 position = wall.GetPosition();
            Casting.Color c = wall.GetColor();
            float height = wall.GetHeight();
            float width = wall.GetWidth();
            Vector2 size = new Vector2(width,height);
            Raylib_cs.Color color = ToRaylibColor(c);
            Raylib.DrawRectangleV(position,size,color);
        }

        /// <summary>
        /// Draws the given list of actors on the screen.
        /// </summary>
        /// <param name="actors">The list of actors to draw.</param>
        public void DrawActors(List<Actor> actors)
        {
            // foreach (Actor actor in actors)
            // {
            //     DrawActor(actor);
            // }
            for (int i = 0; i < actors.Count; i++) {
                DrawActor(actors[i]);
            }
        }

        public void DrawWalls(List<Actor> actors)
        {
            for (int i = 0; i < 4; i++)
            {
                DrawWall(actors[i]);
            }
        }

        public void DrawHUD(Actor hUDT)
        {
            HUD hUD = (HUD) hUDT;
            Vector2 position = hUD.GetPosition();
            Casting.Color c = hUD.GetColor();
            Raylib_cs.Color color = ToRaylibColor(c);
            string HUDText = hUD.GetText();
            Raylib.DrawText(HUDText,(int)position.X,(int)position.Y,hUD.GetFontSize(),color);
        }

        public void DrawHUDs(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                DrawHUD(actor);
            }
        }

        public void DrawPointer(Actor player, Vector2 mousePosition)
        {
            Vector2 playerPosition = player.GetPosition();

            Vector2 a = Vector2.Subtract(mousePosition, playerPosition);
            Vector2 b = Vector2.Normalize(a);
            Vector2 c = Vector2.Multiply(b, 50);
            Vector2 endPosition = Vector2.Add(playerPosition, c);

            Raylib.DrawLineV(playerPosition, endPosition, ToRaylibColor(player.GetColor()));
        }

        
        public void DrawBackground ()
        {
            Raylib.DrawTexture(background, Program.MAX_X/12, Program.MAX_Y/12, ToRaylibColor(Program.WHITE));
        }


        /// <summary>
        /// Copies the buffer contents to the screen. This method should be called at the end of
        /// the game's output phase.
        /// </summary>
        public void FlushBuffer()
        {
            Raylib.EndDrawing();
        }

        /// <summary>
        /// Gets the grid's cell size.
        /// </summary>
        /// <returns>The cell size.</returns>
        public int GetCellSize()
        {
            return cellSize;
        }

        /// <summary>
        /// Gets the screen's height.
        /// </summary>
        /// <returns>The height (in pixels).</returns>
        public int GetHeight()
        {
            return height;
        }

        /// <summary>
        /// Gets the screen's width.
        /// </summary>
        /// <returns>The width (in pixels).</returns>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// Whether or not the window is still open.
        /// </summary>
        /// <returns>True if the window is open; false if otherwise.</returns>
        public bool IsWindowOpen()
        {
            return !Raylib.WindowShouldClose();
        }

        /// <summary>
        /// Opens a new window with the provided title.
        /// </summary>
        public void OpenWindow()
        {
            Raylib.InitWindow(width, height, caption);
            Raylib.SetTargetFPS(frameRate);

            Image image = Raylib.LoadImage("Game/assets/map.png");
            this.background = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image);
        }

        /// <summary>
        /// Draws a grid on the screen.
        /// </summary>
        private void DrawGrid()
        {
            for (int x = 0; x < width; x += cellSize)
            {
                Raylib.DrawLine(x, 0, x, height, Raylib_cs.Color.GRAY);
            }
            for (int y = 0; y < height; y += cellSize)
            {
                Raylib.DrawLine(0, y, width, y, Raylib_cs.Color.GRAY);
            }
        }

        /// <summary>
        /// Converts the given color to it's Raylib equivalent.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>A Raylib color.</returns>
        private Raylib_cs.Color ToRaylibColor(Casting.Color color)
        {
            int r = color.GetRed();
            int g = color.GetGreen();
            int b = color.GetBlue();
            int a = color.GetAlpha();
            return new Raylib_cs.Color(r, g, b, a);
        }

    }
}