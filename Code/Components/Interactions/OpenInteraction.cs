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
	class OpenInteraction : Interaction
	{
		public override void action()
		{
			var ani = GetComponent<Animation>(ComponentType.Animation);
			if (ani == null) return;

			ani.PlayAnimation();

			var playerinventory = PlayerManager.player.GetComponent<Inventory>(ComponentType.Inventory);
			playerinventory._currency += 200;

			WindowManager.newWindow(new WindowMessage("You recieved 200 gold"));

			RemoveComp();
		}

		public override void Draw(SpriteBatch spritebatch)
		{

		}

		public override void Update(double gameTime)
		{

		}
	}
}
