using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AdelieEngine.Collision
{
    public class Box
    {
        public float X, Y, Width, Height;

        public Box(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public bool IsCollidingHorizontally(Box other)
        {
            return this.X + this.Width > other.X && other.X + other.Width > this.X;
        }

        public bool IsCollidingHorizontally(float x, float width)
        {
            return this.X + this.Width > x && x + width > this.X;
        }

        public bool IsCollidingVertically(Box other)
        {
            return this.Y + this.Height > other.Y && other.Y + other.Height > this.Y;
        }

        public bool IsCollidingVertically(float y, float height)
        {
            return this.Y + this.Height > y && y + height > this.Y;
        }

        public bool IsColliding(Box other)
        {
            return this.IsCollidingHorizontally(other) && this.IsCollidingVertically(other);
        }

        public bool IsColliding(float x, float y, float width, float height)
        {
            return this.IsCollidingHorizontally(x, width) && this.IsCollidingVertically(y, height);
        }
    }
}
