using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class GUI : Component
	{
		private Texture2D _container;
		private Texture2D _bar;

		public override ComponentType ComponentType
		{
			get { return ComponentType.GUI; }
		}

		public void LoadContent(ContentManager content)
		{
			_container = content.Load<Texture2D>("container_GUI");
			_bar = content.Load<Texture2D>("bar_GUI");

		}

		public override void Draw(SpriteBatch spritebatch)
		{

			// Draw HP bar
			var stats = GetComponent<Stats>(ComponentType.Stats);
			if (stats == null) return;

			FunctionManager.DrawGUI(_bar, new Rectangle(4, 4, 102, 18), null, Color.Black, spritebatch);
			FunctionManager.DrawGUI(_bar, new Rectangle(5,5, stats.health, 16), null, Color.LimeGreen, spritebatch); 
			
		}

		public override void Update(double gameTime)
		{

		}
	}
}
