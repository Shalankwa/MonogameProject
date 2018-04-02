using Game1.Code.Components;
using Game1.Code.Components.Interactions;
using Game1.Code.Componets.Items;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	static class PlayerManager
	{

		

		public static BaseObject player { get; set; }
		public static List<Vector2> MapTilesExplored { get; set; }

		public static void moveTo(Vector2 position)
		{
			player.GetComponent<Sprite>(ComponentType.Sprite).Teleport(position);
		}

		public static void makePlayer(Entities entities, CameraManager camera, MapManager mapManager)
		{

			var content = ManagerContent.content;

			player = new BaseObject();
			player.Id = FunctionManager.getID();
			player.AddComponent(new Sprite(content.Load<Texture2D>("link_full"), 16, 16, new Vector2(220, 150)));
			player.AddComponent(new PlayerInput());
			player.AddComponent(new Animation(16, 16));
			player.AddComponent(new Collision(mapManager, entities));
			player.AddComponent(new Camera(camera));
			player.AddComponent(new Damage(entities));
			player.AddComponent(new GUI());
			player.AddComponent(new Stats(100));
			player.AddComponent(new Inventory(content, camera));
			player.AddComponent(new PlayerInteract(entities));

			player.GetComponent<GUI>(ComponentType.GUI).LoadContent(content);

		}

		public static void reloadPlayer(Entities entities, CameraManager camera, MapManager mapManager)
		{
			MapTilesExplored = new List<Vector2>();

			if(player == null)
			{
				makePlayer(entities, camera, mapManager);
			} else
			{
				// Assign new collision componet for new map
				player.GetComponent<Collision>(ComponentType.Collision).Reload(mapManager, entities);

				// Reload camera
				player.GetComponent<Camera>(ComponentType.Camera).Reload(camera);

				// Reload damage componet
				player.GetComponent<Damage>(ComponentType.Damage).Reload(entities);

				// Reload Interaction Ent list
				player.GetComponent<Interaction>(ComponentType.Interaction).Reload(entities);

				// Reload inventory
				player.GetComponent<Inventory>(ComponentType.Inventory).Reload(camera, entities);

			}
		}

		public static void UpdateMap(int x, int y)
		{
			if (!Explored(x, y))
			{
				MapTilesExplored.Add(new Vector2(x, y));
			}
		}

		public static bool Explored(int x, int y)
		{
			return MapTilesExplored.Any(t => t.X == x && t.Y == y);
		}

		public static double DistanceToPlayer(Vector2 pos1)
		{
			Vector2 pos2 = player.GetComponent<Sprite>(ComponentType.Sprite).Position;

			double x = Math.Pow(pos1.X - pos2.X, 2);
			double y = Math.Pow(pos1.Y - pos2.Y, 2);
			return Math.Sqrt(x + y);
		}

		public static Direction DirectionToPlayer(Vector2 pos)
		{
			Vector2 playerPos = player.GetComponent<Sprite>(ComponentType.Sprite).Position;

			double xdiffence = playerPos.X - pos.X;
			double ydiffence = playerPos.Y - pos.Y;

			Direction dir;

			if (Math.Abs(xdiffence) > Math.Abs(ydiffence))
			{
				dir = (xdiffence >= 0) ? Direction.Right : Direction.Left;
			}
			else
			{
				dir = (ydiffence >= 0) ? Direction.Down : Direction.Up;
			}

			return dir;
			
		}

		// Depricated
		public static void Update(double gameTime)
		{
			player.Update(gameTime);
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			player.Draw(spriteBatch);
		}
	}
}
