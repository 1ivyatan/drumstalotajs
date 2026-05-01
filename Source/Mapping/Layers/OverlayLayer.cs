using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Layers;

public partial class OverlayLayer : SceneLayer
{
	new public Array<OverlayTile> Flash(Vector2I position)
	{
		var arr = base.Flash(position);
		return new Array<OverlayTile>(arr.Select(t => t as OverlayTile));
	}
	
	new public OverlayTile GetInstance(Vector2I position)
	{
		var tile = base.GetInstance(position);
		return tile as OverlayTile;
	}
	
	public async Task AddTile(OverlayLayerTileData atlas)
	{
		SetCell(atlas.Position, 0, Vector2I.Zero, atlas.Id);
		var nodes = await ToSignal(this, SignalName.TileSpawned);
		if (nodes.Length > 0 && nodes[0].VariantType == Variant.Type.Object)
		{
			var tile = (SceneTile)nodes[0];
			tile.TileId = atlas.Id;
			tile.Rotation = (float)atlas.Radians;
			tile.Data = atlas.Data;
		}
		EmitSignal(SignalName.ChangedLayer);
		GD.Print(2);
	}
	
	new public OverlayLayerData Export()
	{
		return new OverlayLayerData(this);
	}

	public async override Task Load(SceneLayerData layerData)
	{
		if (layerData is OverlayLayerData overlayLayerData)
		{
			foreach (OverlayLayerTileData tile in overlayLayerData.Tiles)
			{
				this.AddTile(tile);
			}
		}
	}
}
