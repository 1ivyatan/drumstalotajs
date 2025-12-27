using Godot;
using System;

public partial class DevicePlacing : Stage
{
	EntityLayer entityLayer;
	Selector selector;
	
	public override void LoadStage()
	{
		Map map = mapRootNode as Map;
		
		entityLayer = mapGridNode.GetNode<TileMapLayer>("EntityLayer") as EntityLayer;
		selector = mapGridNode.GetNode<Node2D>("Selector") as Selector;
		
		selector.Enabled(true);
		
		entityLayer.Connect("EntityCountUpdated", new Callable(this, nameof(UpdateUI)));
		selector.Connect("ClickedOnEntity", new Callable(this, nameof(SetSelection)));
	}
	
	public override void CloseStage()
	{
		entityLayer.RemoveEntitiesByType(EntityType.DevicePlaceholder);
	}
	
	void UpdateUI(int entityTypeId, int count)
	{
		UpdateHeader();
		UpdateFooter();
	}
	
	void UpdateHeader()
	{
		Node headerWidget = (sceneUiNode as Battle).HeaderWidget;
		if (headerWidget != null)
		{
			DevicePlacingHeader widget = headerWidget as DevicePlacingHeader;
			
			widget.SetLabels(
				entityLayer.GetEntityCollection(EntityType.Device).Count
			);
		}
	}
	
	void UpdateFooter()
	{
		Node footerWidget = (sceneUiNode as Battle).FooterWidget;
		if (footerWidget != null)
		{
			DevicePlacingFooter widget = footerWidget as DevicePlacingFooter;
			
			widget.UpdateLock(
				/* !!!!!!!!! */
				entityLayer.GetEntityCollection(EntityType.Device).Count > 0
			);
		}
	}
	
	void SetSelection(int entityType, Vector2I position)
	{
		switch ((EntityType)entityType)
		{
			case EntityType.DevicePlaceholder:
				entityLayer.InsertEntity(EntityType.Device, position);
				break;
			case EntityType.Device:
				entityLayer.InsertEntity(EntityType.DevicePlaceholder, position);
				break;
			default:	
				break;
		}
	}
	
	public override void Input(InputEvent @event) {
				/*
	   			var spaceState = GetWorld2D().DirectSpaceState;
				var query = new PhysicsPointQueryParameters2D();
				query.Position = globalMousePos;
				query.CollideWithAreas = true;
				
				var result = spaceState.IntersectPoint(query, 2);
				
				if (result.Count > 0)
				{
					Area2D collider = (Area2D)result[0]["collider"];
					
					if (collider.IsInGroup("placeholder"))
					{
						Vector2 localPos = MapGridNode.ToLocal(globalMousePos);
						TileMapLayer selectorLayer = MapGridNode.GetNode<TileMapLayer>("Placeholders");
						Vector2I tilePos = selectorLayer.LocalToMap(localPos);
						
						
						ToggleDevice(tilePos);
						EmitSignal(SignalName.ToggledDeviceInGrid, tilePos);
					}
				}*/
	}
}
