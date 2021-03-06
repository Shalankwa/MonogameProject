﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class Sprite : Component
	{
		private Texture2D my_texture;
		public Color colour { get; set; }
		public int width { get; private set; }
		public int height { get; private set; }
		public Vector2 Position { get; private set; }
		public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, width, height); } }
		public float rotation { get; set; }

        public Sprite(Texture2D texture, int width, int height, Vector2 position)
        {
            my_texture = texture;
            this.width = width;
            this.height = height;
            Position = position;
			colour = Color.White;
			rotation = 0;
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
			var camera = GetComponent<Camera>(ComponentType.Camera);
			Vector2 pos;

			// if camera not found, or not in screen, exit, no draw
			if (!(camera != null && camera.GetPosition(Position, out pos)))
				return;

			var ani = GetComponent<Animation>(ComponentType.Animation);
			if(ani != null)
			{
				FunctionManager.DrawAtLayer(my_texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), ani.TextureRectangle, 2, colour, spritebatch);
				//spritebatch.Draw(my_texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), ani.TextureRectangle, Color.White);
				// Old method for drawing before layering
			}
			else
			{
				FunctionManager.DrawAtLayer(my_texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), rotation, null, 2, null, spritebatch);

			}
			

        }

        public void Move(float x, float y)
        {
            //Update pos based on movement
            Position = new Vector2(Position.X + x, Position.Y + y);

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
			}else { // No movement default
				ani.Stand();
			}
		}

		public void rotateAroundObj(Vector2 pos, float rotation)
		{
			Position = Vector2.Transform(Position - pos, Matrix.CreateRotationZ(rotation)) + pos;

		}

		public void rotateAroundObjAtDistance(Vector2 pos, float rotation, int distance)
		{
			Position = Vector2.Transform(Position - pos, Matrix.CreateRotationZ(rotation)) + pos;
		}

		public void Teleport(Vector2 pos)
		{
			Position = pos;
		}
    }
}
