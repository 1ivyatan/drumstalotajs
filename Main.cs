using Godot;
using System;

public partial class Main : Node
{
	Node currentScene;
	
	public override void _Ready()
	{
		currentScene = GetNode<Node>("Main/Scene");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
