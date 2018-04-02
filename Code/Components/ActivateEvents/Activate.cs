using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components.Interactions;
using Game1.Code.EventHandlers;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.ActivateEvents
{
	abstract class Activate : Component
	{
		protected int Channel;

		public Activate(int channel)
		{
			Channel = channel;
			Initialize();
		}

		public override void Initialize()
		{
			ActivateChannelInteraction.OpenChannel += ActivateEvent;
		}

		protected abstract void ActivateEvent(object sender, ChannelActiveEvent e);


		public override void Uninitalize()
		{
			ActivateChannelInteraction.OpenChannel -= ActivateEvent;
		}

		public override ComponentType ComponentType
		{
			get { return ComponentType.Activate; }
		}

	}
}
