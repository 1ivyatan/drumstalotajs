using Godot;
using System;

public abstract partial class Entity : Node2D
{
	[Signal]
	public delegate void EntityLoadedEventHandler(Node2D instance, int entityTypeId);
	
	protected abstract EntityType entityType { get; }
	
	public override void _Ready()
	{
		/* turns out scene tiles spawn right under tilemaplayer */
		Connect("EntityLoaded", new Callable(GetParent(), "OnEntitySpawn"));
		EmitSignal(SignalName.EntityLoaded, this, (int)entityType);
	}
}
