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
		int firedCount = 0;
		
		foreach (KeyValuePair<Vector2I, Entity> entity in deviceEntities)
		{
			SceneTreeTimer delayToFire = GetTree().CreateTimer(GD.RandRange(0.05f, 0.5f));
			delayToFire.Connect("timeout", Callable.From(() => {
				Device device = (Device)entity.Value;
				
				Projectile projectile = device.Fire();
				projectile.Connect("ProjectileLanded", Callable.From(() => {
					firedCount++;
					
					if (firedCount == deviceEntities.Count)
					{
						ToDeviceAdjustment();
					}
				}));
			}));
		}
	}
	
	private void ToDeviceAdjustment()
	{
		(GetNode("../../../") as Battle).StartDeviceAdjustment();
	}
}
