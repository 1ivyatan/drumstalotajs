using Godot;
using System;

public partial class Entity : Node2D
{
	public override void _Ready()
	{
		GD.Print("Hi! I am instance " + this);
	}
}
