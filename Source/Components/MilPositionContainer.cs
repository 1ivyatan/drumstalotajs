using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources.Mapping.Sets;
using Drumstalotajs.Resources.Mapping.Entities;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Components;

public partial class MilPositionContainer : Control
{
	[Export] private Label _posText;
	[Export] private Map _map;
	private bool _precise = false;
	
	public void UpdateCoords()
	{
		var coords = _map.ViewportToMilCoords();
		var str = _map.FormatMilCoords((Vector2I)coords);
		_posText.Text = str;
	}
}
