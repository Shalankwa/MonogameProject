using Game1.Code.Componets.Items;
using Game1.Code.Managers;
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
		}

		public void addItem(Item item)
		{
			_items.Add(item);
			item.LoadContent(_content, _cameraManager);
		}

		public void EquiptItemToSlot(int id, ItemSlot itemSlot)
		{
			var item = _items.FirstOrDefault(i => i.ItemId == id);
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
