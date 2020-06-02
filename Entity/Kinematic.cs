using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdelieEngine.Entity
{
    public class Kinematic : Entity
    {
        public Kinematic(float x, float y, float width, float height, Sprite.Sprite sprite) : base(x, y, width, height, sprite)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Box = new Collision.Box(x, y, width, height);
            this.Sprite = sprite;
        }

        public override void Update(float dt)
        {
            //Update moving here
            base.Update(dt);
        }
    }
}
