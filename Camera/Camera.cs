using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AdelieEngine.Camera
{
    public class Camera
    {
        public float X, Y, Width, Height;

        public Camera(Canvas.Canvas canvas)
        {
            this.X = 0;
            this.Y = 0;
            this.Width = canvas.Width;
            this.Height = canvas.Height;
        }
    }
}
