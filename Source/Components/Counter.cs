using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class Counter : Control
{
	[ExportGroup("Textures")]
	[Export] private Texture2D _icon = null;
	
	[ExportGroup("Internals")]
	[Export] private TextureRect _iconNode;
	[Export] private Label _count;
	
	public override void _Ready()
	{
		if (_icon != null) _iconNode.Texture = _icon;
	}
	
	public void SetCount(int count)
	{
		_count.Text = $"{count}";
	}
}
