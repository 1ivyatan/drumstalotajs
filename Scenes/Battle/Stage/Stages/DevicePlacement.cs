using Godot;
using System;

public partial class DevicePlacement : Stage
{	
	private Node2D map;
	private Selector selector;
	private EntityLayer entityLayer;
	private Callable entitySelectedCall;
	private Callable entityAppearanceCall;
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		
		this.entityAppearanceCall = new Callable(this, nameof(RefreshDevices));
		this.entitySelectedCall = new Callable(this, nameof(ClickedOnDevice));
		
		this.selector.Layer = Selector.SelectorLayer.Entity;
		this.selector.EntityTypeFilter = [Entity.EntityType.DeviceMarker, Entity.EntityType.Device];
		this.selector.SelectorMode = Selector.SelectorFilterMode.Fitlered;
		
		this.selector.Connect("EntitySelected", this.entitySelectedCall);
		this.entityLayer.Connect("EntitySpawned", this.entityAppearanceCall);
		this.entityLayer.Connect("EntityDestroyed", this.entityAppearanceCall);
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
	
	private void RefreshDevices(int entityType, Entity Entity)
	{
		if ((Entity.EntityType)entityType == Entity.EntityType.Device)
		{
			GD.Print(this.entityLayer.EntityCollections[Entity.EntityType.Device].Count);
		}
		//GD.Print();
	}
	
	public override void _ExitTree()
	{
		GD.Print("im being removed");
	}
}
