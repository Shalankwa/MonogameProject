using Game1.Code.EventHandlers;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
    class InputManager
    {
        private KeyboardState keyState;
        private KeyboardState oldKeyState;
		private MouseState mouseState;
		private MouseState oldMouseState;
		private Keys lastKey;
		private ButtonState lastMousePress;
        private static event EventHandler<NewInputEventArgs> _FireNewInput;
        private double counter;
        private static double cooldown;
		private static bool Paused;

        //Static access to subscribe to event
        public static event EventHandler<NewInputEventArgs> FireNewInput
        {
            add { _FireNewInput += value; }
            remove { _FireNewInput -= value; }
        }

        public static bool ThrottleInput { get; set; }
        public static bool LockMovement { get; set; }

		public static void PauseInput() { Paused = true; }
		public static void UnPauseInput() { Paused = false; }
		public static void Cooldown(int n) { cooldown = n; }


		public InputManager()
        {
            ThrottleInput = false;
            LockMovement = false;
            counter = 0;
        }

        public void Update(double gameTime)
        {

			if (Paused) return;

            if(cooldown > 0)
            {
                counter += gameTime;
                if (counter > cooldown)
                {
                    counter = 0;
                    cooldown = 0;
                }
                else return;

            }

            ComputerControlls(gameTime);
        }

        private void ComputerControlls(double gameTime)
        {
            keyState = Keyboard.GetState();
			mouseState = Mouse.GetState();

			// || (lastMousePress == ButtonState.Released)
			if ((keyState.IsKeyUp(lastKey) && lastKey != Keys.None))
            {
                if (_FireNewInput != null)
                {
                    _FireNewInput(this, new NewInputEventArgs(Input.None));
                }
            }

			CheckKeyState(Keys.Enter, Input.Enter);
			keyPressed(Keys.B, Input.Select);
			
			CheckKeyState(Keys.Left, Input.Left);
			CheckKeyState(Keys.Right, Input.Right);
			CheckKeyState(Keys.Down, Input.Down);
			CheckKeyState(Keys.Up, Input.Up);
			CheckKeyState(Keys.A, Input.Left);
			CheckKeyState(Keys.D, Input.Right);
			CheckKeyState(Keys.S, Input.Down);
			CheckKeyState(Keys.W, Input.Up);

			keyPressed(Keys.E, Input.Interact);
			CheckMouseState(mouseState.LeftButton, Input.LeftClick);
			CheckMouseState(mouseState.RightButton, Input.RightClick);
			

			oldKeyState = keyState;
			oldMouseState = mouseState;

		}

		private void keyPressed(Keys key, Input fireInput)
		{
			if(keyState.IsKeyDown(key) && !oldKeyState.IsKeyDown(key))
			{
				if (_FireNewInput != null)
				{
					_FireNewInput(this, new NewInputEventArgs(fireInput));
					lastKey = key;
				}
			}
		}

        private void CheckKeyState(Keys key, Input fireInput)
        {
            if (keyState.IsKeyDown(key))
            {
                if(!ThrottleInput || (ThrottleInput && oldKeyState.IsKeyUp(key)))
                {
                    if(_FireNewInput != null)
                    {
                        _FireNewInput(this, new NewInputEventArgs(fireInput));
                        lastKey = key;
                    }
                }
            }
        }

		private void CheckMouseState(ButtonState button, Input fireInput)
		{
			if(fireInput == Input.RightClick)
			{
				lastMousePress = oldMouseState.RightButton;
			} else
			{
				lastMousePress = oldMouseState.LeftButton;
			}

			if (button == ButtonState.Pressed)
			{
				if (!ThrottleInput || (ThrottleInput && lastMousePress == ButtonState.Released))
				{
					if (_FireNewInput != null)
					{
						_FireNewInput(this, new NewInputEventArgs(fireInput));
						lastMousePress = button;
					}
				}
			}
		}
	}
}
