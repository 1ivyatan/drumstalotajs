using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components;

public partial class TilePicker : ItemList
{
	[Signal] public delegate void SelectedTileEventHandler(SceneLayer layer, string name);
	
	private SceneLayer Layer { get; set; }
	
	public override void _Ready()
	{
		ItemSelected += (long index) => {
			string name = GetItemText((int)index);
			EmitSignal(SignalName.SelectedTile, Layer, name);
		};
	}
	
	public void SetLayer(SceneLayer layer)
	{
		Layer = layer;
	}
}
