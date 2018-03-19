using Game1.Code.Loader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.EventHandlers
{
	class NewMapObjectEvent : EventArgs
	{
		public Objects newObject { get; set; }
		public Vector2 position { get; set; }
		public Dictionary<string, string> properties { get; set; }

		public NewMapObjectEvent(Objects obj, Vector2 pos, Dictionary<string, string> props)
		{
			newObject = obj;
			position = pos;
			properties = props;
		}
	}
}
