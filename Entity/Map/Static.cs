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
        }

        public void Update(float dt)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Map map)
        {
            spriteBatch.Draw(map.TileSet, this.Rectangle, map.Quads[this.QuadId], Color.White);
        }
    }
}
