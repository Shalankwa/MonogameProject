using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components;
using Game1.Code.Components.AIControllers;
using Game1.Code.Managers;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Screens
{
	class ScreenDungeon : Screen
	{

		private MapManager manageMap;
		private CameraManager cameraManager;
		private Entities _entities;

		public ScreenDungeon(ScreenManager screenManager, Point screenSize, ContentManager content) : base(screenManager)
		{
			cameraManager = new CameraManager(screenSize);
			manageMap = new MapManager("Map2", cameraManager);
			_entities = new Entities();
		}

		public override void Initialize()
		{
		}

		public override void LoadContent(ContentManager content)
		{
			
			manageMap.LoadContent(content);

			var player = new BaseObject();
			player.AddComponent(new Sprite(content.Load<Texture2D>("link_full"), 16, 16, new Vector2(220, 150)));
			player.AddComponent(new PlayerInput());
			player.AddComponent(new Animation(16, 16));
			player.AddComponent(new Collision(manageMap));
			player.AddComponent(new Camera(cameraManager));

			var _FirstNPC = new BaseObject();
			_FirstNPC.AddComponent(new Sprite(content.Load<Texture2D>("F_04"), 16, 16, new Vector2(150, 150)));
			_FirstNPC.AddComponent(new AIMovement(300));
			_FirstNPC.AddComponent(new AnimationNPC(16, 16));
			_FirstNPC.AddComponent(new Collision(manageMap));
			_FirstNPC.AddComponent(new Camera(cameraManager));

			var _FirstKnight = new BaseObject();
			_FirstKnight.AddComponent(new Sprite(content.Load<Texture2D>("Knight"), 16, 16, new Vector2(100, 150)));
			_FirstKnight.AddComponent(new AIMovement(300));
			_FirstKnight.AddComponent(new AnimationNPC(16, 16));
			_FirstKnight.AddComponent(new Collision(manageMap));
			_FirstKnight.AddComponent(new Camera(cameraManager));

			_entities.AddEntitie(player);
			_entities.AddEntitie(_FirstNPC);
			_entities.AddEntitie(_FirstKnight);
		}

		public override void Update(double gameTime)
		{
			cameraManager.Update(gameTime);
			if (cameraManager.gameLocked) return;

			_entities.Update(gameTime);
			manageMap.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			manageMap.Draw(spriteBatch);
			_entities.Draw(spriteBatch);
		}
		
	}
}
