using Game1.Code.Componets.Items;
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

namespace Game1.Code.Components
{
	class Inventory : Component
	{
		public int _currency { get; set; }
		private List<Item> _items;
		private Dictionary<ItemSlot, Item> _equipedItem;
		private ContentManager _content;
		private CameraManager _cameraManager;

		public override ComponentType ComponentType
		{
			get { return ComponentType.Inventory; }
		}

		public Inventory(ContentManager content, CameraManager cameraManager)
		{
			_content = content;
			_equipedItem = new Dictionary<ItemSlot, Item>();
			_cameraManager = cameraManager;
			_items = new List<Item>();
			_currency = 0;
		}

		public void Reload(CameraManager cameraManager, Entities entities)
		{
			_cameraManager = cameraManager;
			_items.ForEach(i => i.Reload(cameraManager, entities));

		}

		public bool hasItem(int id)
		{
			return _items.Any(i => i.ItemId == id);
		}

		public void addItem(Item item)
		{
			item.owner = base_Object;

			Vector2 spot;
			if (FindMenuSpot(out spot))
			{
				item.MenuPosition = spot;
			}

			_items.Add(item);
			item.LoadContent(_content, _cameraManager);
		}

		public void removeItem(Item item)
		{
			UnEquipt(item);
			_items.Remove(item);
		}

		public bool itemInMenu(int x, int y, out Item item)
		{
			item = _items.Find(i => i.MenuPosition.X == x && i.MenuPosition.Y == y);

			if (item == null) return false;

			return true;
		}

		private bool FindMenuSpot(out Vector2 spot)
		{
			for(int r = 0; r < 3; r++)
			{
				for (int c = 0; c < 5; c++)
				{
					if (!_items.Any(i => i.MenuPosition.X == c && i.MenuPosition.Y == r))
					{
						spot = new Vector2(c, r);
						return true;
					}
				}
			}
			spot = new Vector2(0,0);
			return false;
		}

		public void EquiptItemToSlot(int id, ItemSlot itemSlot)
		{
			var item = _items.FirstOrDefault(i => i.ItemId == id);

			// If the item is already Equipt, return
			if (_equipedItem.Values.ToList().Contains(item)) return;

			if(item != null)
			{
				if (_equipedItem.ContainsKey(itemSlot))
					_equipedItem[itemSlot] = item;
				else
					_equipedItem.Add(itemSlot, item);
			}
		}

		public void UnEquiptItemInSlot(ItemSlot itemSlot)
		{
			if (_equipedItem.ContainsKey(itemSlot))
			{
				_equipedItem.Remove(itemSlot);
			}
		}

		private void UnEquipt(Item item)
		{
			if (_equipedItem.ContainsValue(item))
			{
				var toRemove = _equipedItem.First(i => i.Value == item);
				_equipedItem.Remove(toRemove.Key);
			}
		}

		public void UseItemInSlot(ItemSlot itemSlot)
		{
			if (_equipedItem.ContainsKey(itemSlot))
			{
				if (!_equipedItem[itemSlot].Active)
				{
					_equipedItem[itemSlot].Action();
				}
			}
		}

		public bool Equiped(ItemSlot itemSlot, out Item item)
		{
			if (_equipedItem.ContainsKey(itemSlot))
			{
				item = _equipedItem[itemSlot];
				return true;
			}
			item = null;
			return false;
		}

		public override void Update(double gameTime)
		{
			foreach (var item in _equipedItem)
			{
				if (item.Value.Active)
					item.Value.Update(gameTime);
			}
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			foreach (var item in _equipedItem)
			{
				if (item.Value.Active)
					item.Value.Draw(spritebatch);
			}
		}
	}
}
