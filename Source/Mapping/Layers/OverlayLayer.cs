using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Layers;

namespace Drumstalotajs.Mapping.Layers;

public partial class OverlayLayer : SceneLayer
{
	new protected List<OverlayLayerTileData> _newTileQueue = new();
	
	new public Array<OverlayTile> Flash(Vector2I position)
	{
		var arr = base.Flash(position);
		return new Array<OverlayTile>(arr.Select(t => t as OverlayTile));
	}
	
	new public OverlayTile GetInstance(Vector2I position)
	{
		var tile = base.GetInstance(position);
		return tile != null ? tile as OverlayTile : null;
	}
	
	protected override void TileSpawnedAction(Node node)
	{
		if (node is OverlayTile overlayTile)
		{
			var pos = LocalToMap(overlayTile.Position);
			var atlas = _newTileQueue.FirstOrDefault(t => t.Position == pos);
			if (atlas != null && atlas is OverlayLayerTileData overlayAtlas)
			{
				overlayTile.TileId = overlayAtlas.Id;
				overlayTile.Data = overlayAtlas.Data;
				overlayTile.Rotation = (float)overlayAtlas.Radians;
				
				if (!Instances.Contains(overlayTile)) Instances.Add(overlayTile);
				_newTileQueue.Remove(atlas);
				EmitSignal(SignalName.TileSpawned, overlayTile);
				EmitSignal(SignalName.ChangedLayer);
			}
		}
	}
	
	public async override Task AddTile(Vector2I position, string atlas)
	{
		OverlayLayerTileData data = new OverlayLayerTileData();
		int id = Atlas.FirstOrDefault(a => a.Name == atlas).Id;
		data.Position = position;
		data.Id = id;
		await AddTile(data);
	}
	
	public async Task AddTile(OverlayLayerTileData atlas)
	{
		_newTileQueue.Add(atlas);
		SetCell(atlas.Position, 0, Vector2I.Zero, atlas.Id);
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
				if (tile != null)
				{
					await AddTile(tile);
				}
			}
		}
	}
}
