using Game1.Code.Components;
using Game1.Code.Componets.Items;
using Game1.Code.EventHandlers;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.GUI_Elements
{
	class WindowMenu : Window
	{
		private readonly Vector2 CURSORSTART = new Vector2(13,13);
		private readonly Vector2 SLOT1POS = new Vector2(105, 105);
		private readonly Vector2 SLOT2POS = new Vector2(183, 105);
		private const int MAXCURSORPOSX = 3;
		private const int MAXCURSORPOSY = 5;

		private int cursorPosX = 0;
		private int cursorPosY = 0;
		private Texture2D cursorTexture;
		private Inventory Inventory;

		public WindowMenu() : base()
		{
			Inventory = PlayerManager.player.GetComponent<Inventory>(ComponentType.Inventory);

			Position = new Vector2(50, 50);
			Width = 220;
			Height = 140;
			Opacity = 1f;
			Colour = Color.White;
			cursorTexture = ManagerContent.LoadTexture("GUI/MenuCursor");
			Texture = ManagerContent.LoadTexture("GUI/Menu2");

			GameModeManager.gameMode = GameMode.Menu;
			InputManager.ThrottleInput = true;
			InputManager.FireNewInput += MenuInput;
		}

		private void MenuInput(object sender, NewInputEventArgs e)
		{
			int x = 0;
			int y = 0;

			switch (e.Input)
			{
				case Input.Up:
					y--;
					break;
				case Input.Down:
					y++;
					break;
				case Input.Left:
					x--;
					break;
				case Input.Right:
					x++;
					break;
				case Input.LeftClick:
					Equipt(ItemSlot.slot1);
					break;
				case Input.RightClick:
					Equipt(ItemSlot.slot2);
					break;
				case Input.Interact:

					break;
				case Input.Select:
					closeMenu();
					break;
			}

			cursorPosX = FunctionManager.Clamp<int>(cursorPosX += x, 0, MAXCURSORPOSX);
			cursorPosY = FunctionManager.Clamp<int>(cursorPosY += y, 0, MAXCURSORPOSY);
		}

		private void closeMenu()
		{
			done = true;
		}

		private void Equipt(ItemSlot slot)
		{
			Item item;
			if(Inventory.itemInMenu(cursorPosX, cursorPosY, out item))
			{
				Inventory.EquiptItemToSlot(item.ItemId, slot);
			}
			else
			{
				Inventory.UnEquiptItemInSlot(slot);
			}
		}

		public override void DeInitialize()
		{
			InputManager.FireNewInput -= MenuInput;
			InputManager.ThrottleInput = false;
			GameModeManager.gameMode = GameMode.Play;
		}

		public override void Reset()
		{
			
		}

		public override void Update(double gameTime)
		{
			
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			// Draw Equipped items
			Item item;
			if (Inventory.Equiped(ItemSlot.slot1, out item))
			{
				FunctionManager.DrawAtLayer(item.GuiTexture,
					new Rectangle(
						(int)Position.X + (int)SLOT1POS.X, 
						(int)Position.Y + (int)SLOT1POS.Y, 16, 16), null, 10, Color.White, spriteBatch);
			}
			if (Inventory.Equiped(ItemSlot.slot2, out item))
			{
				FunctionManager.DrawAtLayer(item.GuiTexture, 
					new Rectangle(
						(int)Position.X + (int)SLOT2POS.X, 
						(int)Position.Y + (int)SLOT2POS.Y, 16, 16), null, 10, Color.White, spriteBatch);
			}

			// Draw Cursor
			base.Draw(spriteBatch);
			FunctionManager.DrawAtLayer(cursorTexture, 
				new Rectangle((int)Position.X + (int)CURSORSTART.X + (int)(cursorPosX * 16) + cursorPosX, 
							  (int)Position.Y + (int)CURSORSTART.Y + (int)(cursorPosY * 16) + cursorPosY, 16, 16),
				null, 10, Color.White, spriteBatch);

			// Draw currency
			FunctionManager.DrawText(Font, " x   " + Inventory._currency, new Vector2(Position.X + 132, Position.Y + 40), Color.Black, spriteBatch);


			// Draw items in slots
			for (int y = 0; y < 3; y++)
			{
				for (int x = 0; x < 5; x++)
				{
					if (Inventory.itemInMenu(x, y, out item))
					{
						FunctionManager.DrawAtLayer(item.GuiTexture,
						new Rectangle((int)Position.X + (int)CURSORSTART.X + (int)(item.MenuPosition.X * 16) + (int)item.MenuPosition.X,
									  (int)Position.Y + (int)CURSORSTART.Y + (int)(item.MenuPosition.Y * 16) + (int)item.MenuPosition.Y, 16, 16),
						null, 10, Color.White, spriteBatch);
					}
				}
			}
		}
	}
}
