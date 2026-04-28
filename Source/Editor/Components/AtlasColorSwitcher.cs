using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Editor.Components;

public partial class AtlasColorSwitcher : Control
{
	[Signal] public delegate void ClickedColorEventHandler(int id);
	
	public void Load(AtlasLayer layer)
	{
		foreach (var i in layer.GetAtlasIds())
		{
			var button = new Button();
			button.Text = $"{i}";
			button.Pressed += () => { 
				EmitSignal(SignalName.ClickedColor, i);
			};
			AddChild(button);
		}
	}
}
