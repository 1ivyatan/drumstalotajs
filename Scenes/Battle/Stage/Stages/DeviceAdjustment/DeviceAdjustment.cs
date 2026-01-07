using Godot;
using System;

public partial class DeviceAdjustment : Stage
{
	private Node2D map;
	private Selector selector;
	private EntityLayer entityLayer;
	private TopPanel topPanel;
	
	private Callable entitySelectedCall;
	
	private DeviceAdjustmentPanel deviceAdjustmentPanel;
	
	private void ClickedOnEntity(int entityType, Vector2I position)
	{
		switch ((Entity.EntityType)entityType)
		{
			case Entity.EntityType.None:
				deviceAdjustmentPanel.HideDeviceInfo();
				break;
			case Entity.EntityType.Device:
				Entity entity = this.entityLayer.EntityCollections[(Entity.EntityType)entityType].Instances[position];
				deviceAdjustmentPanel.ShowDeviceInfo((Device)entity, position);
				break;
			default:
				break;
		}
	}
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.deviceAdjustmentPanel = this.GetNode("DeviceAdjustmentPanel") as DeviceAdjustmentPanel;
	
		this.selector.Layer = Selector.SelectorLayer.All;
		this.selector.EntityTypeFilter = null;
		this.selector.SelectorMode = Selector.SelectorFilterMode.All;
		
		this.entitySelectedCall = new Callable(this, nameof(ClickedOnEntity));
		
		if (this.entityLayer.EntityCollections[Entity.EntityType.DeviceMarker].Count > 0)
		{
			this.entityLayer.EraseEntitiesByType(Entity.EntityType.DeviceMarker);	
		}
		
		this.topPanel.SetTopbarLabel("Ierīču koriģēšana");
		
		this.selector.Connect("EntitySelected", this.entitySelectedCall);
	}
	
	public override void _ExitTree()
	{
		this.selector.Disconnect("EntitySelected", this.entitySelectedCall);
	}
}
