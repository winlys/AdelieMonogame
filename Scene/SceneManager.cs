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
    public static class SceneManager
    {
        public static List<Scene> Scenes = new List<Scene>();
        public static List<Sprite.AnimationSprite> Transitions = new List<Sprite.AnimationSprite>();

        public static Scene CurrentScene;
        public static Scene NextScene;

        public static Sprite.AnimationSprite CurrentTransition;

        public static Canvas.Canvas TransitionCanvas;

        public static bool Switching = false;

        public static int FindSceneId(string name)
        {
            for (int i = 0; i < SceneManager.Scenes.Count; i++)
            {
                if (SceneManager.Scenes[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void SwitchTo(Scene next, Sprite.AnimationSprite transition)
        {
            if (!SceneManager.Switching)
            {
                SceneManager.NextScene = next;
                SceneManager.CurrentTransition = transition;
                SceneManager.CurrentTransition.FrameCurrent = SceneManager.CurrentTransition.FrameStart;
                SceneManager.CurrentTransition.Playing = true;
                SceneManager.Switching = true;
            }
        }

        public static void Initialize(Game game, GraphicsDeviceManager graphics)
        {
            SceneManager.TransitionCanvas = new Canvas.Canvas(game.GraphicsDevice, Engine.Width, Engine.Height, false);

            //Initialize scenes
            for (int i = 0; i < SceneManager.Scenes.Count; i++)
            {
                SceneManager.Scenes[i].Initialize(game, graphics);
            }
        }

        public static void LoadContent(Game game, GraphicsDeviceManager graphics)
        {
            //Load for all scenes
            for (int i = 0; i < SceneManager.Scenes.Count; i++)
            {
                SceneManager.Scenes[i].LoadContent(game, graphics);
            }
        }

        public static void UnloadContent(Game game, GraphicsDeviceManager graphics)
        {
            //Unload for all scenes
            for (int i = 0; i < SceneManager.Scenes.Count; i++)
            {
                SceneManager.Scenes[i].UnloadContent(game, graphics);
            }
        }

        public static void Update(Game game, GraphicsDeviceManager graphics, float deltaTime)
        {
            //Update transition
            if (SceneManager.Switching)
            {
                SceneManager.CurrentTransition.Update(deltaTime);
                if (SceneManager.CurrentTransition.FrameCurrent == (int)(SceneManager.CurrentTransition.FrameEnd / 2))
                {
                    SceneManager.CurrentScene = SceneManager.NextScene;
                }
                else if (SceneManager.CurrentTransition.FrameCurrent >= SceneManager.CurrentTransition.FrameEnd)
                {
                    SceneManager.Switching = false;
                }
            }

            //Update current screen
            if (SceneManager.CurrentScene != null)
            {
                SceneManager.CurrentScene.Update(game, graphics, deltaTime);
            }
        }

        public static void Draw(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            //Draw current screen
            if (SceneManager.CurrentScene != null)
            {
                SceneManager.CurrentScene.Draw(game, graphics, spriteBatch);
            }

            //Draw transition
            if (SceneManager.Switching)
            {
                game.GraphicsDevice.SetRenderTarget(SceneManager.TransitionCanvas.RenderTarget);
                game.GraphicsDevice.Clear(Color.Transparent);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
                if (SceneManager.CurrentTransition != null)
                {
                    SceneManager.CurrentTransition.Draw(spriteBatch);
                }
                spriteBatch.End();
                game.GraphicsDevice.SetRenderTarget(null);
            }
           
        }
    }
}
