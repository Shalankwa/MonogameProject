﻿using Microsoft.Xna.Framework;
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
			foreach (var baseObject in _entities)
			{
				baseObject.Update(gameTime);
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

	}
}
