using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.GUI_Elements;
using Game1.Code.Managers;
using Game1.Code.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.Interactions
{
	class RequirementInteraction : Interaction
	{

		private bool complete;
		private double _count;
		private float _opacity;

		public RequirementInteraction()
		{
			_count = 0;
			_opacity = 1f;
		}

		public override void action()
		{
			if (complete) return;

			var inventory = PlayerManager.player.GetComponent<Inventory>(ComponentType.Inventory);

			if (inventory.hasItem(1))
			{
				complete = true;
				WindowManager.newWindow(new WindowMessage("It's dangerous........"));
			}
			else
			{
				WindowManager.newWindow(new WindowMessage("Do you have the thing buddy??"));
			}
		}

		public override void Draw(SpriteBatch spritebatch)
		{

		}

		public override void Update(double gameTime)
		{
			if (complete)
			{
				var sprite = GetComponent<Sprite>(ComponentType.Sprite);

				_count += gameTime;

				if(_count >= 200)
				{
					sprite.colour *= (_opacity -= 0.02f);
				}
				if(_opacity <= 0)
				{
					KillBase();
					ScreenTown.hasDude = false;
				}
			}
		}
	}
}
