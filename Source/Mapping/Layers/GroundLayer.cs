using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Layers;

public partial class GroundLayer : Layer
{
	private Godot.Collections.Dictionary<Vector2I, double> relativeHeights;
	
	public override void _Ready()
	{
		relativeHeights = new Godot.Collections.Dictionary<Vector2I, double>();
	}
	
	public Resources.Maps.Layers.GroundLayer ExportTiles()
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
		Resources.Maps.Layers.GroundLayer layer = new Resources.Maps.Layers.GroundLayer();
		layer.Tiles = tiles;
		layer.Offset = usedRect.Position;
		layer.RelativeHeights = relativeHeights;
		
		GD.Print(layer.Offset);
		GD.Print(layer.Offset * TileSize);
		
		return layer;
	}
	
	public void SetRelHeight(Vector2I cellPos, double value)
	{
		if (!relativeHeights.ContainsKey(cellPos))
		{
			double defaultHeight = (double)GetCellTileData(cellPos).GetCustomData("DefaultRelHeight");
			relativeHeights.Add(cellPos, defaultHeight);
		}
		
		relativeHeights[cellPos] = Math.Round(value, 3);
	}
	
	public double GetRelHeight(Vector2I cellPos)
	{
		return ((relativeHeights.ContainsKey(cellPos)) ? relativeHeights[cellPos] : 0);
	}
	
	public double GetHeight(Vector2I cellPos)
	{
		double defaultHeight = (double)GetCellTileData(cellPos).GetCustomData("DefaultRelHeight");
		return defaultHeight + ((relativeHeights.ContainsKey(cellPos)) ? relativeHeights[cellPos] : 0);
	}
}
