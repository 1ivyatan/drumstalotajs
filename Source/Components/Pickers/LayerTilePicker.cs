using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Components.Pickers;

public partial class LayerTilePicker : ItemList
{
	[Signal] public delegate void SelectedTileEventHandler(PickedTileData pickedTile);
	
	public LayerBase Layer { get; private set; }
	
	public override void _Ready()
	{
		ItemSelected += (long index) => {
			string name = GetItemText((int)index);
			EmitSignal(SignalName.SelectedTile, new PickedTileData(Layer, name));
		};
	}
	
	public void Initialize(LayerBase layer)
	{
		if (layer is AtlasLayer)
		{
			foreach (var tile in (layer as AtlasLayer).GetAtlas())
			{
				AddItem(tile.ToString());
			}
		}
		Layer = layer;
	}
}
