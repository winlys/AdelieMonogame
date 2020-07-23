using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdelieEngine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdelieEngine.Graphic
{
    public static class GraphicManager
    {
        public static Texture2D WhiteBox;

        public static void Initialize(Game game, GraphicsDeviceManager graphic)
        {
            GraphicManager.WhiteBox = new Texture2D(game.GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            GraphicManager.WhiteBox.SetData<Color>(data);
        }
    }
}
