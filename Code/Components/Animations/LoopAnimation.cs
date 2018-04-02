using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Components.Animations
{
	class LoopAnimation : Animation
	{
		private int Frames;
		private int LoopSpeed;

		public override Rectangle TextureRectangle
			{ get { return new Rectangle(aWidth * animationIndex, 0, aWidth, aHeight); } }

		public LoopAnimation(int width, int height, int frames = 0, int loopSpeed = 100) : base(width, height)
		{
			currState = State.Play;
			LoopSpeed = loopSpeed;
			Frames = frames;
		}

		public override void Update(double gameTime)
		{
			switch (currState)
			{
				case State.Play:
					aniCounter += gameTime;
					if (aniCounter > LoopSpeed)
					{
						animationIndex++;
						animationIndex %= Frames;
						aniCounter = 0;
					}
					break;
			}
		}

		public override void PlayAnimation()
		{
			
		}
	}
}
