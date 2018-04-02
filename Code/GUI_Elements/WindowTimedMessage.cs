using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.GUI_Elements
{
	class WindowTimedMessage : Window
	{

		private string _text;
		private int _currentIndex;
		private double _countDown;

		public WindowTimedMessage(string text, TextSpeed textSpeed = TextSpeed.Fast, double countDowm = 1000) : base()
		{
			Height = 80;
			Width = 240;
			_currentIndex = 0;
			_countDown = countDowm;
			Position = new Vector2(40, 140);

			_text = text;
			active = true;
			done = false;

			SpliteMessage(text);

			InputManager.PauseInput();
		}

		private void SpliteMessage(string text)
		{
			string tempText = "";
			if (Font.MeasureString(text).X > Width)
			{
				string[] atext = text.Split(' ');

				foreach (string s in atext)
				{
					if (Font.MeasureString(tempText + s).X > (Width - 5))
					{
						tempText.Remove(tempText.Length - 1);
						tempText += "\n" + s + " ";
					}
					else
					{
						tempText += s + " ";
					}
				}
			}
			else
			{
				return;
			}
			_text = tempText;
		}

		public override void Reset()
		{

		}

		public override void Update(double gameTime)
		{
			if (_currentIndex <= _text.Length - 1)
			{
				_currentIndex++;
			}
			else
			{
				_countDown -= gameTime;
				if (_countDown < 0)
				{
					done = true;
					active = false;
					InputManager.UnPauseInput();
				}
			}


		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			FunctionManager.DrawText(Font, _text.Substring(0, _currentIndex), new Vector2(Position.X + 5, Position.Y + 5), Color.White, spriteBatch);
		}
	}
}
