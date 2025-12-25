using Godot;
using System;

public partial class DevicePlacing : Stage
{
	[Signal]
	public delegate void OnGridDeviceAddedEventHandler(Vector2I position);
	
	[Signal]
	public delegate void OnGridDeviceRemovedEventHandler(Vector2I position);
	
	EntityLayer entityLayer;
	
	public override void LoadStage()
	{
		Map map = mapRootNode as Map;
		entityLayer = mapGridNode.GetNode<TileMapLayer>("EntityLayer") as EntityLayer;
		Connect("OnGridDeviceAdded", new Callable(map, "AddedDevice"));
		Connect("OnGridDeviceRemoved", new Callable(map, "RemovedDevice"));
	}
	
	void AddDevice(Vector2I position)
	{
		entityLayer.PlaceEntity(EntityType.Device, position);
	}
	
	void RemoveDevice(Vector2I position)
	{
		entityLayer.PlaceEntity(EntityType.DevicePlaceholder, position);
	}
	
	public override void Input(InputEvent @event) {
		if (@event is InputEventMouseButton eventButton)
		{
			if (eventButton.Pressed)
			{
				Vector2 globalMousePos = GetGlobalMousePosition();
				Vector2 localMousePos = mapGridNode.ToLocal(globalMousePos);
				Vector2I cellPos = entityLayer.LocalToMap(localMousePos);
				
				switch ((EntityType)entityLayer.GetCellAlternativeTile(cellPos))
				{
					case EntityType.None:
						break;
					case EntityType.DevicePlaceholder:
						AddDevice(cellPos);
						EmitSignal(SignalName.OnGridDeviceAdded, cellPos);
						break;
					case EntityType.Device:
						RemoveDevice(cellPos);
						EmitSignal(SignalName.OnGridDeviceRemoved, cellPos);
						break;
					default:	
						break;
				}
				
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
	}
}
