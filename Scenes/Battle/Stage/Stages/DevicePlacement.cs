using Godot;
using System;

public partial class DevicePlacement : Stage
{	
	private Node2D map;
	private Selector selector;
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		
		this.selector.Layer = Selector.SelectorLayer.Entity;
		this.selector.EntityTypeFilter = [Entity.EntityType.DeviceMarker, Entity.EntityType.Device];
		this.selector.SelectorMode = Selector.SelectorFilterMode.Fitlered;
	}
	
	public override void _ExitTree()
	{
		GD.Print("im being removed");
	}
}
