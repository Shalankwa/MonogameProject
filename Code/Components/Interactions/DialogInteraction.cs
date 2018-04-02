using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.GUI_Elements;
using Game1.Code.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.Interactions
{
	class DialogInteraction : Interaction
	{
		private string _text;

		public DialogInteraction(string text)
		{
			_text = text;
		}

		public override void action()
		{
			var ani = GetComponent<Animation>(ComponentType.Animation);
			var sprite = GetComponent<Sprite>(ComponentType.Sprite);

			if (ani != null)
			{
				ani.currDirection = PlayerManager.DirectionToPlayer(sprite.Position);
				ani.Stand();
			}

			WindowManager.newWindow(new WindowMessage(_text));
		}

		public override void Draw(SpriteBatch spritebatch)
		{
		}

		public override void Update(double gameTime)
		{
		}
	}
}
