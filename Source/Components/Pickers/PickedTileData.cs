using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

public partial class PickedTileData : Resource
{
	public LayerBase Layer { get; private set; }
	public string Name { get; private set; }
	public PickedTileData(LayerBase layer, string name)
	{
		Layer = layer;
		Name = name;
	}
}
