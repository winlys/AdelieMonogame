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
            this.NextScene = next;
            this.CurrentTransition = transition;
            this.Switching = true;
        }

        public void Update(float dt)
        {

        }
    }
}
