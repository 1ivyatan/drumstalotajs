using Godot;
using System;
using System.Collections;

public partial class Fade : Control
{
	CanvasModulate tint;
	
	public override void _Ready()
	{
		tint = GetNode<CanvasModulate>("Tint");
	}
	
	public void FadeIn(float Delay) {
		
	}
	
	public void FadeOut(float Delay) {
		GD.Print("test");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
