using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdelieEngine;

namespace AdelieEngine.Entity.Map
{
    public class Room
    {
        public List<Static> Statics = new List<Static>();

        public int X, Y, Width, Height;

        public Room(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public void Initialize(Map map)
        {
            for (int i = 0; i < map.Data.BlockData.Length; i++)
            {
                if (map.Data.BlockData[i] == 0)
                {
                    continue;
                }

                if ((int)Math.Floor(i % map.Data.Width) >= this.X - 1 && (int)Math.Floor(i / map.Data.Width) >= this.Y - 1 
                    && (int)Math.Floor(i % map.Data.Width) <= this.X + this.Width && (int)Math.Floor(i / map.Data.Width) <= this.Y + this.Height)
                {
                    this.Statics.Add(new Static((int)Math.Floor(i % map.Data.Width) * map.Data.BlockSize, (int)Math.Floor(i / map.Data.Width) * map.Data.BlockSize, map.Data.BlockSize, map.Data.BlockSize, map.Data.BlockData[i] - 1));
                }
            }
        }

        public void Update(float dt, Map map)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Canvas.Canvas canvas, Map map)
        {
            for (int i = 0; i < this.Statics.Count; i++)
            {
                this.Statics[i].Draw(spriteBatch, canvas, map);
            }
        }
    }
}
