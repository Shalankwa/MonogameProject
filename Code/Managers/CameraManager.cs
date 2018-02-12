using Game1.Code.EventHandlers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	class CameraManager
	{
		private Vector2 _position;
		private Direction _moveDirection;
		public Vector2 _moveToPosition { get; private set; }
		private float _cameraMoveSpeed;
		public Point _screenSize { get; private set; }
		public bool gameLocked { get { return (int)_position.X != (int)_moveToPosition.X || (int)_position.Y != (int)_moveToPosition.Y; } }
		private static event EventHandler<CameraTransitionEvent> _FireCameraTransition;

		//Static access to subscribe to event
		public static event EventHandler<CameraTransitionEvent> FireCameraTransition
		{
			add { _FireCameraTransition += value; }
			remove { _FireCameraTransition -= value; }
		}

		public CameraManager(Point screenSize)
		{
			_screenSize = screenSize;
			_cameraMoveSpeed = 6f;
			_position = new Vector2(0, 0);
		}

		public void Update(double gameTimee)
		{
			if (!gameLocked) return;

			if (_position.X < _moveToPosition.X)
				_position.X += _cameraMoveSpeed;
			if (_position.X > _moveToPosition.X)
				_position.X -= _cameraMoveSpeed;
			if (_position.Y < _moveToPosition.Y)
				_position.Y += _cameraMoveSpeed;
			if (_position.Y > _moveToPosition.Y)
				_position.Y -= _cameraMoveSpeed;

			if(FunctionManager.DistanceTo(_position, _moveToPosition) < 6)
			{
				_position = _moveToPosition;
			}
		}

		public void Move(Direction direction)
		{
			if (gameLocked) return;
			_moveDirection = direction;
			switch (direction)
			{
				case Direction.Left:
					_moveToPosition = new Vector2(_position.X - _screenSize.X, _position.Y);
					break;
				case Direction.Right:
					_moveToPosition = new Vector2(_position.X + _screenSize.X, _position.Y);
					break;
				case Direction.Up:
					_moveToPosition = new Vector2(_position.X, _position.Y - _screenSize.Y);
					break;
				case Direction.Down:
					_moveToPosition = new Vector2(_position.X, _position.Y + _screenSize.Y);
					break;
			}

			_FireCameraTransition(this, new CameraTransitionEvent(direction));
		}

		public bool InScreenCheck(Vector2 vector)
		{
			return ((vector.X > _position.X - 20 && vector.X < _position.X + _screenSize.X + 4)
				&& (vector.Y > _position.Y - 20 && vector.Y < _position.Y + _screenSize.Y + 4)); 
		}

		public Vector2 WorldToScreenPosition(Vector2 position)
		{
			return new Vector2(position.X - _position.X, position.Y - _position.Y);
		}

		public Direction GetDirectionOutOfScreen(Vector2 position)
		{

			if (position.X < _position.X)
				return Direction.Left;
			if (position.X > _position.X + _screenSize.X)
				return Direction.Right;
			if (position.Y < _position.Y)
				return Direction.Up;
			if (position.Y > _position.Y + _screenSize.Y)
				return Direction.Down;

			return Direction.NULL;
		}
	}
}
