using Godot;
using System;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Mapping.Layers;

public partial class GroundTile : AtlasTile
{
	public double _baseHeight;
	
	public GroundTile(GroundLayer layer, Vector2I position) : base(layer, position)
	{
		_baseHeight = layer.BaseHeight;
	}
	
	public double GetRelativeHeight()
	{
		return (double)TileData.GetCustomData("RelativeHeight");
	}
	
	public double GetBaseRelativeHeight()
	{
		return _baseHeight + GetRelativeHeight();
	}
}
