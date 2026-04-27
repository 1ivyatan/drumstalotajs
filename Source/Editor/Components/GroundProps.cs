using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor.Components;

public partial class GroundProps : Props
{
	[Export] private Map _map;
	[Export] private Label _baseRelHeight;
	[Export] private SpinBox _addedHeightSpinner;
	
	public override void Load(Vector2I position)
	{
		Visible = true;
	}
}
