using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

public partial class SelectedTileData : Resource
{
	public SceneLayer Layer { get; private set; }
	public string Name { get; private set; }
	public SelectedTileData(SceneLayer layer, string name)
	{
		Layer = layer;
		Name = name;
	}
}
