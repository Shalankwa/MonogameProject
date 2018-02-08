using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.EventHandlers
{
	class CameraTransitionEvent : EventArgs
	{
		public Direction Direction { get; set; }
		
		public CameraTransitionEvent(Direction direction)
		{
			Direction = direction;
		}
	}
}
