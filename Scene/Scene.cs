using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AdelieEngine.Scene
{
    public class Scene
    {
        public string Name;
        public List<Canvas.Canvas> Canvases = new List<Canvas.Canvas>();
        public List<Entity.Entity> Entities = new List<Entity.Entity>();

        public Scene(string name)
        {
            this.Name = name;
        }

        public virtual void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            this.Canvases.Add(new Canvas.Canvas(game.GraphicsDevice, 320, 180, true));
        }

        public virtual void LoadContent(Game game, GraphicsDeviceManager graphics)
        {
            
        }

        public virtual void UnloadContent(Game game, GraphicsDeviceManager graphics)
        {

        }

        public virtual void Update(Game game, GraphicsDeviceManager graphics, float deltaTime)
        {
            
        }

        public virtual void Draw(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            /*game.GraphicsDevice.SetRenderTarget(this.Canvases[0].RenderTarget);
            game.GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            spriteBatch.Draw(Engine.WhiteBox, new Rectangle(100, 100, 100, 100), Color.Aqua);
            spriteBatch.End();*/
            game.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
