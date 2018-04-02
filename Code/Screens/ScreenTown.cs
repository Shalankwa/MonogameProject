using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components;
using Game1.Code.Components.AIControllers;
using Game1.Code.Components.Animations;
using Game1.Code.Components.Interactions;
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
	class ScreenTown : Screen
	{
		private MapManager manageMap;
		private CameraManager cameraManager;
		private Entities _entities;
		public static bool hasDude = true;

		public ScreenTown(ScreenManager screenManager, ContentManager content) : base(screenManager)
		{
			cameraManager = new CameraManager(screenManager._screenSize);
			manageMap = new MapManager("Town", cameraManager);
			_entities = new Entities();
			Scene = Scene.Town;
		}

		public override void Initialize()
		{
			TileMapLoader.NewMapObject += addNewEntities;
		}

		public override void Uninitialize()
		{
			TileMapLoader.NewMapObject -= addNewEntities;
			manageMap.Uninitialize();
		}

		void addNewEntities(object sender, NewMapObjectEvent e)
		{
			BaseObject newObj = ObjectFactory.newObject(
				e.newObject, e.position, e.properties,
				ScreenManager, ScreenManager._content, manageMap,
				_entities, cameraManager);

			if (newObj != null)
				_entities.AddEntitie(newObj);

			//Debug.Print(e.newObject.ToString());

		}

		public override void LoadContent(ContentManager content)
		{

			manageMap.LoadContent(content);

			//test
			PlayerManager.reloadPlayer(_entities, cameraManager, manageMap);

			if(ScreenManager.lastScene == Scene.Dungeon)
			{
				PlayerManager.moveTo(new Vector2(232, 20));
			}
			else if(ScreenManager.lastScene == Scene.Shop)
			{
				PlayerManager.moveTo(new Vector2(57, 68));
			}
			else if (ScreenManager.lastScene == Scene.Home)
			{
				PlayerManager.moveTo(new Vector2(184, 120));
			}

			if (hasDude)
			{
				var dude = new BaseObject();
				dude.Id = FunctionManager.getID();
				dude.AddComponent(new Sprite(ManagerContent.LoadTexture("NPCS/dude"), 16, 26, new Vector2(232, 14)));
				dude.AddComponent(new ToggleAnimation(36, 52, 0));
				dude.AddComponent(new Collision(manageMap, _entities, true));
				dude.AddComponent(new Camera(cameraManager));
				dude.AddComponent(new RequirementInteraction());

				_entities.AddEntitie(dude);

			}

			_entities.AddEntitie(PlayerManager.player);

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
