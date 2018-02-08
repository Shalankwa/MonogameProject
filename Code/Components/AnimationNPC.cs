using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class AnimationNPC : Animation
	{
		private int _frames = 3;
		
		public AnimationNPC(int width, int height) : base(width, height)
		{

		}

		protected override void ChangeState()
		{
			switch (currDirection)
			{
				case Direction.Up:
					TextureRectangle = new Rectangle(aWidth * 2, aHeight * animationIndex + (1 * animationIndex), aWidth, aHeight);
					break;
				case Direction.Down:
					TextureRectangle = new Rectangle(0, aHeight * animationIndex + (1 * animationIndex), aWidth, aHeight);
					break;
				case Direction.Left:
					TextureRectangle = new Rectangle(aWidth * 3, aHeight * animationIndex + (1 * animationIndex), aWidth, aHeight);
					break;
				case Direction.Right:
					TextureRectangle = new Rectangle(aWidth, aHeight * animationIndex + (1 * animationIndex), aWidth, aHeight);
					break;
			}

			animationIndex = (animationIndex >= _frames - 1) ? 0 : animationIndex + 1;
			currState = State.Standing;
		}

	}
}
