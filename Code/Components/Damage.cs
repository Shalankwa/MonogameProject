using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class Damage : Component
	{
		private Entities _entities;
		private bool _iFrames;
		private double _counter;
		private double _iframeCounter;


		public Damage(Entities entities)
		{
			_entities = entities;
			_counter = 0;
			_iframeCounter = 0;
			_iFrames = false;
		}

		public void Reload(Entities entities)
		{
			_entities = entities;
		}

		public override ComponentType ComponentType
		{
			get { return ComponentType.Damage; }
		}


		public override void Update(double gameTime)
		{
			var stats = GetComponent<Stats>(ComponentType.Stats);
			if (stats == null) return;

			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			if (sprite == null) return;

			if (_iFrames)
			{
				IFrames(gameTime, sprite);
				return;
			}

			BaseObject hitObj;
			if (_entities.CheckCollision(sprite.Rectangle, out hitObj, GetOwnerId()))
			{
				if(hitObj.Hostile && !GetHosility())
					TakeDamage(5);
			}
		}

		public void TakeDamage(int damage)
		{
			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			if (sprite == null) return;

			var stats = GetComponent<Stats>(ComponentType.Stats);
			if (stats == null) return;

			if (!_iFrames)
			{
				//take damage; 
				stats.health -= 5;
				if (stats.health <= 0) KillBase();

				_iFrames = true;
				_iframeCounter = 1500;
				sprite.colour = new Color(100, 0, 0, 100);
			}
		
		}

		private void IFrames(double gameTime, Sprite sprite)
		{
			_counter += gameTime;
			_iframeCounter -= gameTime;

			if(_counter >= 100)
			{
				sprite.colour = (sprite.colour == Color.White) ? new Color(80, 80, 80, 50) : Color.White;
				_counter = 0;
			}

			if(_iframeCounter <= 0)
			{
				_iFrames = false;
				_counter = 0;
				_iframeCounter = 0;
			}

		}

		public override void Draw(SpriteBatch spritebatch)
		{

		}

	}
}
