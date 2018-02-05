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

			XDocument xDox = XDocument.Load(string.Format("Content/{0}.tmx", mapName));

			int mapWidth = int.Parse(xDox.Root.Attribute("width").Value);
			int mapHeight = int.Parse(xDox.Root.Attribute("height").Value);
			int columns = int.Parse(xDox.Root.Element("tileset").Attribute("columns").Value);

			//For each layer to the map
			foreach(XElement elm in xDox.Descendants("layer"))
			{
				Debug.Print(elm.ToString());
				string TileIDs =elm.Element("data").Value;
				string[] sTileIDS = TileIDs.Split(',');

				for (int n = 0; n < sTileIDS.Length; n++)
				{
					int tileID = int.Parse(sTileIDS[n]);
					if (tileID == 0) continue;
					Tile t = new Tile(n % mapWidth, n / mapHeight, 0, tileID % columns - 1, tileID / columns, "dungeon_sheet");
					tiles.Add(t);
				}

			}

			return false;
		}

	}
}
