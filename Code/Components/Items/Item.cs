using Game1.Code.Managers;
using Game1.Code.Map;
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

		public BaseObject owner;
		public Texture2D GuiTexture { get; protected set; }
		
		public int Worth { get; private set; }
		public int ItemId { get; protected set; }
		public bool Active { get; set; }
		public Vector2 MenuPosition { get; set; }

		public Item(int worth = 100)
		{
			Worth = worth;
		}

		public virtual void LoadContent(ContentManager content, CameraManager cameraManager)
		{
			
		}

		public abstract void Reload(CameraManager camera, Entities entities);
		public abstract void Action();

	}
}
