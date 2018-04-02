using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.EventHandlers;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.Interactions
{
	class ActivateChannelInteraction : Interaction
	{

		private static event EventHandler<ChannelActiveEvent> _OpenChannel;

		//Static access to subscribe to event
		public static event EventHandler<ChannelActiveEvent> OpenChannel
		{
			add { _OpenChannel += value; }
			remove { _OpenChannel -= value; }
		}

		private int Channel;

		public ActivateChannelInteraction(int channel)
		{
			Channel = channel;
		}

		public override void action()
		{
			var ani = GetComponent<Animation>(ComponentType.Animation);
			if (ani == null) return;

			if (ani.currState == State.Play) return;

			ani.PlayAnimation();

			if(_OpenChannel != null)
			{
				_OpenChannel(null, new ChannelActiveEvent(Channel));
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
