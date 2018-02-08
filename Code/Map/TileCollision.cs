using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Map
{
	class TileCollision
	{
		public int Xbound { get; set; }
		public int Ybound { get; set; }
		private int Width { get; set; }
		private int Height { get; set; }
		private int Xoffset { get; set; }
		private int Yoffset { get; set; }

		public Rectangle Rectangle { get { return new Rectangle(Xbound*16 + Xoffset, Ybound*16 + Yoffset, Width, Height); } }
		public Vector2 Position { get { return new Vector2(Rectangle.X, Rectangle.Y); } }

		public CameraManager cameraManager { get; set; }

		public bool Intersect(Rectangle rectangle)
		{
			return cameraManager.InScreenCheck(Position) && rectangle.Intersects(new Rectangle((int)Position.X, (int)Position.Y, Width, Height));
		}

		public TileCollision(int xBound, int yBound)
		{
			Xbound = xBound;
			Ybound = yBound;
			Width = 16;
			Height = 16;
			Xoffset = 0;
			Yoffset = 0;
		}

		public TileCollision(int xBound, int yBound, int width, int height, int xOffset, int yOffset)
		{
			Xbound = xBound;
			Ybound = yBound;
			Width = width;
			Height = height;
			Xoffset = xOffset;
			Yoffset = yOffset;
		}
	}
}
