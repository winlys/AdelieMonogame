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
    public class Map : Entity
    {
        public new Data.MapData Data;

        public Texture2D TileSet;

        public List<Room> Rooms = new List<Room>();
        public List<Rectangle> Quads = new List<Rectangle>();

        public Map(Data.MapData data, Texture2D tileset) : base(data)
        {
            this.Data = data;
            this.TileSet = tileset;

            //Initialize tileset and make quads
            for (int y = 0; y < (int)Math.Floor(this.TileSet.Height / this.Data.BlockSize); y++)
            {
                for (int x = 0; x < (int)Math.Floor(this.TileSet.Width / this.Data.BlockSize); x++)
                {
                    this.Quads.Add(new Rectangle((int)(x * this.Data.BlockSize), (int)(y * this.Data.BlockSize), (int)this.Data.BlockSize, (int)this.Data.BlockSize));
                }
            }

            //Create rooms from database and initialize these rooms
            for (int i = 0; i < this.Data.Rooms.Count; i++)
            {
                this.Rooms.Add(new Room(this.Data.Rooms[i][0], this.Data.Rooms[i][1], this.Data.Rooms[i][2], this.Data.Rooms[i][3]));
                this.Rooms[i].Initialize(this);
            }
        }

        public override void Update(float dt)
        {
            //base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.Rooms.Count; i++)
            {
                this.Rooms[i].Draw(spriteBatch, this);
            }
            //base.Draw(spriteBatch);
        }
    }
}
