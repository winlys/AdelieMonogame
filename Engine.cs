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
    public class Engine
    {

        //Title
        public string Title;

        //Screen size
        public int Width;
        public int Height;
        public int WindowWidth;
        public int WindowHeight;
        public Rectangle Rectangle;

        //Time
        public float DeltaTime;
        public float DeltaTimeRaw;
        public float DeltaTimeRate = 1.0f;
        public float FPS;

        //Graphic
        private Rectangle RenderTargetRectangle;
        private RenderTarget2D RenderTarget;
        public static Texture2D WhiteBox;

        //Input
        public Input.Manager InputManager;

        //Scene
        public Scene.Manager SceneManager;

        public Engine(Game game, GraphicsDeviceManager graphics, int width, int height, int windowwidth, int windowheight, string title, bool fullscreen)
        {
            //Size
            this.Width = width;
            this.Height = height;
            this.WindowWidth = windowwidth;
            this.WindowHeight = windowheight;
            this.Rectangle = new Rectangle(0, 0, width, height);

            game.Window.AllowUserResizing = true;

            if (fullscreen)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.IsFullScreen = true;
            }
            else
            {
                graphics.PreferredBackBufferWidth = this.WindowWidth;
                graphics.PreferredBackBufferHeight = this.WindowHeight;
                graphics.IsFullScreen = false;
            }

            //Title
            this.Title = game.Window.Title = title;

            //Input
            this.InputManager = new Input.Manager();

            //Scene
            this.SceneManager = new Scene.Manager();

            //Graphics setting
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = false;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphics.ApplyChanges();

            game.IsMouseVisible = true;
            game.IsFixedTimeStep = true;

            this.RenderTarget = new RenderTarget2D(game.GraphicsDevice, this.Width, this.Height, false, SurfaceFormat.Color, DepthFormat.None);

            Engine.WhiteBox = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            Engine.WhiteBox.SetData<Color>(data);
        }

        public virtual void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < SceneManager.Scenes.Count; i++)
            {
                SceneManager.Scenes[i].Initialize(game, graphics);
            }
        }

        public virtual void LoadContent(Game game, GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < SceneManager.Scenes.Count; i++)
            {
                SceneManager.Scenes[i].LoadContent(game, graphics);
            }
        }

        public virtual void UnloadContent(Game game, GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < SceneManager.Scenes.Count; i++)
            {
                SceneManager.Scenes[i].UnloadContent(game, graphics);
            }
        }

        public virtual void Update(Game game, GraphicsDeviceManager graphics, GameTime gameTime)
        {
            this.DeltaTimeRaw = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.DeltaTime = this.DeltaTimeRaw * this.DeltaTimeRate;

            //Toggle fullscreen for testing       Will delete later
            if (this.InputManager.F.JustDown())
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
            if ((float)game.GraphicsDevice.Viewport.Width / (float)this.Width >= (float)game.GraphicsDevice.Viewport.Height / (float)this.Height)
            {
                Scale = (float)game.GraphicsDevice.Viewport.Height / (float)this.Height;
            }
            else
            {
                Scale = (float)game.GraphicsDevice.Viewport.Width / (float)this.Width;
            }
            this.RenderTargetRectangle.X = (int)Math.Floor((float)(game.GraphicsDevice.Viewport.Width - this.Width * Scale) / 2);
            this.RenderTargetRectangle.Y = (int)Math.Floor((float)(game.GraphicsDevice.Viewport.Height - this.Height * Scale) / 2);
            this.RenderTargetRectangle.Width = (int)(this.Width * Scale);
            this.RenderTargetRectangle.Height = (int)(this.Height * Scale);

            //Scene
            if (this.SceneManager.CurrentScene != null)
            {
                this.SceneManager.CurrentScene.Update(game, graphics, gameTime);
            }

        }

        public virtual void Draw(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.SetRenderTarget(null);
            game.GraphicsDevice.Clear(Color.Transparent);

            //Scene
            if (this.SceneManager.CurrentScene != null)
            {
                this.SceneManager.CurrentScene.Draw(game, graphics, spriteBatch);
            }

            game.GraphicsDevice.SetRenderTarget(this.RenderTarget);
            game.GraphicsDevice.Clear(Color.Transparent);
            //Draw scene canvases
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            //spriteBatch.Draw(this.WhiteBox, new Rectangle(100, 100, 100, 100), Color.White);
            if (this.SceneManager.CurrentScene != null)
            {
                for (int i = 0; i < this.SceneManager.CurrentScene.Canvases.Count; i++)
                {
                    spriteBatch.Draw(this.SceneManager.CurrentScene.Canvases[i].RenderTarget, this.Rectangle, Color.White);
                }
            }
            spriteBatch.End();
            game.GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            spriteBatch.Draw(this.RenderTarget, this.RenderTargetRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
