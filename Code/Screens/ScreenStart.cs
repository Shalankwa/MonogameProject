using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.EventHandlers;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Screens
{
	class ScreenStart : Screen
	{
		private Texture2D _backgroundImage;
		private Point _screenSize;

		private MapManager manageMap;
		private CameraManager cameraManager;

		ContentManager _content;

		public ScreenStart(ScreenManager screenManager, Point screenSize, ContentManager content) : base(screenManager)
		{
			_screenSize = screenSize;
			cameraManager = new CameraManager(screenSize);
			manageMap = new MapManager("MapTitleScreen", cameraManager);
		}

		public override void Initialize()
		{
			InputManager.FireNewInput += StartScreen_InputManage;
		}

		public override void Uninitialize()
		{
			manageMap.Uninitialize();
			InputManager.FireNewInput -= StartScreen_InputManage;
			_backgroundImage.Dispose();
		}

		private void StartScreen_InputManage(object sender, NewInputEventArgs e)
		{
			if(e.Input  == Input.Enter)
			{
				ScreenManager.loadNewScreen(new ScreenDungeon(ScreenManager, _screenSize, _content));
			}
		}

		public override void LoadContent(ContentManager content)
		{
			manageMap.LoadContent(content);
			_backgroundImage = content.Load<Texture2D>("HIRED-HERO");
		}

		public override void Update(double gameTime)
		{

			manageMap.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			FunctionManager.DrawAtLayer(_backgroundImage, new Rectangle(50, 80, 220, 50), null, 10, spriteBatch);
			manageMap.Draw(spriteBatch);

		}

	}
}
