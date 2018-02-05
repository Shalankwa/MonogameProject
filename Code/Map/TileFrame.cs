using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Map
{
	class TileFrame
	{

		public int TextureXPos { get; set; }
		public int TextureYPos { get; set; }

		public TileFrame(int x, int y)
		{
			TextureXPos = x;
			TextureYPos = y;
		}
	}
}
