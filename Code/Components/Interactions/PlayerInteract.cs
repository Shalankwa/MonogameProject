using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.Interactions
{
	class PlayerInteract : Interaction
	{
		private Entities _entities;

		public PlayerInteract(Entities entities)
		{
			_entities = entities;
		}

		public override void Reload(Entities entities)
		{
			_entities = entities;
		}

		public override void action()
		{
			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			if (sprite == null) return;

			var ani = GetComponent<Animation>(ComponentType.Animation);
			if (ani == null) return;

			Interact(sprite, ani);
		}

		private void Interact(Sprite sprite, Animation ani)
		{
			Rectangle interactBox = sprite.Rectangle;


			switch (ani.currDirection)
			{
				case Direction.Up:
					interactBox.Y -= 10;
					break;
				case Direction.Down:
					interactBox.Y += 10;
					break;
				case Direction.Left:
					interactBox.X -= 10;
					break;
				case Direction.Right:
					interactBox.X += 10;
					break;
			}

			BaseObject hitObj;
			
			if(_entities.CheckInteraction(interactBox, out hitObj, base.GetOwnerId()))
			{
				hitObj.GetComponent<Interaction>(ComponentType.Interaction).action();
			}

		}

		public override void Draw(SpriteBatch spritebatch)
		{
			
		}

		public override void Update(double gameTime)
		{
			
		}
	}
}
