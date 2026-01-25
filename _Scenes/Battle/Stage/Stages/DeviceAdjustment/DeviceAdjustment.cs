using Godot;
using System;

public partial class DeviceAdjustment : Stage
{
	private Node2D map;
	private DeviceFiringButton deviceFiringButton;
	private Selector selector;
	private EntityLayer entityLayer;
	private TopPanel topPanel;
	private DeviceAdjustmentPanel deviceAdjustmentPanel;
	
	private Callable entitySelectedCall;
	private Callable deviceFiringCall;
	
	private Device selectedDevice;
	private int deviceAzimuthDirectionSign;
	private int deviceAngleDirectionSign;
	
	private void ToDeviceFiringStage()
	{
		(GetNode("../../../") as Battle).StartDeviceFiring();
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
		if (selectedDevice != null)
		{
			if (this.deviceAzimuthDirectionSign != 0)
			{
				selectedDevice.Azimuth += this.deviceAzimuthDirectionSign;
				this.deviceAdjustmentPanel.ShowDeviceInfo(selectedDevice);
			}
			
			if (this.deviceAngleDirectionSign != 0)
			{
				selectedDevice.Angle += this.deviceAngleDirectionSign;
				this.deviceAdjustmentPanel.ShowDeviceInfo(selectedDevice);
			}
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (this.selectedDevice != null)
		{
			/* Azimuth */
			if (@event.IsActionPressed("device_adjustment_clockwise"))
			{
				this.deviceAzimuthDirectionSign = 1;
			}
			
			if (@event.IsActionPressed("device_adjustment_counterclockwise"))
			{
				this.deviceAzimuthDirectionSign = -1;
			}
			
			if ((@event.IsActionReleased("device_adjustment_clockwise") || @event.IsActionReleased("device_adjustment_counterclockwise")))
			{
				this.deviceAzimuthDirectionSign = 0;
			}
			
			/* Angle */
			if (@event.IsActionPressed("device_adjustment_raise"))
			{
				this.deviceAngleDirectionSign = 1;
			}
			
			if (@event.IsActionPressed("device_adjustment_lower"))
			{
				this.deviceAngleDirectionSign = -1;
			}
			
			if ((@event.IsActionReleased("device_adjustment_raise") || @event.IsActionReleased("device_adjustment_lower")))
			{
				this.deviceAngleDirectionSign = 0;
			}
		}
	}
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.deviceAdjustmentPanel = this.GetNode("DeviceAdjustmentPanel") as DeviceAdjustmentPanel;
		this.deviceFiringButton = this.GetNode("DeviceFiringButton") as DeviceFiringButton; 
		
		this.selector.Layer = Selector.SelectorLayer.All;
		this.selector.EntityTypeFilter = null;
		this.selector.SelectorMode = Selector.SelectorFilterMode.All;
		
		this.entitySelectedCall = new Callable(this, nameof(ClickedOnEntity));
		this.deviceFiringCall = new Callable(this, nameof(ToDeviceFiringStage));
		
		if (this.entityLayer.EntityCollections[Entity.EntityType.DeviceMarker].Count > 0)
		{
			this.entityLayer.EraseEntitiesByType(Entity.EntityType.DeviceMarker);	
		}
		
		this.topPanel.SetTopbarLabel("Ierīču koriģēšana");
		
		this.deviceAzimuthDirectionSign = 0;
		this.selectedDevice = null;
				
		this.selector.Connect("EntitySelected", this.entitySelectedCall);
		this.deviceFiringButton.Connect("pressed", this.deviceFiringCall);
	}
	
	public override void _ExitTree()
	{
		this.selector.Disconnect("EntitySelected", this.entitySelectedCall);
		this.deviceFiringButton.Disconnect("pressed", this.deviceFiringCall);
	}
}
