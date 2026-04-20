using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Mapping.Layers;

public partial class EntityLayer : SceneLayer
{
	new public Array<Entity> Flash(Vector2I position)
	{
		var arr = base.Flash(position);
		return new Array<Entity>(arr.Select(t => t as Entity));
	}
	
	new public Entity GetInstance(Vector2I position)
	{
		var tile = base.GetInstance(position);
		return tile as Entity;
	}
	
	public async void AddTile(EntityLayerTileData atlas)
	{
		SetCell(atlas.Position, 0, Vector2I.Zero, atlas.Id);
		var nodes = await ToSignal(this, SignalName.TileSpawned);
		if (nodes.Length > 0 && nodes[0].VariantType == Variant.Type.Object)
		{
			var tile = (Entity)nodes[0];
			tile.Azimuth = (float)atlas.Azimuth;
		}
		EmitSignal(SignalName.ChangedLayer);
	}
	
	new public EntityLayerData Export()
	{
		return new EntityLayerData(this);
	}
	
	public override void Load(SceneLayerData layerData)
	{
		if (layerData is EntityLayerData entityLayerData)
		{
			
		} else return;
	}
}
