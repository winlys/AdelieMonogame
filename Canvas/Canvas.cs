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

        public Canvas(GraphicsDevice graphics, int width, int height, bool pixel)
        {
            this.Width = width;
            this.Height = height;
            this.RenderTarget = new RenderTarget2D(graphics, this.Width, this.Height, !pixel, SurfaceFormat.Color, DepthFormat.None);
        }
    }
}
