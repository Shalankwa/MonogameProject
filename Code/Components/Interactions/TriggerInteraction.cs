using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.Interactions
{
	class TriggerInteraction : Interaction
	{
		public override void action()
		{
			var ani = GetComponent<Animation>(ComponentType.Animation);
			if (ani == null) return;

			ani.PlayAnimation();

		}

		public override void Draw(SpriteBatch spritebatch)
		{

		}

		public override void Update(double gameTime)
		{

		}
	}
}
