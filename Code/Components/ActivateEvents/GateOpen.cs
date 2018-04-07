using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.EventHandlers;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.ActivateEvents
{
	class GateOpen : Activate
	{
		public GateOpen(int channel) : base(channel)
		{


		}

		public override void Draw(SpriteBatch spritebatch)
		{
			
		}

		public override void Update(double gameTime)
		{
			
		}

		protected override void ActivateEvent(object sender, ChannelActiveEvent e)
		{
			if (e.Channel != Channel) return;

			var ani = GetComponent<Animation>(ComponentType.Animation);
			if (ani == null) return;

			var col = GetComponent<Collision>(ComponentType.Collision);
			if (ani == null) return;

			ani.PlayAnimation();
			
			col.imPassable = (ani.animationIndex >= 0);

		}
	}
}
