using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code
{
    class BaseObject
    {
        public int Id { get; set; }
		public bool Dead { get; set; }
		public bool Hostile { get; set; }

		private readonly List<Component> components;

        //Construct
        public BaseObject()
        {
			Hostile = false;
			Dead = false;
            components = new List<Component>();
        }

        //Unsure Look into later, looks like finder metheod for components in component list
        public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
        {
            return components.Find(c => c.ComponentType == componentType) as TComponentType;
        }

        //Remove a componet from this BaseObject
        public void RemoveComponent(Component comp)
        {
            components.Remove(comp);
        }

        //Add a componet to this BaseObject
        public void AddComponent(Component comp)
        {
            components.Add(comp);
            comp.Initilize(this);
        }

        //Add componets to this BaseObject
        public void AddComponent(List<Component> comps)
        {
            components.AddRange(components);
            foreach (var comp in comps)
            {
                comp.Initilize(this);
            }
        }

        //Update all components attached to this BaseObject
        public virtual void Update(double gameTime)
        {
            foreach(var componet in components)
            {
                componet.Update(gameTime);
            }
        }

        //Draw all components attached to this BaseObject
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var componet in components)
            {
                componet.Draw(spriteBatch);
            }
        }
    }
}
