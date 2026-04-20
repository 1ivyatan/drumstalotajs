using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Tiles;

public partial class GroundTile : AtlasTile
{
	private double _baseHeight;
	private double _relHeight;
	private GroundLayer _layer;
	
	public GroundTile(GroundLayer layer, Vector2I position) : base(layer, position)
	{
		_baseHeight = layer.BaseHeight;
		_relHeight = (double)TileData.GetCustomData("RelativeHeight");
	}
	
	public double GetBaseRelativeHeight()
	{
		return _baseHeight + _relHeight;
	}
	
	public double GetFullHeight()
	{
		return _baseHeight + _relHeight + _layer.GetAddedHeight(CellPosition);
	}
}
