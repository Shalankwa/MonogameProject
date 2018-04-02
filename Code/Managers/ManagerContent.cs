using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	static class ManagerContent
	{
		public static ContentManager content { get; private set; }

		public static void loadContent(ContentManager contentManager)
		{
			content = contentManager;
		}

		public static Texture2D LoadTexture(string texture)
		{
			return content.Load<Texture2D>(texture);
		}

		public static SpriteFont LoadFont(string font)
		{
			return content.Load<SpriteFont>(font);
		}

	}
}
