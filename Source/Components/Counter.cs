using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class Counter : Control
{
	[ExportGroup("Counting")]
	[Export] public int Value { get; 
		set
		{
			field = value;
			_count.Text = $"{field}";
		}
	} = 0;
	
	[ExportGroup("Textures")]
	[Export] private Texture2D _icon = null;
	
	[ExportGroup("Internals")]
	[Export] private TextureRect _iconNode;
	[Export] private Label _count;
	
	public override void _Ready()
	{
		if (_icon != null) _iconNode.Texture = _icon;
	}
	
	public void SetText(string text)
	{
		_count.Text = text;
	}
}
