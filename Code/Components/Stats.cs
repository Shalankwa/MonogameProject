using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class Stats : Component
	{

		public int health { get; set; }

		public Stats(int HP)
		{
			health = HP;
		}

		public override ComponentType ComponentType
		{
			get { return ComponentType.Stats; }
		}

		public override void Draw(SpriteBatch spritebatch)
		{
		}

		public override void Update(double gameTime)
		{
		}
	}
}
