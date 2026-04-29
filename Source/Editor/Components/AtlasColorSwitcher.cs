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
		int count = 0;
		foreach (var i in layer.GetAtlasIds())
		{
			var button = new Button();
			button.Text = $"{i}";
			if (count > 0)
			{
				button.AddThemeColorOverride("font_color", layer.ExtraColors[count - 1]);
			}
			button.Pressed += () => {
				EmitSignal(SignalName.ClickedColor, i);
			};
			AddChild(button);
			count++;
		}
	}
}
