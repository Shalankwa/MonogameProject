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
	class Camera : Component
	{
		private CameraManager _cameraManager;

		public override ComponentType ComponentType
		{
			get { return ComponentType.Camera; }
		}

		public Camera(CameraManager camera)
		{
			_cameraManager = camera;
		}

		public bool GetPosition(Vector2 position, out Vector2 newPosition)
		{
			newPosition = _cameraManager.WorldToScreenPosition(position);
			return _cameraManager.InScreenCheck(position);
		}

		public void MoveCamera(Direction direction)
		{
			_cameraManager.Move(direction);
		}

		public Direction GetDirectionOutOfScreen(Vector2 position)
		{
			return _cameraManager.GetDirectionOutOfScreen(position);
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			
		}

		public override void Update(double gameTime)
		{
			
		}
	}
}
