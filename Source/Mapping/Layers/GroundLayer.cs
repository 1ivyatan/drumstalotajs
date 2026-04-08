using Godot;
using System;
using System.Collections.Generic;

namespace Drumstalotajs.Mapping.Layers;

public partial class GroundLayer : Layer
{
	public double BaseHeight { get; private set; } = 0;
	public Dictionary<Vector2I, double> AddedHeights { get; private set; }
	
	public override void _Ready()
	{
		AddedHeights = new Dictionary<Vector2I, double>();
	}
	
	public void SetAddedHeight(Vector2I position, double value)
	{
		if (AddedHeights.ContainsKey(position))
		{
			AddedHeights[position] = value;
		} else
		{
			AddedHeights.Add(position, value);
		}
	}
	
	public double GetUnaddedHeight(Vector2I position)
	{
		return GetRelHeight(position) + BaseHeight;
	}
	
	public double GetRelHeight(Vector2I position)
	{
		var tileData = GetCellTileData(position);
		return (double)tileData.GetCustomData("RelativeHeight");
	}
	
	public double GetAddedHeight(Vector2I position)
	{
		if (AddedHeights.ContainsKey(position))
		{
			return AddedHeights[position];
		} else return 0;
	}
}
