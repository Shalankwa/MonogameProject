using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code
{
	abstract class Component
	{
		protected BaseObject base_Object;

		public abstract ComponentType ComponentType { get; }

		public void Initilize(BaseObject baseObject)
		{
			base_Object = baseObject;
		}

		//Get parents ID
		public int GetOwnerId()
		{
			return base_Object.Id;
		}

		public void KillBase()
		{
			var Drops = GetComponent<Drop>(ComponentType.Drop);
			if (Drops != null)
			{
				Drops.dropItem();
			}

			base_Object.Dead = true;
		}

		public bool GetHosility()
		{
			return base_Object.Hostile;
		}

		//Delete this component from parent
		public void RemoveComp()
		{
			base_Object.RemoveComponent(this);
		}

		public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
		{
			return base_Object.GetComponent<TComponentType>(componentType);
		}

		public abstract void Update(double gameTime);
		public abstract void Draw(SpriteBatch spritebatch);

		public virtual void Initialize() { }
		public virtual void Uninitalize() { }
    }
}
