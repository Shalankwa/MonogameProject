using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Components.Animations
{
	class ToggleAnimation : Animation
	{
		private int Frames;
		private bool increaseIndex;

		public override Rectangle TextureRectangle
		{ get { return new Rectangle(aWidth * animationIndex, 0, aWidth, aHeight); } }

		public ToggleAnimation(int width, int height, int frames) : base(width, height)
		{
			currState = State.Paused;
			increaseIndex = true;
			Frames = frames;
		}

		public override void Update(double gameTime)
		{
			switch (currState)
			{
				case State.Play:
					Animate(gameTime);
					break;
			}
		}

		public override void PlayAnimation()
		{
			currState = State.Play;
		}

		public override void Stand()
		{
			
		}

		private void Animate(double gameTime)
		{
			aniCounter += gameTime;
			if (aniCounter > 50)
			{
				animationIndex += (increaseIndex) ? 1 : -1;
				aniCounter = 0;

				if (animationIndex >= Frames - 1 || animationIndex <= 0)
				{
					currState = State.Paused;
					increaseIndex = !increaseIndex;
				}
			}
		}
	}
}
