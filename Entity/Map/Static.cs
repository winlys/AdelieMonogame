using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdelieEngine.Entity.Map
{
    public class Static
    {
        public float X, Y, Width, Height;
        public Collision.Box Box;
        public int QuadId;

        private Rectangle Rectangle;

        public Static(float x, float y, float width, float height, int id)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Box = new Collision.Box(x, y, width, height);
            this.QuadId = id;
            this.Rectangle = new Rectangle((int)x, (int)y, (int)width, (int)height);

            this.Box.Showing = false;
        }

        public void Update(float dt)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Canvas.Canvas canvas, Map map)
        {
            this.Rectangle.X = (int)Math.Floor((this.X - canvas.Camera.X) * canvas.Width / canvas.Camera.Width);
            this.Rectangle.Y = (int)Math.Floor((this.Y - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height);
            this.Rectangle.Width = (int)Math.Floor((this.X + map.Data.BlockSize - canvas.Camera.X) * canvas.Width / canvas.Camera.Width) - this.Rectangle.X;
            this.Rectangle.Height = (int)Math.Floor((this.Y + map.Data.BlockSize - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height) - this.Rectangle.Y;

            spriteBatch.Draw(map.TileSet, this.Rectangle, map.Quads[this.QuadId], Color.White);
            this.Box.Draw(spriteBatch, canvas);
        }
    }
}
