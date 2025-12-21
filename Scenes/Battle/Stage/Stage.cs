using Godot;
using System;

public abstract partial class Stage : Node2D
{
	public abstract void Input(InputEvent @event);
}
