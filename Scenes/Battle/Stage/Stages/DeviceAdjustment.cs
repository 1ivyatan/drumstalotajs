using Godot;
using System;

public partial class DeviceAdjustment : Stage
{
	private Node2D map;
	private Selector selector;
	private EntityLayer entityLayer;
	private TopPanel topPanel;
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
	
		this.selector.Layer = Selector.SelectorLayer.Entity;
		this.selector.EntityTypeFilter = [Entity.EntityType.Device];
		this.selector.SelectorMode = Selector.SelectorFilterMode.Fitlered;
	
		this.topPanel.SetTopbarLabel("Ierīču koriģēšana");
	}
}
