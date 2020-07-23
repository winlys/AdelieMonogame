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
    public class TextureSprite : Sprite
    {
        public TextureSprite(Texture2D texture)
        {
            this.Texture = texture;
        }

        public override void Update(float dt)
        {
            this.Rectangle.Width = (int)(this.Texture.Width * this.Scale.X);
            this.Rectangle.Height = (int)(this.Texture.Height * this.Scale.Y);
            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch, Canvas.Canvas canvas)
        {
            this.Rectangle.X = (int)Math.Floor((this.Position.X + this.Offset.X - canvas.Camera.X) * canvas.Width / canvas.Camera.Width);
            this.Rectangle.Y = (int)Math.Floor((this.Position.Y + this.Offset.Y - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height);
            //this.Rectangle.Width = (int)Math.Floor(this.Texture.Width * this.Scale.X * canvas.Width / canvas.Camera.Width);
            //this.Rectangle.Height = (int)Math.Floor(this.Texture.Height * this.Scale.Y * canvas.Height / canvas.Camera.Height);
            this.Rectangle.Width = (int)Math.Floor((this.Position.X + this.Offset.X + this.Texture.Width * this.Scale.X - canvas.Camera.X) * canvas.Width / canvas.Camera.Width) - this.Rectangle.X;
            this.Rectangle.Height = (int)Math.Floor((this.Position.Y + this.Offset.Y + this.Texture.Height * this.Scale.Y - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height) - this.Rectangle.Y;

            spriteBatch.Draw(this.Texture, this.Rectangle, this.Color);
            base.Draw(spriteBatch, canvas);
        }
    }
}
