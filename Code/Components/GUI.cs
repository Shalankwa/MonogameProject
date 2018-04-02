using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Code.Componets.Items;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Code.Components
{
	class GUI : Component
	{
		private Texture2D _container;
		private Texture2D _Vbar;
		private Texture2D _Hbar;
		private Texture2D _HealthContainer;
		private Texture2D _Slot;

		public override ComponentType ComponentType
		{
			get { return ComponentType.GUI; }
		}

		public void LoadContent(ContentManager content)
		{
			_container = content.Load<Texture2D>("container_GUI");
			_HealthContainer = content.Load<Texture2D>("GUI/HealthContainer");
			_Vbar = content.Load<Texture2D>("GUI/Vbar");
			_Hbar = content.Load<Texture2D>("GUI/Hbar");
			_Slot = content.Load<Texture2D>("GUI/Slot");

		}

		public override void Draw(SpriteBatch spritebatch)
		{

			// Draw HP bar
			var stats = GetComponent<Stats>(ComponentType.Stats);
			if (stats == null) return;
			var player = GetComponent<Sprite>(ComponentType.Sprite);
			if (player == null) return;
			var Camera = GetComponent<Camera>(ComponentType.Camera);
			if (player == null) return;

			Vector2 pos;
			Camera.GetPosition(player.Position, out pos);

			DrawSlots(pos, spritebatch);
			DrawHP(pos, spritebatch, stats);

		}

		public void DrawSlots(Vector2 pos, SpriteBatch spriteBatch)
		{
			var Equiped = GetComponent<Inventory>(ComponentType.Inventory);

			//int Slot1YPos = (pos.Y > 120 && pos.X > 180) ? 10 : 210;
			//int Slot2YPos = (pos.Y > 120 && pos.X > 180) ? 30 : 190;
			int Slot1YPos = 210;
			int Slot2YPos = 190;

			float colourA = (pos.Y > 120 && pos.X > 180) ? 0.5f : 1;

			Item item;
			if (Equiped.Equiped(ItemSlot.slot1, out item))
			{
				FunctionManager.DrawAtLayer(item.GuiTexture, new Rectangle(265+2, Slot1YPos+2, 16, 16), null, 7, Color.White * colourA, spriteBatch);
			}
			if (Equiped.Equiped(ItemSlot.slot2, out item))
			{
				FunctionManager.DrawAtLayer(item.GuiTexture, new Rectangle(280 + 2, Slot2YPos + 2, 16, 16), null, 7, Color.White * colourA, spriteBatch);
			}

			FunctionManager.DrawAtLayer(_Slot, new Rectangle(265, Slot1YPos, 20, 20), null, 6, Color.White * colourA, spriteBatch);
			FunctionManager.DrawAtLayer(_Slot, new Rectangle(280, Slot2YPos, 20, 20), null, 6, Color.White * colourA, spriteBatch);
		}

		public void DrawHP(Vector2 pos, SpriteBatch spriteBatch, Stats stats)
		{
			//int HPstartYPos = (pos.Y > 120 && pos.X > 180) ? 1 : 159;
			int HPstartYPos = 159;
			int HPheight = (int)(67 * (stats.health / 100.0)); // 80 - 13
			int Voffset = 67 - HPheight + 11;

			float colourA = (pos.Y > 120 && pos.X > 180) ? 0.5f : 1;

			FunctionManager.DrawAtLayer(_Vbar, new Rectangle(304, HPstartYPos + Voffset, 16, HPheight), null, 6, Color.LimeGreen*colourA, spriteBatch);
			FunctionManager.DrawAtLayer(_HealthContainer, new Rectangle(304, HPstartYPos, 16, 80), null, 7, Color.White*colourA, spriteBatch);
		}

		public override void Update(double gameTime)
		{

		}
	}
}
