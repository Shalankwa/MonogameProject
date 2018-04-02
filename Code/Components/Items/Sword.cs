using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Components;
using Game1.Code.Managers;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Componets.Items
{
	class Sword : Item
	{

		private Entities _entities;
		private double duration = 0;

		public Sword(BaseObject owner, Entities entities)
		{
			ItemId = 1;
			base.owner = owner;
			_entities = entities;
		}

		public override void Reload(CameraManager camera, Entities entities)
		{
			GetComponent<Camera>(ComponentType.Camera).Reload(camera);
			_entities = entities;
		}

		public override void LoadContent(ContentManager content, CameraManager cameraManager)
		{

			RemoveAllComponents();

			var spriteO = owner.GetComponent<Sprite>(ComponentType.Sprite);
			var ownserPos = spriteO.Position;

			var sprite = content.Load<Texture2D>("Items/Sword");
			GuiTexture = sprite;

			AddComponent(new Sprite(sprite, 16, 16, ownserPos));
			AddComponent(new Camera(cameraManager));

		}

		public override void Action()
		{
			Active = true;
			duration = 200;

			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			var player = owner.GetComponent<Animation>(ComponentType.Animation);

			switch (player.currDirection)
			{
				case Direction.Up:
					sprite.rotation = -(float)(Math.PI/2);
					break;
				case Direction.Down:
					sprite.rotation = (float)(Math.PI / 2);
					break;
				case Direction.Left:
					sprite.rotation = (float)(Math.PI);
					break;
				case Direction.Right:
					sprite.rotation = 0;
					break;
			}
		}

		public override void Update(double gameTime)
		{

			var sprite = GetComponent<Sprite>(ComponentType.Sprite);
			var spriteO = owner.GetComponent<Sprite>(ComponentType.Sprite);
			var ownserPos = spriteO.Position;
			sprite.Teleport(ownserPos + new Vector2(0, -12));

		
			sprite.rotateAroundObj(ownserPos, sprite.rotation);

			duration -= gameTime;
			if (duration <= 0)
			{
				Active = false;
				sprite.rotation = 0;
			}
				
			sprite.rotation += 0.25f;

			CheckCollision(sprite);

		}

		public void CheckCollision(Sprite sprite)
		{
			BaseObject hitObj;

			if (_entities.CheckCollision(sprite.Rectangle, out hitObj, owner.Id))
			{
				hitObj.GetComponent<Damage>(ComponentType.Damage)?.TakeDamage(5);
			}
		}

	}
}
