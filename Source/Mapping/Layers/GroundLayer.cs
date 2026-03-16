using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Layers;

public partial class GroundLayer : Layer
{
	private Dictionary<Vector2I, double> relativeHeights;
	
	public override void _Ready()
	{
		relativeHeights = new Dictionary<Vector2I, double>();
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
