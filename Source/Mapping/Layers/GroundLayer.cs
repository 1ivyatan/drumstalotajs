using Godot;
using System;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Layers;

public partial class GroundLayer : AtlasLayer
{
	public AddedGroundHeightAtlas AddedHeights { get; private set; }
	public double BaseHeight { get; private set; } = 0;
	
	public override void _Ready()
	{
		AddedHeights = new AddedGroundHeightAtlas();
		PrepareColorAtlases();
	}
	
	public override Godot.Collections.Array<AtlasTile> Flash(Vector2I position)
	{
		if (GetCellAtlasCoords(position) == Constants.Vector2I.Negative) return [];
		return [ new GroundTile(this, position) ];
	}
	
	public override GroundLayerData Export()
	{
		return new GroundLayerData(this);
	}
	
	public async override Task Load(AtlasLayerData layerData)
	{
		if (layerData is GroundLayerData groundLayerData)
		{
			Clear();
			SetPattern(groundLayerData.Offset, groundLayerData.Tiles);
			BaseHeight = groundLayerData.BaseHeight;
			AddedHeights = groundLayerData.AddedHeights;
		}
	}
	
	public double GetAddedHeight(Vector2I position)
	{
		return AddedHeights.ContainsKey(position) ? AddedHeights[position] : 0;
	}
	
	public void SetAddedHeight(Vector2I position, double value)
	{
		AddedHeights[position] = value;
		EmitSignal(SignalName.ChangedLayer);
	}
}
