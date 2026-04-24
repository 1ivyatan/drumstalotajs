using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Resources.Mapping.Layers;

[GlobalClass]
public partial class SceneLayerTileData : Resource
{
	[Export] public int Id { get; set; }
	[Export] public Vector2I Position { get; set; }
	[Export] public Godot.Collections.Dictionary Data { get; set; }
	public SceneLayerTileData() {}
	public SceneLayerTileData(SceneLayer layer, SceneTile tile)
	{
		Id = tile.TileId;
		Position = layer.LocalToMap(tile.Position);
		Data = tile.Data;
	}
}
