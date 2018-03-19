using Game1.Code.Components;
using Game1.Code.Components.AIControllers;
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

		public static BaseObject MakeKnight(ContentManager content, MapManager mapManager, CameraManager cameraManager, Entities entities)
		{
			var Knight = new BaseObject();
			Knight.Id = FunctionManager.getID();
			Knight.AddComponent(new Sprite(content.Load<Texture2D>("Knight"), 16, 16, new Vector2(100, 150)));
			Knight.AddComponent(new AIMovement(300));
			Knight.AddComponent(new AnimationNPC(16, 16));
			Knight.AddComponent(new Collision(mapManager));
			Knight.AddComponent(new Camera(cameraManager));
			Knight.AddComponent(new Damage(entities));
			Knight.AddComponent(new Stats(15));
			Knight.Hostile = true;

			return Knight;
		}

	}
}
