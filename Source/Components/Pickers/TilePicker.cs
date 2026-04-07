using Godot;
using System;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Components.Pickers;

public partial class TilePicker : ItemList
{
	[Signal] public delegate void SelectedTileEventHandler(SelectedTileData selectedTile);
	
	private Layer Layer { get; set; }
	
	public override void _Ready()
	{
		ItemSelected += (long index) => {
			string name = GetItemText((int)index);
			EmitSignal(SignalName.SelectedTile, new SelectedTileData(Layer, name));
		};
	}
	
	public void SetLayer(Layer layer)
	{
		Layer = layer;
	}
}
