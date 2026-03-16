using Godot;
using System;

namespace drumstalotajs.Mapping.Layers;

public partial class Layer : TileMapLayer
{
	public int TileSize { get => TileSet.TileSize.X; }
	
	public Vector2I[] GetTileAtlas()
	{
		TileSet tileSet = TileSet;
		TileSetAtlasSource source = tileSet.GetSource(0) as TileSetAtlasSource;
		int count = source.GetTilesCount();
		Vector2I[] tileAtlas = new Vector2I[count];
		for (int i = 0; i < count; i++)
		{
			Vector2I coords = source.GetTileId(i);
			tileAtlas[i] = coords;
		}
		return tileAtlas;
	}
	
	public void ExportTiles()
	{
		
	}
	/*
		
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
		}*/
}
