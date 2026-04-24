using Godot;
using System;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Layers;

public partial class AtlasLayer : Layer<Vector2I, AtlasTile, AtlasLayerData>
{
	public override Vector2I[] GetAtlas()
	{
		if (TileSet != null)
		{
			int firstSourceId = TileSet.GetSourceId(0);
			TileSetAtlasSource source = TileSet.GetSource(firstSourceId) as TileSetAtlasSource;
			if (source != null)
			{
				int count = source.GetTilesCount();
				Vector2I[] tileAtlas = new Vector2I[count];
				for (int i = 0; i < count; i++)
				{
					Vector2I coords = source.GetTileId(i);
					tileAtlas[i] = coords;
				}
				return tileAtlas;
			}
		}
		return [];
	}
	
	public override void AddTile(Vector2I position, Vector2I atlas)
	{
		SetCell(position, 0, atlas, 0);
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public override void RemoveTile(Vector2I position)
	{
		EraseCell(position);
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public void RotateTile(Vector2I position, double degrees)
	{
		Vector2I atlasCoords = GetCellAtlasCoords(position);
		if (atlasCoords == Constants.Vector2I.Negative) return;
		int bitwise = 0;
		int quadrant = Calculations.GetQuadrant(degrees);
		
		switch (quadrant)
		{
			case 1: bitwise = 0; break;
			case 2: bitwise = (int)(TileSetAtlasSource.TransformTranspose | 
			TileSetAtlasSource.TransformFlipH); break;
			case 3: bitwise = (int)(TileSetAtlasSource.TransformFlipH | 
			TileSetAtlasSource.TransformFlipV); break;
			case 4: bitwise = (int)(TileSetAtlasSource.TransformTranspose | 
			TileSetAtlasSource.TransformFlipV); break;
			default: break;
		}
		
		SetCell(position, 0, atlasCoords, bitwise);
	}
	
	public override Godot.Collections.Array<AtlasTile> Flash(Vector2I position)
	{
		if (GetCellAtlasCoords(position) == Constants.Vector2I.Negative) return [];
		return [ new AtlasTile(this, position) ];
	}
	
	public override AtlasLayerData Export()
	{
		return new AtlasLayerData(this);
	}
	
	public async override Task Load(AtlasLayerData layerData)
	{
		Clear();
		SetPattern(layerData.Offset, layerData.Tiles);
	}
}
