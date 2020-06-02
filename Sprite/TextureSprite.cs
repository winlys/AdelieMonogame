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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, this.Color);
            base.Draw(spriteBatch);
        }
    }
}
