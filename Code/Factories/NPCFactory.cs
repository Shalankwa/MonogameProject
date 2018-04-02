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
using Game1.Code.Managers;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Factories
{
	static class NPCFactory
	{
		public static BaseObject makeNPC(Vector2 position, Dictionary<string, string> properties, ContentManager content, MapManager map, CameraManager camera, Entities entities)
		{
			NPC npc = FunctionManager.ParseEnum<NPC>(properties["NPC"]);
			BaseObject newNPC = null;

			switch (npc)
			{
				case NPC.Sally:
					newNPC = makeSally(position, properties, content, map, camera, entities);
					break;
				case NPC.Guy:
					newNPC = makeGuy(position, properties, content, map, camera, entities);
					break;
				case NPC.Joe:
					newNPC = makeJoe(position, properties, content, map, camera, entities);
					break;
				case NPC.Andrew:
					newNPC = makeAndrew(position, properties, content, map, camera, entities);
					break;
				case NPC.Eric:
					newNPC = makeEric(position, properties, content, map, camera, entities);
					break;
				case NPC.Andris:
					newNPC = makeAndris(position, properties, content, map, camera, entities);
					break;
			}

			return newNPC;
		}

		private static BaseObject makeAndris(Vector2 position, Dictionary<string, string> properties, ContentManager content, MapManager map, CameraManager camera, Entities entities)
		{
			var Andris = new BaseObject();
			Andris.Id = FunctionManager.getID();
			Andris.AddComponent(new Sprite(content.Load<Texture2D>("NPCS/Andris"), 16, 16, position));
			Andris.AddComponent(new AIMovement(300));
			Andris.AddComponent(new AnimationNPC(16, 16));
			Andris.AddComponent(new Collision(map, entities));
			Andris.AddComponent(new Camera(camera));

			return Andris;
		}

		private static BaseObject makeEric(Vector2 position, Dictionary<string, string> properties, ContentManager content, MapManager map, CameraManager camera, Entities entities)
		{
			var Eric = new BaseObject();
			Eric.Id = FunctionManager.getID();
			Eric.AddComponent(new Sprite(content.Load<Texture2D>("NPCS/Eric"), 16, 16, position));
			Eric.AddComponent(new AIMovement(300));
			Eric.AddComponent(new AnimationNPC(16, 16));
			Eric.AddComponent(new Collision(map, entities));
			Eric.AddComponent(new Camera(camera));
			Eric.AddComponent(new DialogInteraction("I like walking! It's comfy and easy to do!"));

			return Eric;
		}

		private static BaseObject makeAndrew(Vector2 position, Dictionary<string, string> properties, ContentManager content, MapManager map, CameraManager camera, Entities entities)
		{
			var Andrew = new BaseObject();
			Andrew.Id = FunctionManager.getID();
			Andrew.AddComponent(new Sprite(content.Load<Texture2D>("NPCS/Andrew"), 16, 16, position));
			Andrew.AddComponent(new AnimationNPC(16, 16));
			Andrew.AddComponent(new Collision(map, entities, true));
			Andrew.AddComponent(new Camera(camera));
			Andrew.AddComponent(new DialogInteraction("This is my home."));

			return Andrew;
		}

		private static BaseObject makeJoe(Vector2 position, Dictionary<string, string> properties, ContentManager content, MapManager map, CameraManager camera, Entities entities)
		{
			var Joe = new BaseObject();
			Joe.Id = FunctionManager.getID();
			Joe.AddComponent(new Sprite(content.Load<Texture2D>("NPCS/Joe"), 16, 16, position));
			Joe.AddComponent(new AnimationNPC(16, 16));
			Joe.AddComponent(new Collision(map, entities));
			Joe.AddComponent(new Camera(camera));
			Joe.AddComponent(new Inventory(content, camera));
			Joe.AddComponent(new DialogInteraction("NOOO!! STAY AWAY!... im shy."));

			return Joe;

		}

		private static BaseObject makeSally(Vector2 position, Dictionary<string, string> properties, ContentManager content, MapManager map, CameraManager camera, Entities entities)
		{
			var Sally = new BaseObject();
			Sally.Id = FunctionManager.getID();
			Sally.AddComponent(new Sprite(content.Load<Texture2D>("F_04"), 16, 16, position));
			Sally.AddComponent(new AIMovement(300));
			Sally.AddComponent(new AnimationNPC(16, 16));
			Sally.AddComponent(new Collision(map, entities));
			Sally.AddComponent(new Camera(camera));
			Sally.AddComponent(new DialogInteraction("That man over there is scary..."));

			return Sally;

		}

		private static BaseObject makeGuy(Vector2 position, Dictionary<string, string> properties, ContentManager content, MapManager map, CameraManager camera, Entities entities)
		{

			var Trader = new BaseObject();
			Trader.Id = FunctionManager.getID();
			Trader.AddComponent(new Sprite(content.Load<Texture2D>("NPCS/Guy"), 16, 16, position));
			Trader.AddComponent(new AnimationNPC(16, 16));
			Trader.AddComponent(new Collision(map, entities, true));
			Trader.AddComponent(new Camera(camera));
			Trader.AddComponent(new Inventory(content, camera));
			var inv = Trader.GetComponent<Inventory>(ComponentType.Inventory);
			inv.addItem(new Sword(Trader, entities));
			Trader.AddComponent(new TradeInteraction());
			inv._currency += 500;

			return Trader;

		}
	}
}
