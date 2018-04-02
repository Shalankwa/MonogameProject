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
	class WindowMessage : Window
	{

		private string _text;
		private int _currentIndex;

		public WindowMessage(string text, TextSpeed textSpeed = TextSpeed.Fast) : base()
		{
			Height = 80;
			Width = 240;
			_currentIndex = 0;
			Position = new Vector2(40, 140);

			_text = text;
			active = true;
			done = false;

			SpliteMessage(text);

			GameModeManager.gameMode = GameMode.Dialog;
			InputManager.FireNewInput += EndText;
		}

		private void EndText(object sender, NewInputEventArgs e)
		{

			if (_currentIndex <= _text.Length - 1) return;

			if (e.Input != Input.Interact) return;

			GameModeManager.gameMode = GameMode.Play;

			DeInitialize();
			done = true;
			active = false;
		}

		private void SpliteMessage(string text)
		{
			string tempText = "";
			if(Font.MeasureString(text).X > Width)
			{
				string[] atext = text.Split(' ');

				foreach(string s in atext)
				{
					if(Font.MeasureString(tempText + s).X > (Width - 5))
					{
						tempText.Remove(tempText.Length - 1);
						tempText += "\n" + s + " ";
					}
					else
					{
						tempText += s + " ";
					}
				}
			} else
			{
				return;
			}
			_text = tempText;
		}

		private int findSpace(string text)
		{
			return 0;
		}

		public override void Reset()
		{

		}

		public override void Update(double gameTime)
		{
			if (_currentIndex <= _text.Length - 1)
				_currentIndex++;

		}

		public override void DeInitialize()
		{
			InputManager.FireNewInput -= EndText;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			FunctionManager.DrawText(Font, _text.Substring(0, _currentIndex), new Vector2(Position.X + 5, Position.Y + 5), Color.White, spriteBatch);
		}
	}
}
