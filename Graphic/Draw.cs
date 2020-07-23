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
    public static class Draw
    {
        private static Rectangle rectangle = new Rectangle(0, 0, 0, 0);
        private static Rectangle rectangle1 = new Rectangle(0, 0, 0, 0);
        private static Rectangle rectangle2 = new Rectangle(0, 0, 0, 0);
        private static Rectangle rectangle3 = new Rectangle(0, 0, 0, 0);
        private static Rectangle rectangle4 = new Rectangle(0, 0, 0, 0);

        private static Vector2 vector1 = Vector2.Zero;
        private static Vector2 vector2 = Vector2.Zero;
        private static Vector2 vectorScale = Vector2.Zero;

        private static float angle = 0;
        private static float length = 0;

        //Draw line
        public static void Line(SpriteBatch spriteBatch, Canvas.Canvas canvas, float x1, float y1, float x2, float y2)
        {
            Draw.vector1.X = (x1 - canvas.Camera.X) * canvas.Width / canvas.Camera.Width;
            Draw.vector1.Y = (y1 - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height;
            Draw.vector2.X = (x2 - canvas.Camera.X) * canvas.Width / canvas.Camera.Width;
            Draw.vector2.Y = (y2 - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height;

            Draw.angle = (float)Math.Atan2(Draw.vector2.Y - Draw.vector1.Y, Draw.vector2.X - Draw.vector1.X);
            Draw.length = (float)Math.Sqrt((Draw.vector2.Y - Draw.vector1.Y) * (Draw.vector2.Y - Draw.vector1.Y) + (Draw.vector2.X - Draw.vector1.X) * (Draw.vector2.X - Draw.vector1.X));

            Draw.vectorScale.X = Draw.length;

            if (y2 == y1)
            {
                Draw.vectorScale.Y = canvas.Height / canvas.Camera.Height;
            }
            else if (x2 == x1)
            {
                Draw.vectorScale.Y = canvas.Width / canvas.Camera.Width;
            }
            else
            {
                Draw.vectorScale.Y = (float)Math.Sqrt((1 / (x2 - x1) / (x2 - x1)) + (1 / (y2 - y1) / (y2 - y1))) / 
                    (float)Math.Sqrt((1 / (Draw.vector2.X - Draw.vector1.X) / (Draw.vector2.X - Draw.vector1.X)) + (1 / (Draw.vector2.Y - Draw.vector1.Y) / (Draw.vector2.Y - Draw.vector1.Y)));
            }

            spriteBatch.Draw(GraphicManager.WhiteBox, Draw.vector1, null, Color.Red, Draw.angle, Vector2.UnitY / 2, Draw.vectorScale, SpriteEffects.None, 0f);
        }

        public static void Line(SpriteBatch spriteBatch, Canvas.Canvas canvas, float x1, float y1, float x2, float y2, Color color)
        {
            Draw.vector1.X = (x1 - canvas.Camera.X) * canvas.Width / canvas.Camera.Width;
            Draw.vector1.Y = (y1 - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height;
            Draw.vector2.X = (x2 - canvas.Camera.X) * canvas.Width / canvas.Camera.Width;
            Draw.vector2.Y = (y2 - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height;

            Draw.angle = (float)Math.Atan2(Draw.vector2.Y - Draw.vector1.Y, Draw.vector2.X - Draw.vector1.X);
            Draw.length = (float)Math.Sqrt((Draw.vector2.Y - Draw.vector1.Y) * (Draw.vector2.Y - Draw.vector1.Y) + (Draw.vector2.X - Draw.vector1.X) * (Draw.vector2.X - Draw.vector1.X));

            Draw.vectorScale.X = Draw.length;

            if (y2 == y1)
            {
                Draw.vectorScale.Y = canvas.Height / canvas.Camera.Height;
            }
            else if (x2 == x1)
            {
                Draw.vectorScale.Y = canvas.Width / canvas.Camera.Width;
            }
            else
            {
                Draw.vectorScale.Y = (float)Math.Sqrt((1 / (x2 - x1) / (x2 - x1)) + (1 / (y2 - y1) / (y2 - y1))) /
                    (float)Math.Sqrt((1 / (Draw.vector2.X - Draw.vector1.X) / (Draw.vector2.X - Draw.vector1.X)) + (1 / (Draw.vector2.Y - Draw.vector1.Y) / (Draw.vector2.Y - Draw.vector1.Y)));
            }

            spriteBatch.Draw(GraphicManager.WhiteBox, Draw.vector1, null, color, Draw.angle, Vector2.UnitY / 2, Draw.vectorScale, SpriteEffects.None, 0f);
        }

        public static void Line(SpriteBatch spriteBatch, Canvas.Canvas canvas, float x1, float y1, float x2, float y2, Color color, float lineWidth)
        {
            Draw.vector1.X = (x1 - canvas.Camera.X) * canvas.Width / canvas.Camera.Width;
            Draw.vector1.Y = (y1 - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height;
            Draw.vector2.X = (x2 - canvas.Camera.X) * canvas.Width / canvas.Camera.Width;
            Draw.vector2.Y = (y2 - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height;

            Draw.angle = (float)Math.Atan2(Draw.vector2.Y - Draw.vector1.Y, Draw.vector2.X - Draw.vector1.X);
            Draw.length = (float)Math.Sqrt((Draw.vector2.Y - Draw.vector1.Y) * (Draw.vector2.Y - Draw.vector1.Y) + (Draw.vector2.X - Draw.vector1.X) * (Draw.vector2.X - Draw.vector1.X));

            Draw.vectorScale.X = Draw.length;

            if (y2 == y1)
            {
                Draw.vectorScale.Y = canvas.Height / canvas.Camera.Height;
            }
            else if (x2 == x1)
            {
                Draw.vectorScale.Y = canvas.Width / canvas.Camera.Width;
            }
            else
            {
                Draw.vectorScale.Y = lineWidth * (float)Math.Sqrt((1 / (x2 - x1) / (x2 - x1)) + (1 / (y2 - y1) / (y2 - y1))) /
                    (float)Math.Sqrt((1 / (Draw.vector2.X - Draw.vector1.X) / (Draw.vector2.X - Draw.vector1.X)) + (1 / (Draw.vector2.Y - Draw.vector1.Y) / (Draw.vector2.Y - Draw.vector1.Y)));
            }

            spriteBatch.Draw(GraphicManager.WhiteBox, Draw.vector1, null, color, Draw.angle, Vector2.UnitY / 2, Draw.vectorScale, SpriteEffects.None, 0f);
        }

        //Draw rectangle
        public static void Rectangle(SpriteBatch spriteBatch, Canvas.Canvas canvas, float x, float y, float width, float height, bool fill)
        {
            Draw.rectangle.X = (int)Math.Floor((x - canvas.Camera.X) * canvas.Width / canvas.Camera.Width);
            Draw.rectangle.Y = (int)Math.Floor((y - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height);
            Draw.rectangle.Width = (int)Math.Floor((x + width - canvas.Camera.X) * canvas.Width / canvas.Camera.Width) - Draw.rectangle.X;
            Draw.rectangle.Height = (int)Math.Floor((y + height - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height) - Draw.rectangle.Y;

            if (fill)
            {
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle, Color.Red);
            }
            else
            {
                //Top side of rectangle
                Draw.rectangle1 = Draw.rectangle;
                Draw.rectangle1.Height = (int)Math.Floor(canvas.Height / canvas.Camera.Height);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle1, Color.Red);

                //Left side of rectangle
                Draw.rectangle2 = Draw.rectangle;
                Draw.rectangle2.Width = (int)Math.Floor(canvas.Width / canvas.Camera.Width);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle2, Color.Red);

                //Bottom side of rectangle
                Draw.rectangle3 = Draw.rectangle;
                Draw.rectangle3.Y = Draw.rectangle.Y + Draw.rectangle.Height - (int)Math.Floor(canvas.Height / canvas.Camera.Height);
                Draw.rectangle3.Height = (int)Math.Floor(canvas.Height / canvas.Camera.Height);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle3, Color.Red);

                //Right side of rectangle
                Draw.rectangle4 = Draw.rectangle;
                Draw.rectangle4.X = Draw.rectangle.X + Draw.rectangle.Width - (int)Math.Floor(canvas.Width / canvas.Camera.Width);
                Draw.rectangle4.Width = (int)Math.Floor(canvas.Width / canvas.Camera.Width);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle4, Color.Red);
            }
        }

        public static void Rectangle(SpriteBatch spriteBatch, Canvas.Canvas canvas, float x, float y, float width, float height, bool fill, Color color)
        {
            Draw.rectangle.X = (int)Math.Floor((x - canvas.Camera.X) * canvas.Width / canvas.Camera.Width);
            Draw.rectangle.Y = (int)Math.Floor((y - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height);
            Draw.rectangle.Width = (int)Math.Floor((x + width - canvas.Camera.X) * canvas.Width / canvas.Camera.Width) - Draw.rectangle.X;
            Draw.rectangle.Height = (int)Math.Floor((y + height - canvas.Camera.Y) * canvas.Height / canvas.Camera.Height) - Draw.rectangle.Y;

            if (fill)
            {
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle, color);
            }
            else
            {
                //Top side of rectangle
                Draw.rectangle1 = Draw.rectangle;
                Draw.rectangle1.Height = (int)Math.Floor(canvas.Height / canvas.Camera.Height);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle1, color);

                //Left side of rectangle
                Draw.rectangle2 = Draw.rectangle;
                Draw.rectangle2.Width = (int)Math.Floor(canvas.Width / canvas.Camera.Width);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle2, color);

                //Bottom side of rectangle
                Draw.rectangle3 = Draw.rectangle;
                Draw.rectangle3.Y = Draw.rectangle.Y + Draw.rectangle.Height - (int)Math.Floor(canvas.Height / canvas.Camera.Height);
                Draw.rectangle3.Height = (int)Math.Floor(canvas.Height / canvas.Camera.Height);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle3, color);

                //Right side of rectangle
                Draw.rectangle4 = Draw.rectangle;
                Draw.rectangle4.X = Draw.rectangle.X + Draw.rectangle.Width - (int)Math.Floor(canvas.Width / canvas.Camera.Width);
                Draw.rectangle4.Width = (int)Math.Floor(canvas.Width / canvas.Camera.Width);
                spriteBatch.Draw(GraphicManager.WhiteBox, Draw.rectangle4, color);
            }
        }
    }
}
