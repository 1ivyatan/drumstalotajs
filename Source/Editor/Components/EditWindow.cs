using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Tiling;

namespace Drumstalotajs.Editor.Components;

public partial class EditWindow : Window
{
	[Export] private Map _map;
	[Export] private Label _emptyText;
	[Export] private TileEditor _tileEditor;
	public int PickedSourceId = 0;
	
	public override void _Ready()
	{
		_tileEditor.GroundProps.ColorContainer.ClickedColor += (int id) => {
			PickedSourceId = id;
		};
		_tileEditor.DecorationProps.ColorContainer.ClickedColor += (int id) => {
			PickedSourceId = id;
		};
	}
	
	public void GetTile(Vector2I coords)
	{
		if (_map.IsEmpty(coords))
		{
			DeselectTile();
			return;
		}
		
		_emptyText.Visible = false;
		_tileEditor.Load(coords);
	}
	
	public void DeselectTile()
	{
		_tileEditor.Close();
		_emptyText.Visible = true;
	}
}
