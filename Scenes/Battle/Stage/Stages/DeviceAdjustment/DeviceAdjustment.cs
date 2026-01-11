using Godot;
using System;

public partial class DeviceAdjustment : Stage
{
	private Node2D map;
	private BattleButton battleButton;
	private Selector selector;
	private EntityLayer entityLayer;
	private TopPanel topPanel;
	private DeviceAdjustmentPanel deviceAdjustmentPanel;
	
	private Callable entitySelectedCall;
	private Callable battleStageCall;
	
	private Device selectedDevice;
	private int deviceRotationDirectionSign;
	
	private void ToBattleStage()
	{
		
		(GetNode("../../../") as Battle).StartBattle();
	}
	
	private void ClickedOnEntity(int entityType, Vector2I position)
	{
		switch ((Entity.EntityType)entityType)
		{
			case Entity.EntityType.None:
				deviceAdjustmentPanel.HideDeviceInfo();
				
				this.selectedDevice = null;
				
				break;
			case Entity.EntityType.Device:
				Device device = (Device)this.entityLayer.EntityCollections[(Entity.EntityType)entityType].Instances[position];

				this.selectedDevice = device;

				deviceAdjustmentPanel.ShowDeviceInfo(device);
				break;
			default:
				break;
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (deviceRotationDirectionSign != 0 && selectedDevice != null)
		{
			selectedDevice.Azimuth += deviceRotationDirectionSign;
			deviceAdjustmentPanel.ShowDeviceInfo(selectedDevice);
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("device_adjustment_clockwise") && this.selectedDevice != null)
		{
			this.deviceRotationDirectionSign = 1;
		}
		
		if (@event.IsActionPressed("device_adjustment_counterclockwise") && this.selectedDevice != null)
		{
			this.deviceRotationDirectionSign = -1;
		}
		
		if ((@event.IsActionReleased("device_adjustment_clockwise") || @event.IsActionReleased("device_adjustment_counterclockwise")) && this.selectedDevice != null)
		{
			this.deviceRotationDirectionSign = 0;
		}
	}
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.deviceAdjustmentPanel = this.GetNode("DeviceAdjustmentPanel") as DeviceAdjustmentPanel;
		this.battleButton = this.GetNode("BattleButton") as BattleButton; 
		
		this.selector.Layer = Selector.SelectorLayer.All;
		this.selector.EntityTypeFilter = null;
		this.selector.SelectorMode = Selector.SelectorFilterMode.All;
		
		this.entitySelectedCall = new Callable(this, nameof(ClickedOnEntity));
		this.battleStageCall = new Callable(this, nameof(ToBattleStage));
		
		if (this.entityLayer.EntityCollections[Entity.EntityType.DeviceMarker].Count > 0)
		{
			this.entityLayer.EraseEntitiesByType(Entity.EntityType.DeviceMarker);	
		}
		
		this.topPanel.SetTopbarLabel("Ierīču koriģēšana");
		
		this.deviceRotationDirectionSign = 0;
		this.selectedDevice = null;
				
		this.selector.Connect("EntitySelected", this.entitySelectedCall);
		this.battleButton.Connect("pressed", this.battleStageCall);
	}
	
	public override void _ExitTree()
	{
		this.selector.Disconnect("EntitySelected", this.entitySelectedCall);
		this.battleButton.Disconnect("pressed", this.battleStageCall);
	}
}
