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
	
	private void ClickedOnDevice(int entityType, Vector2I position)
	{
		Entity entity = this.entityLayer.EntityCollections[(Entity.EntityType)entityType].Instances[position];
		deviceAdjustmentPanel.ShowEntityInfo(entity);
	}
	
	public override void _Ready()
	{
		this.map = this.GetNode<Node2D>("../../MapContainer/Map");
		this.selector = this.map.GetNode<Node2D>("Selector") as Selector;
		this.entityLayer = this.map.GetNode<TileMapLayer>("Grid/EntityLayer") as EntityLayer;
		this.topPanel = this.GetNode("../../../TopPanel") as TopPanel;
		this.deviceAdjustmentPanel = this.GetNode("DeviceAdjustmentPanel") as DeviceAdjustmentPanel;
	
		this.selector.Layer = Selector.SelectorLayer.Entity;
		this.selector.EntityTypeFilter = [Entity.EntityType.Device];
		this.selector.SelectorMode = Selector.SelectorFilterMode.Fitlered;
		
		this.entitySelectedCall = new Callable(this, nameof(ClickedOnDevice));
		
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
