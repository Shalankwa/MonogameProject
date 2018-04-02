using Game1.Code.Components;
using Game1.Code.Components.ActivateEvents;
using Game1.Code.Components.AIControllers;
using Game1.Code.Components.Animations;
using Game1.Code.Components.Interactions;
using Game1.Code.Managers;
using Game1.Code.Map;
using Game1.Code.MapObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Factories
{
	static class ObjectFactory
	{

		public static BaseObject newObject(Objects obj, Vector2 position, Dictionary<string, string> properties, 
			ScreenManager screenManager, ContentManager content, MapManager map, Entities entities, CameraManager camera)
		{
		
			BaseObject newObject = null;

			position += camera._moveToPosition;

			switch (obj)
			{
				case Objects.TriggerScene:
					newObject = newSceneTrigger(obj, position, properties, PlayerManager.player, screenManager, content);
					break;
				case Objects.Enemy:
					newObject = EnemyFactory.MakeKnight(position, content, map, camera, entities);
					break;
				case Objects.Object:
					newObject = newSceneObject(obj, position, properties, map, camera, entities);
					break;
				case Objects.NPC:
					newObject = NPCFactory.makeNPC(position, properties, content, map, camera, entities);
					break;
			}

			return newObject;
		}

		private static BaseObject newSceneObject(Objects obj, Vector2 position, Dictionary<string, string> properties, MapManager map, CameraManager camera, Entities entities)
		{
			Objects type = FunctionManager.ParseEnum<Objects>(properties["Type"]);
			BaseObject newObj = null;

			switch (type)
			{
				case Objects.Door:
					newObj = makeDoor(position, properties, map, camera, entities);
					break;
				case Objects.Switch:
					newObj = makeSwitch(position, properties, map, camera, entities);
					break;
				case Objects.Chest:
					newObj = makeChest(position, properties, map, camera, entities);
					break;
				case Objects.Sign:
					newObj = makeSign(position, properties, map, camera, entities);
					break;
			}

			return newObj;
		}

		private static BaseObject makeDoor(Vector2 position, Dictionary<string, string> properties, MapManager map, CameraManager camera, Entities entities)
		{
			int Channel = int.Parse(properties["Channel"]);

			BaseObject door = new BaseObject();
			door.Id = FunctionManager.getID();
			door.AddComponent(new Sprite(ManagerContent.LoadTexture("Objects/DoorSpriteSheet"), 48, 32, position ));
			door.AddComponent(new Camera(camera));
			door.AddComponent(new ToggleAnimation(48, 32, 5));
			door.AddComponent(new Collision(map, entities, true));
			door.AddComponent(new GateOpen(Channel));

			return door;
		}

		private static BaseObject makeSign(Vector2 position, Dictionary<string, string> properties, MapManager map, CameraManager camera, Entities entities)
		{
			string text = properties["Text"];

			BaseObject sign = new BaseObject();
			sign.Id = FunctionManager.getID();
			sign.AddComponent(new Sprite(ManagerContent.LoadTexture("Objects/Sign"), 16, 16, position));
			sign.AddComponent(new Collision(map, entities, true));
			sign.AddComponent(new Camera(camera));
			sign.AddComponent(new DialogInteraction(text));

			return sign;

		}

		private static BaseObject makeChest(Vector2 position, Dictionary<string, string> properties, MapManager map, CameraManager camera, Entities entities)
		{

			BaseObject chest = new BaseObject();
			chest.Id = FunctionManager.getID();
			chest.AddComponent(new Sprite(ManagerContent.LoadTexture("Objects/Chest"), 16, 16, position));
			chest.AddComponent(new Camera(camera));
			chest.AddComponent(new Collision(map, entities, true));
			chest.AddComponent(new ToggleAnimation(16, 16, 4));
			chest.AddComponent(new OpenInteraction());

			return chest;
		}

		private static BaseObject makeSwitch(Vector2 position, Dictionary<string, string> properties, MapManager map, CameraManager camera, Entities entities)
		{
			int Channel = int.Parse(properties["Channel"]);

			BaseObject Switch = new BaseObject();
			Switch.Id = FunctionManager.getID();

			Switch.AddComponent(new Sprite(ManagerContent.LoadTexture("Objects/Switch"), 16, 16, position));
			Switch.AddComponent(new Camera(camera));
			Switch.AddComponent(new ToggleAnimation(16, 16, 7));
			Switch.AddComponent(new Collision(map, entities, true));
			Switch.AddComponent(new ActivateChannelInteraction(Channel));

			return Switch;
		}

		private static BaseObject newSceneTrigger(Objects obj, Vector2 position, Dictionary<string, string> properties, BaseObject player, ScreenManager screenManager,ContentManager content)
		{
			if (!properties.ContainsKey("width")) return null;

			SceneTrigger sceneTrigger;

			string sceneName = properties["TriggerScene"];
			Scene scene = FunctionManager.ParseEnum<Scene>(sceneName);

			sceneTrigger = new SceneTrigger(scene, position, int.Parse(properties["width"]), int.Parse(properties["height"]), screenManager, content);

			return sceneTrigger;
		}


	}
}
