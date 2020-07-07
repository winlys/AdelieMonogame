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
    public class TextSprite : Sprite
    {
        public SpriteFont Font;
        public string Text;

        public TextSprite(SpriteFont font, string text)
        {
            this.Font = font;
            this.Text = text;
            this.Color = Color.Black;
        }

        public override void Update(float dt)
        {

            //Update Rectangle
            //No need to update scale
            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, this.Text, this.Position + this.Offset, this.Color);
            base.Draw(spriteBatch);
        }
    }
}
