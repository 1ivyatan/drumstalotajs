using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class AtlasLayerData : LayerData
{
	[Export] public Vector2I Offset { get; set; }
	[Export] public TileMapPattern Tiles { get; set; }
	
	public AtlasLayerData() {}
	public AtlasLayerData(AtlasLayer layer)
	{
		Rect2I usedRect = layer.GetUsedRect();
		Array<Vector2I> allCells = new Array<Vector2I>();
		
		for (int y = usedRect.Position.Y; y < usedRect.Position.Y + usedRect.Size.Y; y++)
		{
			for (int x = usedRect.Position.X; x < usedRect.Position.X + usedRect.Size.X; x++)
			{
				allCells.Add(new Vector2I(x, y));
			}
		}
		
		TileMapPattern tiles = layer.GetPattern(allCells);
		Tiles = tiles;
		Offset = usedRect.Position;
	}
}
