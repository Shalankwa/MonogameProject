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
	class WindowShop : Window
	{
		private readonly Vector2 CURSORSTART = new Vector2(13, 13);
		private readonly Vector2 TRADECUROSRSTART = new Vector2(140, 13);
		private Vector2 cursorPositionStart;
		private const int MAXCURSORPOSX = 3;
		private const int MAXCURSORPOSY = 5;
		private bool buying;

		private int cursorPosX = 0;
		private int cursorPosY = 0;
		private Texture2D cursorTexture;
		private Inventory Inventory;
		private Inventory TraderInventory;


		public WindowShop(Inventory trader) : base()
		{
			Inventory = PlayerManager.player.GetComponent<Inventory>(ComponentType.Inventory);
			TraderInventory = trader;

			Position = new Vector2(50, 50);
			cursorPositionStart = CURSORSTART;
			Width = 220;
			Height = 140;
			Opacity = 1f;
			Colour = Color.White;
			buying = false;
			cursorTexture = ManagerContent.LoadTexture("GUI/MenuCursor");
			Texture = ManagerContent.LoadTexture("GUI/MenuShop");

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
					
					break;
				case Input.RightClick:
					
					break;
				case Input.Interact:
					MakeTrade();
					break;
				case Input.Select:
					closeMenu();
					break;
			}

			if(cursorPosX + x > MAXCURSORPOSX && !buying)
			{
				cursorPosX = 0;
				buying = true;
				cursorPositionStart = TRADECUROSRSTART;
			}
			else if (cursorPosX + x < 0 && buying)
			{
				cursorPosX = MAXCURSORPOSX;
				buying = false;
				cursorPositionStart = CURSORSTART;
			} else
			{
				cursorPosX = FunctionManager.Clamp<int>(cursorPosX += x, 0, MAXCURSORPOSX);
			}

			cursorPosY = FunctionManager.Clamp<int>(cursorPosY += y, 0, MAXCURSORPOSY);
		}

		private void MakeTrade()
		{
			Item item;

			// If buying, try to make a purchase item at cursorPos
			if (buying)
			{
				TraderInventory.itemInMenu(cursorPosX, cursorPosY, out item);
				if (item != null)
				{
					if (Inventory._currency >= item.Worth)
					{
						TraderInventory.removeItem(item);
						Inventory.addItem(item);
						Inventory._currency -= item.Worth;
						TraderInventory._currency += item.Worth;
					} else
					{
						WindowManager.newWindow(new WindowTimedMessage("Not enough funds \nCost: " + item.Worth));
					}
				}
			}
			// If Selling, Try to sell item at cursorPos
			else
			{
				Inventory.itemInMenu(cursorPosX, cursorPosY, out item);
				if (item != null)
				{
					if (TraderInventory._currency >= item.Worth)
					{
						Inventory.removeItem(item);
						TraderInventory.addItem(item);
						TraderInventory._currency -= item.Worth;
						Inventory._currency += item.Worth;
					}
					else
					{
						WindowManager.newWindow(new WindowTimedMessage("Not enough funds \nCost: " + item.Worth));
					}
				}
			}

		}

		private void closeMenu()
		{
			done = true;
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
			Item item;

			// Draw Cursor
			base.Draw(spriteBatch);
			FunctionManager.DrawAtLayer(cursorTexture,
				new Rectangle((int)Position.X + (int)cursorPositionStart.X + (int)(cursorPosX * 16) + cursorPosX,
							  (int)Position.Y + (int)cursorPositionStart.Y + (int)(cursorPosY * 16) + cursorPosY, 16, 16),
				null, 10, Color.White, spriteBatch);

			// Draw currency
			FunctionManager.DrawText(Font, "" + Inventory._currency, new Vector2(Position.X + 106, Position.Y + 14), Color.Black, spriteBatch, 0.8f);
			FunctionManager.DrawText(Font, "" + TraderInventory._currency, new Vector2(Position.X + 86, Position.Y + 114), Color.Black, spriteBatch, 0.8f);

			// Draw Players items in slots
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

			// Draw Traders items in slots
			for (int y = 0; y < 3; y++)
			{
				for (int x = 0; x < 5; x++)
				{
					if (TraderInventory.itemInMenu(x, y, out item))
					{
						FunctionManager.DrawAtLayer(item.GuiTexture,
						new Rectangle((int)Position.X + (int)TRADECUROSRSTART.X + (int)(item.MenuPosition.X * 16) + (int)item.MenuPosition.X,
									  (int)Position.Y + (int)TRADECUROSRSTART.Y + (int)(item.MenuPosition.Y * 16) + (int)item.MenuPosition.Y, 16, 16),
						null, 10, Color.White, spriteBatch);
					}
				}
			}
		}
	}
}
