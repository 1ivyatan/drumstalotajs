using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public partial class OverlayLayer : SceneLayer
{
	new public Array<OverlayTile> Flash(Vector2I position)
	{
		var arr = base.Flash(position);
		return new Array<OverlayTile>(arr.Select(t => t as OverlayTile));
	}
	
	public async void AddTile(OverlayLayerTileData atlas)
	{
		SetCell(atlas.Position, 0, Vector2I.Zero, atlas.Id);
		var nodes = await ToSignal(this, SignalName.TileSpawned);
		if (nodes.Length > 0 && nodes[0].VariantType == Variant.Type.Object)
		{
			var tile = (SceneTile)nodes[0];
			tile.Rotation = (float)atlas.Radians;
		}
		EmitSignal(SignalName.ChangedLayer);
	}
	
	new public OverlayLayerData Export()
	{
		return new OverlayLayerData(this);
	}
	
	public override void Load(SceneLayerData layerData)
	{
		if (layerData is OverlayLayerData entityLayerData)
		{
			
		} else return;
	}
}
