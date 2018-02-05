using Game1.Code.Loader;
using Game1.Code.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	class MapManager
	{
		const int _tileHeight = 16;
		const int _tileWidth = 16;
		List<Tile> _tiles;
		string _mapName;

		public MapManager(string mapName)
		{
			_mapName = mapName;
		}

		public void LoadContent(ContentManager content)
		{
			_tiles = new List<Tile>();
			TileMapLoader.LoadTileMap<Tile>(_mapName, out _tiles);

			foreach(var tile in _tiles)
			{
				tile.LoadContent(content);
			}

		}

		public void Draw(SpriteBatch sb)
		{
			foreach (var tile in _tiles)
			{
				tile.Draw(sb);
			}
		}

		public void Update(double gameTime)
		{
			foreach (var tile in _tiles)
			{
				tile.Update(gameTime);
			}
		}
	}
}
