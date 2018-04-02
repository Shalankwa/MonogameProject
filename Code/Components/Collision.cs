using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Managers;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class Collision : Component
	{
		public bool imPassable { get; set; }

		private MapManager _mapManager;
		private Entities _entities;

		public Collision(MapManager managerMap, Entities entities, bool impas = false)
		{
			_entities = entities;
			_mapManager = managerMap;
			imPassable = impas;

		}

		public void Reload(MapManager managerMap, Entities entities)
		{
			_mapManager = managerMap;
			_entities = entities;
		}

		public override ComponentType ComponentType {

			get { return ComponentType.Collision; }

		}

		public bool CheckCollision(Rectangle rec)
		{
			BaseObject hitObj;
			_entities.CheckCollision(rec, out hitObj, base.GetOwnerId());

			bool cantPass = false;

			if (hitObj != null)
			{
				var col = hitObj.GetComponent<Collision>(ComponentType.Collision);
				if (col != null)
					cantPass = col.imPassable;
			}


			return _mapManager.CheckCollision(rec) || cantPass;
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			
		}

		public override void Update(double gameTime)
		{
			
		}
	}
}
