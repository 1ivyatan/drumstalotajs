using Godot;
using System;

namespace Drumstalotajs.Mapping.Layers;

public partial class Layer : TileMapLayer, ILayer<Vector2I>
{
	public Vector2I[] GetAtlas()
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
	
	public void AddTile(Vector2I position, Vector2I atlasCoords)
	{
		SetCell(position, 0, atlasCoords, 0);
	}
	
	public void RemoveTile(Vector2I position)
	{
		EraseCell(position);
	}
	
	
	/*
	new public async Task<SceneTile> AddTile(string name, Vector2I position)
	{
		int id = _sceneLayerSet.SceneTiles.FirstOrDefault(s => s.Name == name).Id;
		SetCell(position, 0, new Vector2I(0, 0), id);
		var data = await ToSignal(this, SignalName.SpawnedTile);
		return (SceneTile)data[0];
	}*/
	
}
