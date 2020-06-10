using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AdelieEngine.Scene
{
    public class Manager
    {
        public List<Scene> Scenes;
        public List<Sprite.AnimationSprite> Transitions;

        public Scene CurrentScene;
        public Scene NextScene;

        public Sprite.AnimationSprite CurrentTransition;

        public Canvas.Canvas TransitionCanvas;

        private bool Switching = false;

        public Manager()
        {
            this.Scenes = new List<Scene>();
            this.Scenes.Add(new Scene("Default"));
            this.CurrentScene = this.Scenes[0];
        }

        public int FindSceneId(string name)
        {
            for (int i = 0; i < this.Scenes.Count; i++)
            {
                if (this.Scenes[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SwitchTo(Scene next, Sprite.AnimationSprite transition)
        {
            if (!this.Switching)
            {
                this.NextScene = next;
                this.CurrentTransition = transition;
                this.CurrentTransition.FrameCurrent = this.CurrentTransition.FrameStart;
                this.CurrentTransition.Playing = true;
                this.Switching = true;
            }
        }

        public void Update(Game game, GraphicsDeviceManager graphics, float deltaTime)
        {
            //Update transition
            if (this.Switching)
            {
                this.CurrentTransition.Update(deltaTime);
                if (this.CurrentTransition.FrameCurrent == (int)(this.CurrentTransition.FrameEnd / 2))
                {
                    this.CurrentScene = this.NextScene;
                }
                else if (this.CurrentTransition.FrameCurrent == this.CurrentTransition.FrameEnd)
                {
                    this.Switching = false;
                }
            }

            //Update current screen
            if (this.CurrentScene != null)
            {
                this.CurrentScene.Update(game, graphics, deltaTime);
            }
        }

        public void Draw(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            //Draw current screen
            if (this.CurrentScene != null)
            {
                this.CurrentScene.Draw(game, graphics, spriteBatch);
            }

            //Draw transition
            if (this.Switching)
            {
                game.GraphicsDevice.SetRenderTarget(this.TransitionCanvas.RenderTarget);
                game.GraphicsDevice.Clear(Color.Transparent);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
                if (this.CurrentTransition != null)
                {
                    this.CurrentTransition.Draw(spriteBatch);
                }
                spriteBatch.End();
                game.GraphicsDevice.SetRenderTarget(null);
            }
           
        }
    }
}
