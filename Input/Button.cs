using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AdelieEngine.Input
{
    public class Button
    {
        public Keys Key;
        private int count_down = 0;
        private int count_up = 0;

        public Button(Keys key)
        {
            this.Key = key;
        }

        public bool IsDown()
        {
            return Keyboard.GetState().IsKeyDown(this.Key);
        }

        public bool IsUp()
        {
            return !Keyboard.GetState().IsKeyDown(this.Key);
        }

        public bool JustDown()
        {
            if (this.IsDown())
            {
                this.count_down++;
            }
            else
            {
                this.count_down = 0;
            }
            this.count_down = Math.Min(this.count_down, 2);
            return this.count_down == 1;
        }

        public bool JustUp()
        {
            if (this.IsUp())
            {
                this.count_up++;
            }
            else
            {
                this.count_up = 0;
            }
            this.count_up = Math.Min(this.count_up, 2);
            return this.count_up == 1;
        }
    }
}
