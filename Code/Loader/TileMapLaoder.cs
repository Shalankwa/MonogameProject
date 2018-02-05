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

			//Link TMX document
			XDocument xDox = XDocument.Load(string.Format("Content/{0}.tmx", mapName));

			//Gather map info
			int mapWidth = int.Parse(xDox.Root.Attribute("width").Value);
			int mapHeight = int.Parse(xDox.Root.Attribute("height").Value);
			int columns = int.Parse(xDox.Root.Element("tileset").Attribute("columns").Value);
			string tileSet_name = xDox.Root.Element("tileset").Attribute("name").Value;
			List<int> _uniqueTiles = new List<int>();

			//Collect tiles with unique attributes
			foreach (XElement elm in xDox.Root.Element("tileset").Descendants("tile"))
			{
				_uniqueTiles.Add(int.Parse(elm.Attribute("id").Value));
			}

			//For each layer to the map
			foreach (XElement elm in xDox.Descendants("layer"))
			{
				string TileIDs =elm.Element("data").Value;
				string[] sTileIDS = TileIDs.Split(',');

				for (int n = 0; n < sTileIDS.Length; n++)
				{
					int tileID = int.Parse(sTileIDS[n]);
					if (tileID == 0) continue;

					int xpos = n % mapWidth;
					int ypos = n / mapWidth;
					int zpos = 0;
					int txpos = tileID % columns - 1;
					int typos = tileID / columns;
					Tile t;

					if (_uniqueTiles.Contains(tileID - 1))
					{

						List<TileFrame> tileFrames = new List<TileFrame>();
						loadTileInfo(xDox, tileFrames, tileID, columns);
						t = new Tile(xpos, ypos, zpos, tileFrames, 100, tileSet_name);

					}
					else
					{
						t = new Tile(xpos, ypos, zpos, txpos, typos, tileSet_name);
					}
					tiles.Add(t);
				}

			}

			return false;
		}

		private static void loadTileInfo(XDocument xDox, List<TileFrame> tileFrames, int tileID, int columns)
		{
			foreach (XElement elm in xDox.Root.Element("tileset").Descendants("tile"))
			{
				if(int.Parse(elm.Attribute("id").Value) == tileID - 1)
				{
					foreach (XElement frame in elm.Element("animation").Descendants("frame"))
					{
						int newTildID = int.Parse(frame.Attribute("tileid").Value);
						int newtxpos = newTildID % columns;
						int newtypos = newTildID / columns;

						tileFrames.Add(new TileFrame(newtxpos, newtypos));
					}
				}
			}
		}
	}
}
