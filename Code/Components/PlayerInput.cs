using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components.Interactions;
using Game1.Code.EventHandlers;
using Game1.Code.GUI_Elements;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
    class PlayerInput : Component
    {
        public override ComponentType ComponentType
        {
            get { return ComponentType.PlayerInput; }
        }

        public PlayerInput()
        {
            //Subscribe to FireNewInputs events, call PlayerAction Method
            InputManager.FireNewInput += PlayerAction;
        }

		public override void Uninitalize()
		{
			InputManager.FireNewInput -= PlayerAction;
		}

		//Perform PlayerAction based on event Args
		void PlayerAction(object sender, NewInputEventArgs e)
        {

			if (GameModeManager.gameMode != GameMode.Play) return;

            //Get sprite component from BaseObject class
            var sprite = GetComponent<Sprite>(ComponentType.Sprite);
            if (sprite == null) return;

			var ani = GetComponent<Animation>(ComponentType.Animation);
			if (ani == null) return;

			var camera = GetComponent<Camera>(ComponentType.Camera);
			if (camera == null || camera.GetInTransition()) return;

			var interact = GetComponent<Interaction>(ComponentType.Interaction);
			if (interact == null) return;

			var collision = GetComponent<Collision>(ComponentType.Collision);

			var x = 0f;
			var y = 0f;

			var inventory = GetComponent<Inventory>(ComponentType.Inventory);

            switch (e.Input)
            {
                case Input.Up:
					y = -1.5f;
					ani.currDirection = Direction.Up;
                    break;
                case Input.Down:
					y = 1.5f;
					ani.currDirection = Direction.Down;
					break;
                case Input.Left:
					x = -1.5f;
					ani.currDirection = Direction.Left;
					break;
                case Input.Right:
					x = 1.5f;
					ani.currDirection = Direction.Right;
					break;
				case Input.LeftClick:
					inventory.UseItemInSlot(ItemSlot.slot1);
					break;
				case Input.RightClick:
					inventory.UseItemInSlot(ItemSlot.slot2);
					break;
				case Input.Interact:
					interact.action();
					break;
				case Input.Select:
					WindowManager.newWindow(new WindowMenu());
					break;
			}

			if(collision != null && !collision.CheckCollision(
				new Rectangle((int)(sprite.Position.X + x), (int)(sprite.Position.Y + y), sprite.width, sprite.height))){
				sprite.Move(x, y);
			}
			else
			{
				ani.currState = State.Walking;
			}

			Vector2 position;

			if (!camera.GetPosition(sprite.Position, out position))
			{
				camera.MoveCamera(camera.GetDirectionOutOfScreen(sprite.Position));
			}
		}

		public override void Draw(SpriteBatch spritebatch)
        {
            
        }

        public override void Update(double gameTime)
        {

        }
    }
}
