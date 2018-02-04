using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Diagnostics;
using Game1.Code.Map;

namespace Game1.Code.Loader
{
	class TileMapLoader
	{

		public static bool LoadTileMap<T>(string mapName, out List<Tile> tiles)
		{

			tiles = new List<Tile>();

			XDocument xDox = XDocument.Load(string.Format("Conectent/{0}.tmx", mapName));

			int mapWidth = int.Parse(xDox.Root.Attribute("width").Value);
			int mapHeight = int.Parse(xDox.Root.Attribute("height").Value);
			int layers = int.Parse(xDox.Root.Attribute("Layers").Value);
			int tileSize = int.Parse(xDox.Root.Attribute("tileWidth").Value);

			string TileIDs = xDox.Root.Element("Layer1").Element("data").Value;
			string[] sTileIDS = TileIDs.Split(',');


			for (int n = 0; n < sTileIDS.Length; n++)
			{
				Tile t = new Tile(n % mapWidth, n / mapHeight, 0, int.Parse(sTileIDS[n]) % tileSize, int.Parse(sTileIDS[n]) / tileSize, "dungeon_Sheet.png");
				tiles.Add(t);
			}

			return false;
		}

	}
}
