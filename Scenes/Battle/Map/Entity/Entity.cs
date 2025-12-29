using Godot;
using System;

public partial class Entity : Node2D
{
	public enum EntityType
	{
		None = -1,
		DeviceMarker = 0,
		Device = 1
	}
	
	[Signal]
	public delegate void EntitySpawnedEventHandler(Node2D instance);
	
	[Signal]
	public delegate void EntityDestroyedEventHandler(Node2D instance);
	
	private Node parent;
	private Callable spawnCall;
	private Callable destroyCall;
	
	public override void _Ready()
	{
		this.parent = GetParent();
		if (this.parent != null)
		{
			this.spawnCall = new Callable(this.parent, "_EntitySpawned");
			this.destroyCall = new Callable(this.parent, "_EntityDestroyed");
			
			this.Connect("EntitySpawned", this.spawnCall);
			this.Connect("EntityDestroyed", this.destroyCall);
			
			this.EmitSignal(SignalName.EntitySpawned, this);
		}
		
	}
	
	public override void _ExitTree()
	{
		if (this.parent != null)
		{
			this.EmitSignal(SignalName.EntitySpawned, this);
			this.Disconnect("EntitySpawned", this.spawnCall);
			this.Disconnect("EntityDestroyed", this.destroyCall);
		}
	}
}
