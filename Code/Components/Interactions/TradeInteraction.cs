using Game1.Code.GUI_Elements;
using Game1.Code.Managers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Components.Interactions
{
	class TradeInteraction : Interaction
	{


		public TradeInteraction()
		{
			
		}
	
		public override void action()
		{

			var inventory = GetComponent<Inventory>(ComponentType.Inventory);
			if (inventory == null) return;

			var ani = GetComponent<Animation>(ComponentType.Animation);
			var sprite = GetComponent<Sprite>(ComponentType.Sprite);

			if (ani != null)
			{
				ani.currDirection = PlayerManager.DirectionToPlayer(sprite.Position);
				ani.Stand();
			}

			WindowManager.newWindow(new WindowShop(inventory));
		}

		public override void Draw(SpriteBatch spritebatch)
		{

		}

		public override void Update(double gameTime)
		{
		}

	}
}
