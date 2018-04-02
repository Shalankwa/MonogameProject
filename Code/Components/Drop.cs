using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Map;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class Drop : Component
	{
		private Entities _entities;
		private BaseObject DropItem;

		public override ComponentType ComponentType
		{
			get { return ComponentType.Drop; }
		}

		public Drop(Entities entities, BaseObject dropitem)
		{
			_entities = entities;
			DropItem = dropitem;
		}

		public void dropItem()
		{
			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			DropItem.GetComponent<Sprite>(ComponentType.Sprite).Teleport(sprite.Position);

			_entities.AddEntitie(DropItem);
		}

		public override void Draw(SpriteBatch spritebatch)
		{

		}

		public override void Update(double gameTime)
		{

		}
	}
}
