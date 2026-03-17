using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Layers;

public partial class DecorationLayer : Layer
{
	
	public override void _Ready()
	{
		
	}
	
	public Resources.Maps.Layers.DecorationLayer ExportTiles()
	{
		Rect2I usedRect = GetUsedRect();
		Array<Vector2I> allCells = new Array<Vector2I>();
		for (int y = usedRect.Position.Y; y < usedRect.Position.Y + usedRect.Size.Y; y++)
		{
			for (int x = usedRect.Position.X; x < usedRect.Position.X + usedRect.Size.X; x++)
			{
				allCells.Add(new Vector2I(x, y));
			}
		}
		
		TileMapPattern tiles = GetPattern(allCells);
		Resources.Maps.Layers.DecorationLayer layer = new Resources.Maps.Layers.DecorationLayer();
		layer.Tiles = tiles;
		layer.Offset = usedRect.Position;
		
		return layer;
	}
	
	public void LoadLayer(Resources.Maps.Layers.DecorationLayer decorationLayer)
	{
		SetPattern(decorationLayer.Offset, decorationLayer.Tiles);
	}
}
