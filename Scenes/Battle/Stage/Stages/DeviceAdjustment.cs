using Godot;
using System;

public partial class DeviceAdjustment : Stage
{
	private Node2D map;
	private Selector selector;
	private EntityLayer entityLayer;
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
	
	}
}
