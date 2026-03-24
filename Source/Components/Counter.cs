using Godot;
using System;

namespace drumstalotajs.Components;

public partial class Counter : Control
{
	[Export] private Texture2D Icon { get; set; }
	
	private TextureRect iconControl;
	private Label counter;
	
	public override void _Ready()
	{
		counter = GetNode<Label>("Counter");
		iconControl = GetNode<TextureRect>("Icon");
		iconControl.Texture = Icon;
		SetCounter(0);
	}
	
	public void SetCounter(int count)
	{
		counter.Text = $"{count}";
	}
}
