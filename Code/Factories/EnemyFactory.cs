using Game1.Code.Components;
using Game1.Code.Components.AIControllers;
using Game1.Code.Components.Animations;
using Game1.Code.Components.Pickups;
using Game1.Code.Managers;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Factories
{
	static class EnemyFactory
	{

		public static BaseObject MakeKnight(Vector2 position, ContentManager content, MapManager mapManager, CameraManager cameraManager, Entities entities)
		{

			BaseObject coin = new BaseObject();
			coin.AddComponent(new Sprite(ManagerContent.LoadTexture("Items/coin_gold"), 16, 16, new Vector2(200, 100)));
			coin.AddComponent(new Camera(cameraManager));
			coin.AddComponent(new LoopAnimation(32, 32, 8, 100));
			coin.AddComponent(new PickUp());

			var Knight = new BaseObject();
			Knight.Id = FunctionManager.getID();
			Knight.AddComponent(new Sprite(content.Load<Texture2D>("Knight"), 16, 16, position));
			Knight.AddComponent(new AIMovement(300));
			Knight.AddComponent(new AnimationNPC(16, 16));
			Knight.AddComponent(new Collision(mapManager, entities));
			Knight.AddComponent(new Camera(cameraManager));
			Knight.AddComponent(new Damage(entities));
			Knight.AddComponent(new Stats(15));
			Knight.AddComponent(new Drop(entities, coin));
			Knight.Hostile = true;

			return Knight;
		}

	}
}
