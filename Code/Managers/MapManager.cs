using Game1.Code.EventHandlers;
using Game1.Code.Loader;
using Game1.Code.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Managers
{
	class MapManager
	{
		const int _tileHeight = 16;
		const int _tileWidth = 16;
		private int MapAreaX = 4;
		private int MapAreaY = 4;
		private List<Tile> _prevMapAreaTiles;
		private List<Tile> _tiles;
		private List<TileCollision> _tileCollisions;
		private string _mapName;
		private string _mapArea { get { return "Maps/" + _mapName + "_X" + MapAreaX + "_Y" + MapAreaY; } }
		private CameraManager _cameraManager;
		private ContentManager _content;

		public MapManager(string mapName, CameraManager cameraManager)
		{
			_cameraManager = cameraManager;
			_tiles = new List<Tile>();
			_tileCollisions = new List<TileCollision>();
			_mapName = mapName;
			CameraManager.FireCameraTransition += loadNewArea;
		}

		public void Initialize()
		{
			CameraManager.FireCameraTransition += loadNewArea;
		}

		public void Uninitialize()
		{
			CameraManager.FireCameraTransition -= loadNewArea;
		}

		private void loadNewArea(object sender, CameraTransitionEvent e)
		{
			_prevMapAreaTiles = _tiles;
			_tiles = new List<Tile>();

			int MapOffsetX = (int)_cameraManager._moveToPosition.X / _tileWidth; 
			int MapOffsetY = (int)_cameraManager._moveToPosition.Y	/ _tileHeight;
			bool loadObjects = !PlayerManager.Explored(MapAreaX, MapAreaY);

			if (e.Direction == Direction.Left) MapAreaX -= 1;
			if (e.Direction == Direction.Right) MapAreaX += 1;
			if (e.Direction == Direction.Up) MapAreaY -= 1;
			if (e.Direction == Direction.Down) MapAreaY += 1;

			if (!TileMapLoader.LoadTileMap<Tile>(_mapArea, out _tiles, out _tileCollisions, loadObjects))
			{
				TileMapLoader.LoadTileMap<Tile>("Maps/Map2_X3_Y4", out _tiles, out _tileCollisions, loadObjects);
			}

			foreach (var tile in _tiles)
			{
				tile.xPos += MapOffsetX;
				tile.yPos += MapOffsetY;
				tile.cameraManager = _cameraManager;
				tile.LoadContent(_content);
			}
			foreach (var tileCollision in _tileCollisions)
			{
				tileCollision.Xbound += MapOffsetX;
				tileCollision.Ybound += MapOffsetY;
				tileCollision.cameraManager = _cameraManager;
			}

			PlayerManager.UpdateMap(MapAreaX, MapAreaY);
		}

		public void LoadContent(ContentManager content)
		{
			_content = content;
			TileMapLoader.LoadTileMap<Tile>(_mapArea, out _tiles, out _tileCollisions, true);

			foreach(var tile in _tiles)
			{
				tile.LoadContent(content);
				tile.cameraManager = _cameraManager;
			}
			_tileCollisions.ForEach(t => t.cameraManager = _cameraManager);
		}

		internal bool CheckCollision(Rectangle rec)
		{
			//Using Any instead of for loop to cheack each tile for intersection
			return _tileCollisions.Any(tile => tile.Intersect(rec));
		}

		public void Draw(SpriteBatch sb)
		{
			foreach (var tile in _tiles)
			{
				tile.Draw(sb);
			}

			if(_prevMapAreaTiles != null)
			{
				foreach (var tile in _prevMapAreaTiles)
				{
					tile.Draw(sb);
				}
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
