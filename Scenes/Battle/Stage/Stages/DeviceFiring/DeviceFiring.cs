using Godot;
using System;

public partial class DeviceFiring : Control
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
		
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.topPanel.SetTopbarLabel("Ierīču šaušana");
	}
}
