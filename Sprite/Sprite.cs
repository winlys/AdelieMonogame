using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AdelieEngine.Sprite
{
    public class Sprite
    {
        public Vector2 Position;
        public Vector2 Offset = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public Rectangle Rectangle = new Rectangle(0, 0, 0, 0);

        public Texture2D Texture;

        public Color Color = Color.White;

        public Sprite()
        {

        }

        public virtual void Update(float dt)
        {
            this.Rectangle.X = (int)this.Position.X + (int)this.Offset.X;
            this.Rectangle.Y = (int)this.Position.Y + (int)this.Offset.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Canvas.Canvas canvas)
        {
            
        }
    }
}
