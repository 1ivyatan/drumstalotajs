using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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

		this.selector.EntityTypeFilter = [];
		this.selector.SelectorMode = Selector.SelectorFilterMode.None;
		
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.topPanel.SetTopbarLabel("Ierīču šaušana");
		
		this.StartFiringSequence();
	}
	
	private void StartFiringSequence()
	{
		Dictionary<Vector2I, Entity> deviceEntities = this.entityLayer.EntityCollections[Entity.EntityType.Device].Instances;
		
		foreach (KeyValuePair<Vector2I, Entity> entity in deviceEntities)
		{
			Device device = (Device)entity.Value;
			device.Fire();
		}
		
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
