using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components;
using Game1.Code.Components.AIControllers;
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
	class ScreenShop : Screen
	{
		private MapManager manageMap;
		private CameraManager cameraManager;
		private Entities _entities;

		public ScreenShop(ScreenManager screenManager, ContentManager content) : base(screenManager)
		{
			cameraManager = new CameraManager(screenManager._screenSize);
			manageMap = new MapManager("Shop", cameraManager);
			_entities = new Entities();
			Scene = Scene.Shop;
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
		}

		public override void LoadContent(ContentManager content)
		{


			manageMap.LoadContent(content);

			//test
			PlayerManager.reloadPlayer(_entities, cameraManager, manageMap);

			if(ScreenManager.lastScene == Scene.Town)
			{
				PlayerManager.moveTo(new Vector2(152, 190));
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
