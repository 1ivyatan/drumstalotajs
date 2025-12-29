using Godot;
using System;

public partial class DevicePlacement : Stage
{	
	private Node2D map;
	private Selector selector;
	private EntityLayer entityLayer;
	
	private Callable entitySelectedCall;
	private Callable entityAppearanceCall;
	private Callable toDeviceAdjustmentStageCall;
	
	private TopPanel topPanel;
	private Button startButton;
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		
		this.entityAppearanceCall = new Callable(this, nameof(PrepareToAdjustmentButton));
		this.entitySelectedCall = new Callable(this, nameof(ClickedOnDevice));
		this.toDeviceAdjustmentStageCall = new Callable(this, nameof(ToAdjustmentStage));
		
		this.selector.Layer = Selector.SelectorLayer.Entity;
		this.selector.EntityTypeFilter = [Entity.EntityType.DeviceMarker, Entity.EntityType.Device];
		this.selector.SelectorMode = Selector.SelectorFilterMode.Fitlered;
		
		this.startButton = this.GetNode<Button>("StartButton");
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.topPanel.SetTopbarLabel("Ierīču ievietošana");
		
		this.selector.Connect("EntitySelected", this.entitySelectedCall);
		this.entityLayer.Connect("EntitySpawned", this.entityAppearanceCall);
		this.entityLayer.Connect("EntityDestroyed", this.entityAppearanceCall);
		this.startButton.Connect("pressed", this.toDeviceAdjustmentStageCall);
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
	
	private bool HasEnoughDevices()
	{
		long count = this.entityLayer.EntityCollections[Entity.EntityType.Device].Count;
		
		return count > 0;
	}
	
	private void ToAdjustmentStage()
	{
		if (this.HasEnoughDevices())
		{
			(GetNode("../../../") as Battle).StartDeviceAdjustment();
		}
	}
	
	private void PrepareToAdjustmentButton(int entityType, Entity Entity)
	{
		if ((Entity.EntityType)entityType == Entity.EntityType.Device)
		{
			this.startButton.Disabled = !this.HasEnoughDevices();
		}
	}
	
	public override void _ExitTree()
	{
		this.selector.Disconnect("EntitySelected", this.entitySelectedCall);
		this.entityLayer.Disconnect("EntitySpawned", this.entityAppearanceCall);
		this.entityLayer.Disconnect("EntityDestroyed", this.entityAppearanceCall);
		this.startButton.Disconnect("pressed", this.toDeviceAdjustmentStageCall);
	}
}
