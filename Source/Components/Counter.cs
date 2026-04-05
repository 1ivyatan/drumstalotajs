using Godot;
using System;

namespace Drumstalotajs.Components;

public partial class Counter : Node
{
	[Export] private Texture2D Icon { get; set; }
	
	protected TextureRect _icon;
	protected Label _counter;
	
	public override void _Ready()
	{
		_icon = GetNode<TextureRect>("Icon");
		_counter = GetNode<Label>("Counter");
		_icon.Texture = Icon;
	}
	
	public void SetCount(int count)
	{
		_counter.Text = $"{count}";
	}
}
