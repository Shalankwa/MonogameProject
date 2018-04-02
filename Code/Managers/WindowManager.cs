using Game1.Code.GUI_Elements;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	class WindowManager
	{
		private static List<Window> _Windows = new List<Window>();

		public static void newWindow(Window window)
		{
			_Windows.Add(window);

		}

		public static void RemoveAll()
		{
			foreach(var window in _Windows)
			{
				window.DeInitialize();
			}

			_Windows = new List<Window>();
		}

		public void Update(double gameTime)
		{

			for(int i = 0; i < _Windows.Count; i++)
			{
				if (_Windows[i].active && !_Windows[i].done)
				{
					_Windows[i].Update(gameTime);
				}

				if (_Windows[i].done)
				{
					_Windows[i].DeInitialize();
					_Windows.RemoveAt(i);
					i--;
				}
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach(var window in _Windows)
			{
				window.Draw(spriteBatch);
			}
		}

	}
}
