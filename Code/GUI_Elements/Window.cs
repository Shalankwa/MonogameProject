using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.GUI_Elements
{
	abstract class Window
	{
		public Vector2 Position { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		protected SpriteFont Font;
		protected Texture2D Texture;
		protected Color FontColour;
		public Color Colour;
		public bool done { get; set; }
		public bool active { get; set; }
		public bool AMenu { get; set; }
		public float Opacity { get; set; }

		protected Window()
		{
			FontColour = Color.White;
			AMenu = false;
			Opacity = 0.8f;
			Colour = Color.Black;
			Font = ManagerContent.LoadFont("GUI/GUI_Font");
			Texture = ManagerContent.LoadTexture("GUI/BackgroundWhite");
		}

		public virtual void LoadContent(ContentManager content)
		{
			
		}

		public abstract void Update(double gameTime);
		public abstract void Reset();
		public virtual void DeInitialize() { }

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			FunctionManager.DrawAtLayer(Texture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), null, 9, Colour * Opacity, spriteBatch);
		}


	}
}
