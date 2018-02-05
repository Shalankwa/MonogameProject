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

		List<TileFrame> tileFrames;
		public int animationSpeed { get; set; }
		private double _frameCounter = 0;
		private int _animationIndex = 0;

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
			tileFrames = new List<TileFrame>(1);
			tileFrames.Add(new TileFrame(TXPos, TYPos));
			textureName = Tname;
		}

		public Tile(int XPos, int YPos, int ZPos, List<TileFrame> frames, int aniS, string Tname)
		{

			xPos = XPos;
			yPos = YPos;
			zPos = ZPos;
			tileFrames = frames;
			animationSpeed = aniS;
			textureName = Tname;
		}

		public void LoadContent(ContentManager content)
		{
			texture = content.Load<Texture2D>(textureName);
		}

		public void Update(double gameTime)
		{
			if (tileFrames.Count <= 1) return;

			_frameCounter += gameTime;
			if(_frameCounter >= animationSpeed)
			{
				_frameCounter = 0;
				_animationIndex++;
				if (_animationIndex >= tileFrames.Count) 
					_animationIndex = 0;
			}
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, new Rectangle(xPos * width, yPos * height, width, height),
				new Rectangle(tileFrames[_animationIndex].TextureXPos * width, tileFrames[_animationIndex].TextureYPos * height,
				width, height), Color.White);
		}
	}
}
