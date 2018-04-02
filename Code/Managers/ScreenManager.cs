using Game1.Code.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	public class ScreenManager
	{
		public Scene lastScene;
		public Scene currScene;

		private Screen _currScreen;
		private Screen _lastScreen;
		private Texture2D _FadeTexture;
		private Phase _currentPhase;
		private double _counter;
		private float _darkness;
		private Screen _toLoadScreene;
		public ContentManager _content { get; private set; }
		public Point _screenSize { get; private set; }

		public ScreenManager(ContentManager content, Point screenSize)
		{
			_content = content;
			_screenSize = screenSize;
			_FadeTexture = ManagerContent.LoadTexture("GUI/MessageBox");
		}

		private enum Phase
		{
			FadeOut,
			FadeIn,
			Running
		}

		public void loadNewScreen(Screen screen, bool fade = true)
		{
			_toLoadScreene = screen;
			InputManager.PauseInput();

			if (!fade)
			{
				AfterFadeOut();
				_currentPhase = Phase.Running;
				Debug.Print("Skipping fade");
				return;
			}

			_currentPhase = Phase.FadeOut;
			_counter = 0;
			_darkness = 0;
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
			//_currScreen.Update(gameTime);
			switch (_currentPhase)
			{
				case Phase.FadeOut:
					FadeOut(gameTime);
					break;
				case Phase.FadeIn:
					FadeIn(gameTime);
					break;
				case Phase.Running:
					_currScreen.Update(gameTime);
					break;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{

			if (_currScreen != null)
			{
				_currScreen.Draw(spriteBatch);
			}

			if (_currentPhase == Phase.FadeIn || _currentPhase == Phase.FadeOut)
			{
				//spriteBatch.Draw(_FadeTexture, new Rectangle(0, 0, 160, 144), new Color(0, 0, 0, _darkness));
				FunctionManager.DrawAtLayer(_FadeTexture, new Rectangle(0, 0, _screenSize.X, _screenSize.Y), null, 10, Color.Black * _darkness, spriteBatch);

			}
		}

		private void FadeIn(double gameTime)
		{

			_counter += gameTime;
			if (_counter > 100)
			{
				_darkness -= 0.1f;
			}
			if (_darkness <= 0)
			{
				_currentPhase = Phase.Running;
			}
		}

		private void FadeOut(double gameTime)
		{

			_counter += gameTime;
			if (_counter > 100)
			{
				_darkness += 0.1f;
			}

			if (_darkness >= 1)
			{
				AfterFadeOut();
				_currentPhase = Phase.FadeIn;
				_counter = 0;
			}
		}

		private void AfterFadeOut()
		{

			lastScene = currScene;
			currScene = _toLoadScreene.Scene;

			_lastScreen = _currScreen;
			if (_lastScreen != null)
				_lastScreen.Uninitialize();

			_currScreen = _toLoadScreene;
			_currScreen.Initialize();
			_currScreen.LoadContent(_content);

			InputManager.UnPauseInput();
			InputManager.Cooldown(250);

		}


		public Screen makeScene(Scene scene)
		{
			Screen screen = null;

			switch (scene)
			{
				case Scene.Town:
					screen = new ScreenTown(this, _content);
					break;
				case Scene.Dungeon:
					screen = new ScreenDungeon(this, _content);
					break;
				case Scene.Shop:
					screen = new ScreenShop(this, _content);
					break;
				case Scene.Home:
					screen = new ScreenHome(this, _content);
					break;
			}

			return screen;

		}

	}
}
