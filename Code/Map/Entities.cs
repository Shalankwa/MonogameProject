using Game1.Code.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Map
{
	class Entities
	{
		private List<BaseObject> _entities;

		public Entities()
		{
			_entities = new List<BaseObject>();
		}

		public void CreatePlayer(Vector2 position)
		{

		}

		public void AddEntitie(BaseObject newEnt)
		{
			_entities.Add(newEnt);
		}

		public void Update(double gameTime)
		{

			for (int i = 0; i < _entities.Count; i++)
			{
				_entities[i].Update(gameTime);
				if (_entities[i].Dead)
				{
					_entities.RemoveAt(i);
					i--;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach(var baseObject in _entities)
			{
				baseObject.Draw(spriteBatch);
			}
		}

		public void Initialize()
		{

		}

		public void Uninitialize()
		{
			if (_entities == null) return;

		}

		public bool CheckCollision(Rectangle rectangle, out BaseObject hitObj, int ID)
		{
			foreach(var baseObj in _entities)
			{
				if (ID == baseObj.Id) continue;

				var sprite = baseObj.GetComponent<Sprite>(ComponentType.Sprite);
				if (sprite == null) continue;

				if (sprite.Rectangle.Intersects(rectangle))
				{
					hitObj = baseObj;
					return true;
				}

			}
			hitObj = null;
			return false;
		}

	}
}
