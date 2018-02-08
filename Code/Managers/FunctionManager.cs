using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	class FunctionManager
	{
		private static Random _rnd = new Random();

		public static int Random(int min, int max)
		{
			return _rnd.Next(min, max + 1);
		}

		public static double DistanceTo(Vector2 pos1, Vector2 pos2)
		{
			double x = Math.Pow(pos1.X - pos2.X, 2);
			double y = Math.Pow(pos1.Y - pos2.Y, 2);
			return Math.Sqrt(x + y);
		}

		public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
		{
			if (value.CompareTo(min) < 0)
				return min;
			if (value.CompareTo(max) > 0)
				return max;

			return value;
		}

		public static void DrawAtLayer(Texture2D texture, Rectangle destination, Nullable<Rectangle> sourceRectangle, Nullable<int> Layer, SpriteBatch spriteBatch)
		{
			if (Layer == null) Layer = 0;

			float flayer = (float)(destination.Y / 240.0 + (Layer * (240 / 15) / 240.0));
			if (flayer < 0) flayer = 0;
			if (flayer > 1) flayer = 1;
			//Debug.Print("drawn on layer: " + flayer + " with ybound: " + destination.Y);

			spriteBatch.Draw(texture, destination, sourceRectangle, Color.White, 0, new Vector2(0,0), SpriteEffects.None, flayer);
		}

		internal static void DrawAtLayer(Texture2D texture, Rectangle destination, SpriteBatch spriteBatch)
		{
			
			float flayer = texture.Bounds.Y / 240;
			spriteBatch.Draw(texture, destination, destination, Color.White, 0, new Vector2(0,0), SpriteEffects.None, flayer);
		}
	}
}
