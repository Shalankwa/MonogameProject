﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Diagnostics;
using Game1.Code.Map;
using Game1.Code.EventHandlers;
using Microsoft.Xna.Framework;
using Game1.Code.Managers;

namespace Game1.Code.Loader
{
	class TileMapLoader
	{
		private static int mapWidth;
		private static int mapHeight;
		private static int tileWidth;
		private static int tileHeight;
		private static int columns;
		private static int currLayer;
		private static string tileSet_name;
		private static event EventHandler<NewMapObjectEvent> _newMapObject;

		//Static access to subscribe to event
		public static event EventHandler<NewMapObjectEvent> NewMapObject
		{
			add { _newMapObject += value; }
			remove { _newMapObject -= value; }
		}

		public static bool LoadTileMap<T>(string mapName, out List<Tile> tiles, out List<TileCollision> collisions, bool loadObjects)
		{
			collisions = new List<TileCollision>();
			tiles = new List<Tile>();


			//Link TMX document
			XDocument xDox;
			try
			{
				xDox = XDocument.Load(string.Format("Content/{0}.tmx", mapName));

			}catch(Exception)
			{
				Debug.Print("Failed to find " + mapName + " map data.");
				return false;
			}

			//Gather map info
			mapWidth = int.Parse(xDox.Root.Attribute("width").Value);
			tileWidth = int.Parse(xDox.Root.Attribute("tilewidth").Value);
			mapHeight = int.Parse(xDox.Root.Attribute("height").Value);
			tileHeight = int.Parse(xDox.Root.Attribute("tileheight").Value);
			columns = int.Parse(xDox.Root.Element("tileset").Attribute("columns").Value);
			tileSet_name = xDox.Root.Element("tileset").Attribute("name").Value;
			currLayer = 0;
			List<int> _uniqueTiles = new List<int>();

			//Collect tiles with unique attributes
			foreach (XElement elm in xDox.Root.Element("tileset").Descendants("tile"))
			{
				_uniqueTiles.Add(int.Parse(elm.Attribute("id").Value));
			}

			//For each layer to the map
			foreach (XElement elm in xDox.Descendants("layer"))
			{
				string TileIDs = elm.Element("data").Value;
				string[] sTileIDS = TileIDs.Split(',');

				for (int n = 0; n < sTileIDS.Length; n++)
				{		

					int tileID = int.Parse(sTileIDS[n]);
					if (tileID == 0) continue;

					int xpos = n % mapWidth;
					int ypos = n / mapWidth;
					int zpos = 0;
					int txpos = (tileID - 1) % columns;
					int typos = (tileID - 1) / columns;
					Tile t;

					if (_uniqueTiles.Contains(tileID - 1))
					{

						List<TileFrame> tileFrames = new List<TileFrame>();
						loadTileInfo(xDox, tileFrames, collisions, tileID, columns, xpos, ypos);
						t = new Tile(xpos, ypos, zpos, tileFrames, 100, tileSet_name, currLayer);

					}
					else
					{
						t = new Tile(xpos, ypos, zpos, txpos, typos, tileSet_name, currLayer);
					}
					tiles.Add(t);
				}
				currLayer++;
			}


			//Read Objects to be placed if in a new area
			if (!loadObjects) return true;

			XElement objects;
			try
			{
				objects  = xDox.Root.Elements("objectgroup").Last();
			}
			catch
			{
				return true;
			}
			
			foreach (XElement elm in objects.Descendants("object"))
			{

				string atri = elm.Attributes().ToString();
				string name = elm.Attribute("name").Value;
				int x = int.Parse(elm.Attribute("x").Value);
				int y = int.Parse(elm.Attribute("y").Value);

				Dictionary<string, string> properties = new Dictionary<string, string>(); ;

				if (elm.Attribute("width") != null)
				{
					int width = int.Parse(elm.Attribute("width").Value);
					int height = int.Parse(elm.Attribute("height").Value);
					properties.Add("width", width.ToString());
					properties.Add("height", height.ToString());
				}

				foreach (XElement property in elm.Descendants("property"))
				{
					properties.Add(property.Attribute("name").Value, property.Attribute("value").Value);
				}

				
				_newMapObject(null, new NewMapObjectEvent(FunctionManager.ParseEnum<Objects>(name), new Vector2(x, y), properties));
				

			}
			return true;
		}

		private static void loadTileInfo(XDocument xDox, List<TileFrame> tileFrames, List<TileCollision> collisions, int tileID, int columns, int xpos, int ypos)
		{
			foreach (XElement elm in xDox.Root.Element("tileset").Descendants("tile"))
			{
				bool found = false;
				//Search to tile Element that matches ID
				if( found = (int.Parse(elm.Attribute("id").Value) == tileID - 1))
				{
					foreach (XElement elmt in elm.Elements())
					{
						bool animated = false;
						//Get tile animation frames if found
						if (animated = (elmt.Name.ToString().Equals("animation")))
						{
							foreach (XElement frame in elmt.Descendants("frame"))
							{
								int newTildID = int.Parse(frame.Attribute("tileid").Value);
								int newtxpos = newTildID % columns;
								int newtypos = newTildID / columns;

								tileFrames.Add(new TileFrame(newtxpos, newtypos));
							}
						}
						//Get tile collision if found
						if (elmt.Name.ToString().Equals("objectgroup"))
						{
							if (!animated) 
								tileFrames.Add(new TileFrame(tileID % columns - 1, tileID / columns));

							XElement col = elmt.Element("object");
							int width = int.Parse(col.Attribute("width").Value);
							int height = int.Parse(col.Attribute("height").Value);
							int xOffset = int.Parse(col.Attribute("x").Value);
							int yOffset = int.Parse(col.Attribute("y").Value);

							collisions.Add(new TileCollision(xpos, ypos, width, height, xOffset, yOffset));
						}
					}
				}
				if (found) return;
			}
		}
	}
}
