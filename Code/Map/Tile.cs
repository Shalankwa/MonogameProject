using Game1.Code.Managers;
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
		public int layer { get; set; }

		public Vector2 Position { get { return new Vector2(xPos*width, yPos*height); } }

		public int TextureXPos { get; set; }
		public int TextureYPos { get; set; }

		List<TileFrame> tileFrames;
		public int animationSpeed { get; set; }
		private double _frameCounter = 0;
		private int _animationIndex = 0;

		private Texture2D texture;
		public string textureName;
		public CameraManager cameraManager { get; set; }

		public Tile()
		{

		}

		public Tile(int XPos, int YPos, int ZPos, int TXPos, int TYPos, string Tname, int Layer)
		{
			xPos = XPos;
			yPos = YPos;
			zPos = ZPos;
			tileFrames = new List<TileFrame>(1);
			tileFrames.Add(new TileFrame(TXPos, TYPos));
			textureName = Tname;
			layer = Layer;

		}

		public Tile(int XPos, int YPos, int ZPos, List<TileFrame> frames, int aniS, string Tname, int Layer)
		{
			xPos = XPos;
			yPos = YPos;
			zPos = ZPos;
			tileFrames = frames;
			animationSpeed = aniS;
			textureName = Tname;
			layer = Layer;
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
			var screenPos = cameraManager.WorldToScreenPosition(Position);
			if (cameraManager.InScreenCheck(Position))
			{

				//FunctionManager.DrawAtLayer(my_texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), ani.TextureRectangle, null, spritebatch);

				FunctionManager.DrawAtLayer(texture, new Rectangle((int)screenPos.X, (int)screenPos.Y, width, height),
					new Rectangle(tileFrames[_animationIndex].TextureXPos * width, 
								tileFrames[_animationIndex].TextureYPos * height,
								width, height), layer, sb);
			}
		}
	}
}
