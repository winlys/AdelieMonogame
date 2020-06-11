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
    public class AnimationSprite : Sprite
    {
        private int framewidth;
        public int FrameStart, FrameEnd, FrameCurrent; //Start from 0
        public int FrameCount;

        private float TimeCounter = 0;
        public float TimePerFrame = 0.01f;

        public bool Playing = true;

        public List<Rectangle> Quads = new List<Rectangle>();


        public AnimationSprite(Texture2D texture, int frameWidth)
        {
            this.framewidth = frameWidth;
            this.Texture = texture;

            //Frame
            this.FrameCount = (int)Math.Floor((float)this.Texture.Width / (float)frameWidth);
            this.FrameStart = this.FrameCurrent = 0;
            this.FrameEnd = this.FrameCount - 1;

            //Quads
            for (int i = 0; i < this.FrameCount; i++)
            {
                this.Quads.Add(new Rectangle(i * frameWidth, 0, frameWidth, this.Texture.Height));
            }
        }

        public int FrameWidth { get { return this.framewidth; } }

        public override void Update(float dt)
        {
            if (this.Playing)
            {
                this.TimeCounter += dt;
                if (this.TimeCounter >= this.TimePerFrame)
                {
                    this.TimeCounter = 0;
                    this.FrameCurrent++;
                    if (this.FrameCurrent > this.FrameEnd)
                    {
                        this.FrameCurrent = this.FrameStart;
                    }
                }
            }
            else
            {
                this.TimeCounter = 0;
            }

            //Update rectangle
            base.Update(dt);
            this.Rectangle.Width = (int)(this.FrameWidth * this.Scale.X);
            this.Rectangle.Height = (int)(this.Texture.Height * this.Scale.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, this.Quads[this.FrameCurrent], this.Color);
            base.Draw(spriteBatch);
        }
    }
}
