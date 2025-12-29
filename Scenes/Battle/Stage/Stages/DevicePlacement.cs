using Godot;
using System;

public partial class DevicePlacement : Stage
{	
	private Node2D map;
	private Selector selector;
	private EntityLayer entityLayer;
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		
		this.selector.Layer = Selector.SelectorLayer.Entity;
		this.selector.EntityTypeFilter = [Entity.EntityType.DeviceMarker, Entity.EntityType.Device];
		this.selector.SelectorMode = Selector.SelectorFilterMode.Fitlered;
		
		this.selector.Connect("EntitySelected", new Callable(this, nameof(ClickedOnDevice)));
	}
	
	private void ClickedOnDevice(int entityType, Vector2I position)
	{
		switch ((Entity.EntityType)entityType)
		{
			case Entity.EntityType.Device:
				this.entityLayer.InsertEntity(Entity.EntityType.DeviceMarker, position);
				break;
			case Entity.EntityType.DeviceMarker:
				this.entityLayer.InsertEntity(Entity.EntityType.Device, position);
				break;
			default:
				break;
		}
	}
	
	public override void _ExitTree()
	{
		GD.Print("im being removed");
	}
}
