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
        private Keys lastKey;
        private static event EventHandler<NewInputEventArgs> _FireNewInput;
        private double counter;
        private static double cooldown;

        //Static access to subscribe to event
        public static event EventHandler<NewInputEventArgs> FireNewInput
        {
            add { _FireNewInput += value; }
            remove { _FireNewInput -= value; }
        }

        public static bool ThrottleInput { get; set; }
        public static bool LockMovement { get; set; }

        public InputManager()
        {
            ThrottleInput = false;
            LockMovement = false;
            counter = 0;
        }

        public void Update(double gameTime)
        {
            if(cooldown > 0)
            {
                counter += gameTime;
                if (counter > gameTime)
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

            if (keyState.IsKeyUp(lastKey) && lastKey != Keys.None)
            {
                if (_FireNewInput != null)
                {
                    _FireNewInput(this, new NewInputEventArgs(Input.None));
                }
            }

            CheckKeyState(Keys.Left, Input.Left);
            CheckKeyState(Keys.Right, Input.Right);
            CheckKeyState(Keys.Down, Input.Down);
            CheckKeyState(Keys.Up, Input.Up);
            CheckKeyState(Keys.A, Input.Left);
            CheckKeyState(Keys.D, Input.Right);
            CheckKeyState(Keys.S, Input.Down);
            CheckKeyState(Keys.W, Input.Up);
            CheckKeyState(Keys.Enter, Input.Enter);

			oldKeyState = keyState;
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
    }
}
