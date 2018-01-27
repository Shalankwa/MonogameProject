using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
    class Sprite : Component
    {
        private Texture2D my_texture;
        private int width;
        private int height;
        private Vector2 pos;

        public Sprite(Texture2D texture, int width, int height, Vector2 position)
        {
            my_texture = texture;
            this.width = width;
            this.height = height;
            pos = position;
        }

        public override ComponentType ComponentType
        {
            get { return ComponentType.Sprite; }
        }

        public override void Update(double gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
			var ani = GetComponent<Animation>(ComponentType.Animation);
			if(ani != null)
			{
				spritebatch.Draw(my_texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), ani.TextureRectangle, Color.White);
			}
			else
			{
				spritebatch.Draw(my_texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);
			}

        }

        public void Move(float x, float y)
        {
            //Update pos based on movement
            pos = new Vector2(pos.X + x, pos.Y + y);

			var ani = GetComponent<Animation>(ComponentType.Animation);
			if (ani == null) return;

			if (x > 0)
			{
				ani.ResetCounter(State.Walking, Direction.Right);
			}
			else if(x < 0) {
				ani.ResetCounter(State.Walking, Direction.Left);
			}
			else if(y > 0)
			{
				ani.ResetCounter(State.Walking, Direction.Down);
			}else if (y < 0)
			{
				ani.ResetCounter(State.Walking, Direction.Up);
			}

		}
    }
}
