using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Componets.Items
{
	abstract class Item : BaseObject
	{

		protected BaseObject _owner;
		public Texture2D GuiTexture { get; protected set; }
		
		public int ItemId { get; set; }
		public bool Active { get; set; }
		public Vector2 MenuPosition { get; set; }

		public virtual void LoadContent(ContentManager content, CameraManager cameraManager)
		{
			
		}

		public abstract void Action();

	}
}
