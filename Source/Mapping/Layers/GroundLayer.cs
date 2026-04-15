using Godot;
using System;
using Drumstalotajs.Utils;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public partial class GroundLayer : AtlasLayer
{	
	public AddedGroundHeightAtlas AddedHeights { get; private set; }
	public double BaseHeight { get; private set; } = 0;
	
	public override void _Ready()
	{
		AddedHeights = new AddedGroundHeightAtlas();
	}
	
	public override Godot.Collections.Array<AtlasTile> Flash(Vector2I position)
	{
		if (GetCellAtlasCoords(position) == Types.Vector2I.Negative) return [];
		return [ new GroundTile(this, position) ];
	}
	
	public override GroundLayerData Export()
	{
		return new GroundLayerData(this);
	}
	
	public double GetBaseHeight()
	{
		return BaseHeight;
	}
	
	public double GetAddedHeight(Vector2I position)
	{
		return AddedHeights.ContainsKey(position) ? AddedHeights[position] : 0;
	}
	
	public void SetAddedHeight(Vector2I position, double value)
	{
		AddedHeights[position] = value;
		EmitSignal(SignalName.Changed);
	}
}
