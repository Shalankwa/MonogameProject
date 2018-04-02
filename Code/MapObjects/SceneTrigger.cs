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

		public SceneTrigger(Scene scene, Vector2 pos, int width, int height, ScreenManager screenManager, ContentManager content)
		{
			_scene = scene;
			_content = content;
			_position = pos;
			triggerBox = new Rectangle((int)_position.X, (int)_position.Y, width, height);
			_screenManager = screenManager;

		}

		public override void Update(double gameTime)
		{
			var sprite = PlayerManager.player.GetComponent<Sprite>(ComponentType.Sprite);
			if (sprite == null) return;

			if (sprite.Rectangle.Intersects(triggerBox))
			{
				_screenManager.loadNewScreen(_screenManager.makeScene(_scene));
			}
		}

	}
}
