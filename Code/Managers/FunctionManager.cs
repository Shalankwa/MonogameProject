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
		private static int ID = 0;

		public static int Random(int min, int max)
		{
			return _rnd.Next(min, max + 1);
		}

		public static int getID()
		{
			return ID++;
		}

		public static void resetIDs()
		{
			ID = 0;
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

			// Determine float value for layer based on Y location + the layer they are on
			float flayer = (float)(destination.Y / 2000.0 + (Layer * (2000.0 / 15) / 2000.0));
			// Clamp value between 0 - 1
			if (flayer < 0) flayer = 0;
			if (flayer > 1) flayer = 1;

			// Draw at position and layer
			spriteBatch.Draw(texture, destination, sourceRectangle, Color.White, 0, new Vector2(0,0), SpriteEffects.None, flayer);
		}

		public static void DrawAtLayer(Texture2D texture, Rectangle destination, Nullable<Rectangle> sourceRectangle, Nullable<int> Layer, Nullable<Color> colour, SpriteBatch spriteBatch)
		{
			if (Layer == null) Layer = 0;
			if (colour == null) colour = Color.White;


			// Determine float value for layer based on Y location + the layer they are on
			float flayer = (float)(destination.Y / 2000.0 + (Layer * (2000.0 / 15) / 2000.0));
			// Clamp value between 0 - 1
			if (flayer < 0) flayer = 0;
			if (flayer > 1) flayer = 1;


			// Draw at position and layer
			spriteBatch.Draw(texture, destination, sourceRectangle, colour.Value, 0, new Vector2(0, 0), SpriteEffects.None, flayer);
		}

		public static void DrawGUI(Texture2D texture, Rectangle destination, Nullable<Rectangle> sourceRectangle, Nullable<Color> colour, SpriteBatch spriteBatch)
		{
			if (colour == null) colour = Color.White;
			// Draw at position and layer
			spriteBatch.Draw(texture, destination, sourceRectangle, colour.Value, 0, new Vector2(0, 0), SpriteEffects.None, 1);
		}

		internal static void DrawAtLayer(Texture2D texture, Rectangle destination, SpriteBatch spriteBatch)
		{
			float flayer = texture.Bounds.Y / 240;
			spriteBatch.Draw(texture, destination, destination, Color.White, 0, new Vector2(0,0), SpriteEffects.None, flayer);
		}
	}
}
