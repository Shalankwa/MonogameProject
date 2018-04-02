using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
    class Animation : Component
    {
        public override ComponentType ComponentType
        {
            get { return ComponentType.Animation; }
        }

        protected int aWidth;
        protected int aHeight;
        public virtual Rectangle TextureRectangle { get; protected set; } 
        public Direction currDirection { get; set; }
        public State currState;
        public double aniCounter;
        public int animationIndex;

        public Animation(int width, int height)
        {
            aWidth = width; 
            aHeight = height;
            aniCounter = 0;
            animationIndex = 0;
            currState = State.Standing;
			currDirection = Direction.Down;
			TextureRectangle = new Rectangle(0, 0, aWidth, aHeight);

		}

        public override void Update(double gameTime)
        {
			switch (currState)
			{
				case State.Walking:
					aniCounter += gameTime;
					if(aniCounter > 200)
					{
						ChangeState();
						aniCounter = 0;
					}
					break;
				case State.Standing:
					aniCounter += gameTime;
					break;
			}
        }

		public void ResetCounter(State state, Direction direction)
		{
			if(currDirection != direction)
			{
				aniCounter = 1000;
				animationIndex = 0;
			}

			currState = state;
			currDirection = direction;
		}

		protected virtual void ChangeState()
		{
			switch (currDirection)
			{
				case Direction.Up:
					TextureRectangle = new Rectangle(aWidth * animationIndex, aHeight, aWidth, aHeight);
					break;
				case Direction.Down:
					TextureRectangle = new Rectangle(aWidth * animationIndex, 0, aWidth, aHeight);
					break;
				case Direction.Left:
					TextureRectangle = new Rectangle(aWidth * animationIndex, aHeight * 2, aWidth, aHeight);
					break;
				case Direction.Right:
					TextureRectangle = new Rectangle(aWidth * animationIndex, aHeight * 3, aWidth, aHeight);
					break;
			}

			animationIndex = (animationIndex == 0) ? 1 : 0;
			currState = State.Standing;
		}

		public virtual void Stand()
		{
			currState = State.Standing;
			animationIndex = 0;
			ChangeState();
		}

		public override void Draw(SpriteBatch spritebatch)
		{

		}

		public virtual void PlayAnimation() { }

	}
}
