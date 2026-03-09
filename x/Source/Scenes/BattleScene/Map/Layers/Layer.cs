using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map.Layers
{
	public partial class Layer : TileMapLayer
	{
		public int TileSize { get => TileSet.TileSize.X; }
		
		public void LoadLayer(string path)
		{
			if (ResourceLoader.Exists(path))
			{
				Resources.Levels.Pattern pattern = GD.Load<Resources.Levels.Pattern>(path);
				
				if (pattern != null)
				{
					SetPattern(pattern.Offset, pattern.Tiles);
				}
			}
		}
		
		protected int[] GetSceneTileIds(TileMapLayer layer, int sourceId)
		{
			TileSet tileSet = layer.TileSet;
			TileSetScenesCollectionSource source = tileSet.GetSource(sourceId) as TileSetScenesCollectionSource;
			int count = source.GetSceneTilesCount();
			int[] tileIds = new int[count];
			for (int i = 0; i < count; i++)
			{
				int id = source.GetSceneTileId(i);
				tileIds[i] = id;
			}
			return tileIds;
		}
	}
}
