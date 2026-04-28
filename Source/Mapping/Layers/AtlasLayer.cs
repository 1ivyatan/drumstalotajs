using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Mapping.Layers;

public partial class AtlasLayer : Layer<Vector2I, AtlasTile, AtlasLayerData>
{
	[Export] private Color[] extraColors = [];
	
	public override void _Ready()
	{
		PrepareColorAtlases();
	}
	
	protected void PrepareColorAtlases()
	{
		if (extraColors != null && extraColors.Length > 0 && TileSet != null)
		{
			int firstSourceId = TileSet.GetSourceId(0);
			foreach (var color in extraColors)
			{
				var newAtlas = Utilities.Layers.TintAtlasSource(this, firstSourceId, color);
				TileSet.AddSource(newAtlas);
			}
		}
	}
	
	public int[] GetAtlasIds()
	{
		if (TileSet == null) return [];
		int[] ids = new int[TileSet.GetSourceCount()];
		for (int i = 0; i < ids.Length; i++)
		{
			ids[i] = TileSet.GetSourceId(i);
		}
		return ids;
	}
	
	public Dictionary<int, Array<Vector2I>> GetAtlases()
	{
		if (TileSet == null) return null;
		Dictionary<int, Array<Vector2I>> data = new Dictionary<int, Array<Vector2I>>();
		for (int i = 0; i < TileSet.GetSourceCount(); i++)
		{
			int sourceId = TileSet.GetSourceId(i);
			var atlas = GetAtlas(i);
			if (atlas != null)
			{
				data[sourceId] = new Array<Vector2I>(atlas);
			}
		}
		return data;
	}
	
	public Vector2I[] GetAtlas(int idx)
	{
		if (TileSet != null)
		{
			TileSetAtlasSource source = TileSet.GetSource(idx) as TileSetAtlasSource;
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
	
	public override Vector2I[] GetAtlas()
	{
		if (TileSet != null)
		{
			int firstSourceId = TileSet.GetSourceId(0);
			return GetAtlas(firstSourceId);
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
	
	public void ChangeTileSource(Vector2I position, int id)
	{
		Vector2I atlasCoords = GetCellAtlasCoords(position);
		if (atlasCoords == Constants.Vector2I.Negative) return;
		int altId = GetCellAlternativeTile(position);
		Vector2I atlas = GetCellAtlasCoords(position);
		SetCell(position, id, atlas, altId);
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public void RotateTile(Vector2I position, double degrees)
	{
		Vector2I atlasCoords = GetCellAtlasCoords(position);
		if (atlasCoords == Constants.Vector2I.Negative) return;
		int sourceId = GetCellSourceId(position);
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
		
		SetCell(position, sourceId, atlasCoords, bitwise);
		EmitSignal(SignalName.ChangedLayer);
	}
	
	public AtlasTile GetTile(Vector2I position)
	{
		if (GetCellAtlasCoords(position) == Constants.Vector2I.Negative) return null;
		return new AtlasTile(this, position);
	}
	
	public override Godot.Collections.Array<AtlasTile> Flash(Vector2I position)
	{
		var tile = GetTile(position);
		return tile != null ? [tile] : [];
	}
	
	public override AtlasLayerData Export()
	{
		return new AtlasLayerData(this);
	}
	
	public async override Task Load(AtlasLayerData layerData)
	{
		Clear();
		SetPattern(layerData.Offset, layerData.Tiles);
		FixInvalidTiles();
	}
}
