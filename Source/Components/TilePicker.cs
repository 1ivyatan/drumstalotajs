using Godot;
using System;

namespace Drumstalotajs.Components;

public partial class TilePicker : ItemList
{
	[Signal] public delegate void SelectedTileEventHandler(string name);
	
	public override void _Ready()
	{
		ItemSelected += (long index) => {
			string name = GetItemText((int)index);
			EmitSignal(SignalName.SelectedTile, name);
		};
	}
}
