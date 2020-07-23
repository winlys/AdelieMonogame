using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdelieEngine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdelieEngine.Entity
{
    public class Entity
    {
        public float X, Y, Width, Height;
        public Vector2 Velocity = new Vector2(0, 0);

        public Collision.Box Box;
        public Sprite.Sprite Sprite;
        private Sprite.Sprite sprite;

        public Data.Data Data;

        public string Type = "Entity";

        public Entity(Data.Data data)
        {
            this.Data = data;
            this.Box = new Collision.Box(0, 0, 0, 0);
            this.Sprite = new Sprite.Sprite();
        }

        public Entity(float x, float y, Sprite.Sprite sprite)
        {
            this.X = x;
            this.Y = y;
            this.Width = 0;
            this.Height = 0;
            this.Box = new Collision.Box(x, y, 0, 0);
            this.Sprite = sprite;
        }

        public Entity(float x, float y, float width, float height, Sprite.Sprite sprite)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Box = new Collision.Box(x, y, width, height);
            this.Sprite = sprite;
        }
        
        public virtual void Update(float dt)
        {
            //Collision box
            this.Box.X = this.X;
            this.Box.Y = this.Y;
            this.Box.Width = this.Width;
            this.Box.Height = this.Height;

            //Sprite
            this.Sprite.Position.X = this.X;
            this.Sprite.Position.Y = this.Y;

            this.Sprite.Update(dt);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Canvas.Canvas canvas)
        {
            //this.Box.Showing = true;
            this.Sprite.Draw(spriteBatch, canvas);
            this.Box.Draw(spriteBatch, canvas);
        }


        public virtual void ShowCollisionBox()
        {
            this.Box.Showing = true;
        }

        public virtual void HideCollisionBox()
        {
            this.Box.Showing = false;
        }

        public virtual void ToggleCollisionBox()
        {
            this.Box.Showing = !this.Box.Showing;
        }
    }
}
