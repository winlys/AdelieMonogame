using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AdelieEngine.Canvas
{
    public class Canvas
    {
        public int Width, Height;
        public RenderTarget2D RenderTarget;
        public bool Pixel;
        public SamplerState SamplerState;
        public Camera.Camera Camera;

        public Canvas(GraphicsDevice graphics, int width, int height, bool pixel)
        {
            this.Width = width;
            this.Height = height;
            this.Pixel = pixel;
            if (this.Pixel)
            {
                this.SamplerState = SamplerState.PointClamp;
            }
            else
            {
                this.SamplerState = SamplerState.LinearClamp;
            }
            this.RenderTarget = new RenderTarget2D(graphics, this.Width, this.Height, !pixel, SurfaceFormat.Color, DepthFormat.None);
            this.Camera = new Camera.Camera(this);
        }

        public void Begin(Game game)
        {
            game.GraphicsDevice.SetRenderTarget(this.RenderTarget);
        }

        public void End(Game game)
        {
            game.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
