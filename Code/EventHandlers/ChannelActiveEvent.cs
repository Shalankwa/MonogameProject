using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.EventHandlers
{
	class ChannelActiveEvent : EventArgs
	{
		public int Channel { get; private set; }

		public ChannelActiveEvent(int channel)
		{
			Channel = channel;
		}

	}
}
