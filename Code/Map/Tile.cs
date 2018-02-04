using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Map
{
	class Tile
	{
		private const int width = 16;
		private const int height = 16;


		public int xPos { get; set; }
		public int yPos { get; set; }
		public int zPos { get; set; }

		public int TextureXPos { get; set; }
		public int TextureYPos { get; set; }
		private Texture2D texture;
		public string textureName;

		public Tile()
		{

		}

		public Tile(int XPos, int YPos, int ZPos, int TXPos, int TYPos, string Tname)
		{
			xPos = XPos;
			yPos = YPos;
			zPos = ZPos;
			TextureXPos = TXPos;
			TextureYPos = TYPos;
			textureName = Tname;
		}

		public void LoadContent(ContentManager content)
		{
			texture = content.Load<Texture2D>(textureName);
		}

		public void Update(double gameTime)
		{

		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, new Rectangle(xPos * width, yPos * height, width, height),
				new Rectangle(TextureXPos * width, TextureYPos * height, width, height), Color.White);
		}
	}
}
