using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.Pickups
{
	class PickUp : Component
	{
		public override ComponentType ComponentType
		{
			get { return ComponentType.PickUp; }
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			
		}

		public override void Update(double gameTime)
		{
			var psprite = PlayerManager.player.GetComponent<Sprite>(ComponentType.Sprite);
			if (psprite == null) return;

			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			if (sprite == null) return;

			if (sprite.Rectangle.Intersects(psprite.Rectangle))
			{
				var inv = PlayerManager.player.GetComponent<Inventory>(ComponentType.Inventory);
				if (inv == null) return;

				inv._currency += 100;

				KillBase();
			}


		}
	}
}
