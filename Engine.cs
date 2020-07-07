using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AdelieEngine
{
    public static class Engine
    {

        //Title
        public static string Title = "Adelie";

        //Screen size
        public static int Width = 1920;
        public static int Height = 1080;
        public static int WindowWidth = 960;
        public static int WindowHeight = 540;
        public static Rectangle Rectangle = new Rectangle(0, 0, Engine.Width, Engine.Height);

        //Time
        public static float DeltaTime;
        public static float DeltaTimeRaw;
        public static float DeltaTimeRate = 1.0f;
        public static float FPS;

        //Graphic
        private static Rectangle RenderTargetRectangle;
        private static RenderTarget2D RenderTarget;
        public static Texture2D WhiteBox;
        public static bool Fullscreen = false;

        public static void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            game.Window.AllowUserResizing = true;
            game.Window.Title = Engine.Title;

            if (Engine.Fullscreen)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.PreferredBackBufferWidth = Engine.WindowWidth;
                graphics.PreferredBackBufferHeight = Engine.WindowHeight;
                graphics.IsFullScreen = false;
            }

            //Graphics setting
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = false;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphics.ApplyChanges();

            game.IsMouseVisible = true;
            game.IsFixedTimeStep = true;

            Engine.RenderTarget = new RenderTarget2D(game.GraphicsDevice, Engine.Width, Engine.Height, false, SurfaceFormat.Color, DepthFormat.None);

            Engine.WhiteBox = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            Engine.WhiteBox.SetData<Color>(data);

            Engine.Rectangle = new Rectangle(0, 0, Engine.Width, Engine.Height);

            //Initialize Scene
            Scene.SceneManager.Initialize(game, graphics);
        }

        public static void LoadContent(Game game, GraphicsDeviceManager graphics)
        {
            Scene.SceneManager.LoadContent(game, graphics);
        }

        public static void UnloadContent(Game game, GraphicsDeviceManager graphics)
        {
            Engine.WhiteBox.Dispose();
            Scene.SceneManager.UnloadContent(game, graphics);
        }

        public static void Update(Game game, GraphicsDeviceManager graphics, GameTime gameTime)
        {
            Engine.DeltaTimeRaw = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Engine.DeltaTime = Engine.DeltaTimeRaw * Engine.DeltaTimeRate;

            //Toggle fullscreen for testing       Will delete later
            if (Input.InputManager.F.JustDown())
            {
                if (graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = 960;
                    graphics.PreferredBackBufferHeight = 540;
                    game.IsMouseVisible = true;
                }
                else
                {
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    game.IsMouseVisible = false;
                }
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }

            //Update main rendertarget or viewport
            float Scale = 0f;
            if ((float)game.GraphicsDevice.Viewport.Width / (float)Engine.Width >= (float)game.GraphicsDevice.Viewport.Height / (float)Engine.Height)
            {
                Scale = (float)game.GraphicsDevice.Viewport.Height / (float)Engine.Height;
            }
            else
            {
                Scale = (float)game.GraphicsDevice.Viewport.Width / (float)Engine.Width;
            }
            Engine.RenderTargetRectangle.X = (int)Math.Floor((float)(game.GraphicsDevice.Viewport.Width - Engine.Width * Scale) / 2);
            Engine.RenderTargetRectangle.Y = (int)Math.Floor((float)(game.GraphicsDevice.Viewport.Height - Engine.Height * Scale) / 2);
            Engine.RenderTargetRectangle.Width = (int)(Engine.Width * Scale);
            Engine.RenderTargetRectangle.Height = (int)(Engine.Height * Scale);

            //Scene
            Scene.SceneManager.Update(game, graphics, Engine.DeltaTime);

        }

        public static void Draw(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.SetRenderTarget(null);
            game.GraphicsDevice.Clear(Color.Transparent);

            //Scene
            Scene.SceneManager.Draw(game, graphics, spriteBatch);

            //Draw scenes
            if (Scene.SceneManager.CurrentScene != null)
            {
                //Draw all the canvas/rendertarget of the current scene
                for (int i = 0; i < Scene.SceneManager.CurrentScene.Canvases.Count; i++)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Scene.SceneManager.CurrentScene.Canvases[i].SamplerState, null, null, null);
                    spriteBatch.Draw(Scene.SceneManager.CurrentScene.Canvases[i].RenderTarget, Engine.RenderTargetRectangle, Color.White);
                    spriteBatch.End();
                }
            }

            //Draw transitions
            if (Scene.SceneManager.Switching)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null);
                spriteBatch.Draw(Scene.SceneManager.TransitionCanvas.RenderTarget, Engine.RenderTargetRectangle, Color.White);
                spriteBatch.End();
            }
        }
    }
}
