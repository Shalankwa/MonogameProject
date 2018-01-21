using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.EventHandlers;
using Game1.Code.Managers;
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

        //Perform PlayerAction based on event Args
        void PlayerAction(object sender, NewInputEventArgs e)
        {

            //Get sprite component from BaseObject class
            var sprite = GetComponent<Sprite>(ComponentType.Sprite);
            if (sprite == null) return;

            switch (e.Input)
            {
                case Input.Up:
                    sprite.Move(0, -1.5f);
                    break;
                case Input.Down:
                    sprite.Move(0, 1.5f);
                    break;
                case Input.Left:
                    sprite.Move(-1.5f, 0);
                    break;
                case Input.Right:
                    sprite.Move(1.5f, 0);
                    break;
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
