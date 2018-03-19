using Game1.Code.Managers;
using Game1.Code.MapObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Factories
{
	static class ObjectFactory
	{

		public static BaseObject newObject(Objects obj, Vector2 position, Dictionary<string, string> properties, BaseObject player, ScreenManager screenManager, ContentManager content)
		{
			BaseObject newObject = null;

			switch (obj)
			{
				case Objects.TriggerScene:
					newObject = newSceneTrigger(obj, position, properties, player, screenManager, content);
					break;
			}

			return newObject;
		}

		private static BaseObject newSceneTrigger(Objects obj, Vector2 position, Dictionary<string, string> properties, BaseObject player, ScreenManager screenManager,ContentManager content)
		{
			if (!properties.ContainsKey("width")) return null;

			SceneTrigger sceneTrigger;

			string sceneName = properties["TriggerScene"];

			Debug.Print("new SceneTrigger");
			sceneTrigger = new SceneTrigger(sceneName, position, int.Parse(properties["width"]), int.Parse(properties["height"]), player, screenManager, content);

			return sceneTrigger;
		}


	}
}
