using Game1.Code.Components;
using Game1.Code.Managers;
using Game1.Code.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.MapObjects
{
	class SceneTrigger : BaseObject
	{
		private ScreenManager _screenManager;
		private ContentManager _content;
		private Scene _scene;
		private Vector2 _position;
		public Rectangle triggerBox { get; private set; }
		private BaseObject _player;

		public SceneTrigger(string scene, Vector2 pos, int width, int height, BaseObject player, ScreenManager screenManager, ContentManager content)
		{
			_content = content;
			_scene = Scene.Town;
			_position = pos;
			_player = player;
			triggerBox = new Rectangle((int)_position.X, (int)_position.Y, width, height);
			_screenManager = screenManager;
		}

		public override void Update(double gameTime)
		{
			var sprite = _player.GetComponent<Sprite>(ComponentType.Sprite);
			if (sprite == null) return;

			if (sprite.Rectangle.Intersects(triggerBox))
			{
				_screenManager.loadNewScreen(new ScreenStart(_screenManager, _content));
			}
		}

	}
}
