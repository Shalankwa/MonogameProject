using Game1.Code.Managers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Screens
{
	public abstract class Screen
	{
		public Scene Scene;
		protected ScreenManager ScreenManager;

		public Screen(ScreenManager screenManager)
		{
			ScreenManager = screenManager;
		}

		public virtual void Initialize() { }
		public virtual void Uninitialize() { }
		public abstract void LoadContent(ContentManager content);
		public abstract void Update(double gameTime);
		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
