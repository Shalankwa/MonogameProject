using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class Collision : Component
	{

		private MapManager _mapManager;

		public Collision(MapManager managerMap)
		{
			_mapManager = managerMap;
		}

		public override ComponentType ComponentType {

			get { return ComponentType.Collision; }

		}

		public bool CheckCollision(Rectangle rec)
		{
			return _mapManager.CheckCollision(rec);
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			
		}

		public override void Update(double gameTime)
		{
			
		}
	}
}
