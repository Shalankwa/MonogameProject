using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components;
using Game1.Code.Components.AIControllers;
using Game1.Code.Componets.Items;
using Game1.Code.EventHandlers;
using Game1.Code.Factories;
using Game1.Code.Loader;
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
		private BaseObject player;

		public ScreenDungeon(ScreenManager screenManager, ContentManager content) : base(screenManager)
		{
			cameraManager = new CameraManager(screenManager._screenSize);
			manageMap = new MapManager("Town", cameraManager);
			_entities = new Entities();
		}

		public override void Initialize()
		{
			TileMapLoader.NewMapObject += addNewEntities;
		}

		public override void Uninitialize()
		{
			TileMapLoader.NewMapObject -= addNewEntities;
		}

		void addNewEntities(object sender, NewMapObjectEvent e)
		{
			BaseObject newObj = ObjectFactory.newObject(e.newObject, e.position, e.properties, player, ScreenManager, ScreenManager._content);
			
			if(newObj != null)
				_entities.AddEntitie(newObj);
		}

		public override void LoadContent(ContentManager content)
		{

			player = new BaseObject();
			player.Id = FunctionManager.getID();
			player.AddComponent(new Sprite(content.Load<Texture2D>("link_full"), 16, 16, new Vector2(220, 150)));
			player.AddComponent(new PlayerInput());
			player.AddComponent(new Animation(16, 16));
			player.AddComponent(new Collision(manageMap));
			player.AddComponent(new Camera(cameraManager));
			player.AddComponent(new Damage(_entities));
			player.AddComponent(new GUI());
			player.AddComponent(new Stats(100));
			player.AddComponent(new Inventory(content, cameraManager));
			var inv = player.GetComponent<Inventory>(ComponentType.Inventory);
			inv.addItem(new Sword(player, _entities));
			inv.EquiptItemToSlot(1, ItemSlot.slot1);
			player.GetComponent<GUI>(ComponentType.GUI).LoadContent(content);

			var _FirstNPC = new BaseObject();
			_FirstNPC.Id = FunctionManager.getID();
			_FirstNPC.AddComponent(new Sprite(content.Load<Texture2D>("F_04"), 16, 16, new Vector2(150, 150)));
			_FirstNPC.AddComponent(new AIMovement(300));
			_FirstNPC.AddComponent(new AnimationNPC(16, 16));
			_FirstNPC.AddComponent(new Collision(manageMap));
			_FirstNPC.AddComponent(new Camera(cameraManager));

			var _FirstKnight = EnemyFactory.MakeKnight(content, manageMap, cameraManager, _entities);
			//_FirstKnight.Id = FunctionManager.getID();
			//_FirstKnight.AddComponent(new Sprite(content.Load<Texture2D>("Knight"), 16, 16, new Vector2(100, 150)));
			//_FirstKnight.AddComponent(new AIMovement(300));
			//_FirstKnight.AddComponent(new AnimationNPC(16, 16));
			//_FirstKnight.AddComponent(new Collision(manageMap));
			//_FirstKnight.AddComponent(new Camera(cameraManager));
			//_FirstKnight.AddComponent(new Damage(_entities));
			//_FirstKnight.AddComponent(new Stats(15));

			manageMap.LoadContent(content);

			//test
			_entities.AddEntitie(player);
			_entities.AddEntitie(_FirstNPC);
			_entities.AddEntitie(_FirstKnight);
		}

		public override void Update(double gameTime)
		{
			cameraManager.Update(gameTime);
			if (cameraManager.gameLocked) return;

			manageMap.Update(gameTime);


			_entities.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			manageMap.Draw(spriteBatch);
			_entities.Draw(spriteBatch);
		}
		
	}
}
