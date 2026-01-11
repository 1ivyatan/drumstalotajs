using Godot;
using System;

public partial class DeviceFiring : Control
{
	private Node2D map;
	private Selector selector;
	private EntityLayer entityLayer;
	
	private TopPanel topPanel;
	
	/* to be removed!!!!!!!! */
	private Timer tempFakeTimer;
	/* ^^^^^^^^^^^^^^^^^^^^^^^^ */
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.topPanel.SetTopbarLabel("Ierīču šaušana");
		
		/* !!!!!!!!!!!!! */
		this.tempFakeTimer = this.GetNode<Timer>("TempFakeTimer");
		this.tempFakeTimer.Connect("timeout", new Callable(this, nameof(OnTimeOut)));
		this.tempFakeTimer.Start();
		/* !!!!!!!!!!!!!! */
	}
	
	private void ToDeviceAdjustment()
	{
		(GetNode("../../../") as Battle).StartDeviceAdjustment();
	}
	
	private void OnTimeOut()
	{
		ToDeviceAdjustment();
	}
}
