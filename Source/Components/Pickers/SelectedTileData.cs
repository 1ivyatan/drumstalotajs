using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

public partial class SelectedTileData : Resource
{
	public Layer Layer { get; private set; }
	public string Name { get; private set; }
	public SelectedTileData(Layer layer, string name)
	{
		Layer = layer;
		Name = name;
	}
}
