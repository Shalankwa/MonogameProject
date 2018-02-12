using Game1.Code.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	public class ScreenManager
	{
		private Screen _currScreen;
		private Screen _lastScreen;
		private ContentManager _content;

		public ScreenManager(ContentManager content)
		{
			_content = content;
		}

		public void loadNewScreen(Screen screen)
		{
			_lastScreen = _currScreen;
			if (_lastScreen != null)
				_lastScreen.Uninitialize();
			_lastScreen = null;

			_currScreen = screen;
			_currScreen.Initialize();
			_currScreen.LoadContent(_content);
		}

		public void GoBackOneScreen()
		{
			if (_lastScreen == null) return;

			_currScreen.Uninitialize();

			var temp = _currScreen;
			_currScreen = _lastScreen;
			_lastScreen = temp;
		}

		public void Update(double gameTime)
		{
			_currScreen.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			_currScreen.Draw(spriteBatch);
		}
	}
}
