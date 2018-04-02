using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components.AIControllers
{
	class AIMovement : Component
	{

		private Direction _currDirection;
		private int _frequency;
		private double _counter;
		private double _wait;

		public AIMovement(int frequency)
		{
			_frequency = frequency;
			_wait = 0;
			ChangeDirection();
		}

		public override ComponentType ComponentType
		{
			get { return ComponentType.AIMovement; }
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			

		}

		public override void Update(double gameTime)
		{

			//Get sprite component from BaseObject class
			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			if (sprite == null) return;

			var collision = GetComponent<Collision>(ComponentType.Collision);
			var camera = GetComponent<Camera>(ComponentType.Camera);
			if (camera == null) return;

			if (!camera.InScreen(sprite.Position)) return;

			var x = 0f;
			var y = 0f;

			if (_wait > 0)
			{
				_wait -= gameTime;
				_wait = (_wait < 0) ? 0 : _wait;
				sprite.Move(x, y);
				return;
			}

			toWait();

			_counter += gameTime;

			if(_counter > _frequency)
			{
				ChangeDirection();
			}

			switch (_currDirection)
			{
				case Direction.Up:
					y = -0.5f;
					break;
				case Direction.Down:
					y = 0.5f;
					break;
				case Direction.Left:
					x = -0.5f;
					break;
				case Direction.Right:
					x = 0.5f;
					break;
			}

			if (collision.CheckCollision(new Rectangle((int)(sprite.Position.X + x), (int)(sprite.Position.Y + y), sprite.width, sprite.height)))
			{
				ChangeDirection();
				return;
			}

			sprite.Move(x, y);

		}

		private void ChangeDirection()
		{
			_counter = 0;
			_currDirection = (Direction)FunctionManager.Random(0, 3);
		}

		private void toWait()
		{
			_wait = (FunctionManager.Random(0, 100) > 98) ? 1000 : 0;
		}
	}
}
