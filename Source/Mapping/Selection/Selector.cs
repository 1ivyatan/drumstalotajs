using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	//[Signal] public delegate void SelectedEventHandler(Vector2I cellPos);
	[Signal] public delegate void HoveredGroundEventHandler(Vector2I cellPos);
	[Signal] public delegate void HoveredEntityEventHandler(Entities.Entity entity);
	[Signal] public delegate void UnhoveredEntityEventHandler();
	
	public bool Locked { get; set; } = false;
	public bool Readonly { get; set; } = true;
	
	private Map map;
	private Camera.MapCamera camera;
	private Sprite2D sprite;
	
	private Vector2I currentCellPos;
	private Entities.Entity currentEntity = null;
	private bool canGround = false;
	private bool canEntity = false;
	//private bool moving = false;
	//private Timer timer;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		map = GetNode<Node2D>("../") as Map;
		camera = map.GetNode<Camera2D>("Camera") as Camera.MapCamera;
		movementTimer = SetMovementTimer(.05f, ScanEntities);
		AddChild(movementTimer);
		//currentEntity = null;
		//Visible = false;
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent && Mode != SelectorMode.LOCK)
		{
			if (HasNewCellPosition(mouseEvent))
			{
				HandleGround(mouseEvent);
			}

			HandleEntity(mouseEvent);
		}
	}
	
	private bool HasNewCellPosition(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseMotion mouseMotion || mouseEvent is InputEventMouseButton mouseClick && ( mouseClick.ButtonIndex == MouseButton.WheelUp || mouseClick.ButtonIndex == MouseButton.WheelDown))
		{
			Vector2 mouseLocalPos = GetLocalMousePos();
			Vector2I cellPos = map.GroundLayer.LocalToMap(mouseLocalPos);
			if (cellPos != currentCellPos)
			{
				currentCellPos = map.GroundLayer.LocalToMap(mouseLocalPos); 
				return true;
			} else return false;
		} else return false;
	}
	
	private void HandleGround(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseMotion mouseMotion)
		{
			canGround = AllowedGround(currentCellPos);
			PlaceHighlighter();
		}
	}
	
	private void HandleEntity(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseMotion mouseMotion)
		{
			canEntity = false;
			StartMovementTimer(movementTimer);
		}
	}
	
	private void PlaceHighlighter()
	{
		if (canEntity)
		{
			Position = currentEntity.Position;
			Rotation = currentEntity.Rotation;
		} else if (canGround)
		{
			int tileSize = map.GroundLayer.TileSize;
			SetPosition((currentCellPos * tileSize) + new Vector2I(tileSize / 2, tileSize / 2));
			Rotation = 0f;
			Visible = true;
		} else
		{
			Visible = false;
		}
	}
	
	
	private void ScanEntities()
	{
		Vector2 mouseLocalPos = GetLocalMousePos();
		Entities.Entity[] entities = AllowedEntities(mouseLocalPos);
		if (entities != null && entities.Length > 0)
		{
			currentEntity = entities[0];
			canEntity = true;
		} else
		{
			canEntity = false;
		}

		PlaceHighlighter();
	}
		/*
		if (!Locked)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				Vector2 localPos = GetLocalPos();
				Vector2I cellPos = GetCellPos(localPos);
				
				
				if (mouseEvent is InputEventMouseMotion mouseMotion)
				{
					if (cellPos != currentCellPos)
					{
						currentCellPos = cellPos;
						ResetMovementTimer(timer);
					}
				}
				
				try {
					if (currentEntity != null)
					{
						MoveEntityHighlighter(currentEntity.Position, currentEntity.Rotation);
					} else
					{
						cellPos = GetCellPos(localPos);
						MoveGroundHighlighter(cellPos);
						
					}
				} catch (Exception e)
				{
					currentEntity = null;
					cellPos = GetCellPos(localPos);
					MoveGroundHighlighter(cellPos);
					GD.Print(e);
				}
			}
		} else
		{
			HideHighlighter();
		}*/
	
//	private void ScanEntities()
/*	{
		Vector2 localPos = GetLocalPos();
		Vector2I cellPos = GetCellPos(localPos);
		Entities.Entity[] entities = AllowedEntityFilter(localPos);
		
		if (entities != null && entities.Length > 0)
		{
			Entities.Entity entity = entities[0];
			
			if (currentEntity != entity)
			{
				currentEntity = entity;
				EmitSignal("HoveredEntity", currentEntity);
			}
			
			var removeSelectedEntity = () => {
				currentEntity = null;
				EmitSignal("UnhoveredEntity");
			};
			
			var removeSelectedEntityCall = Callable.From(removeSelectedEntity);
			
			if (!entity.IsConnected("mouse_exited", removeSelectedEntityCall))
			{
				entity.Connect("mouse_exited", removeSelectedEntityCall);
			}
			
			MoveEntityHighlighter(currentEntity.Position, currentEntity.Rotation);
		}
	}*/
	
	/*public void Reposition()
	{
		if (!Locked)
		{
			Vector2 localPos = GetLocalPos();
			Vector2I cellPos = GetCellPos(localPos);
			MoveGroundHighlighter(cellPos);
		}
	}
	
	public void Reflash()
	{
		if (!Locked)
		{
			ScanEntities();
		}
	}*//*
	
	private void MoveEntityHighlighter(Vector2 localPos, float rotation)
	{
		Position = localPos;
		Rotation = rotation;
		Visible = true;
	}
	
	private void MoveGroundHighlighter(Vector2I cellPos)
	{
		bool allowed = AllowedTileFilter(currentCellPos);
		if (allowed)
		{
			SetPosition((cellPos * map.GroundLayer.TileSize) + new Vector2I(map.GroundLayer.TileSize / 2, map.GroundLayer.TileSize / 2));
			Rotation = 0f;
			Visible = true;
		}
		
		if (allowed || !Readonly)
		{
			EmitSignal("HoveredGround", cellPos);
		}
		
	}
	
	private void HideHighlighter()
	{
		Visible = false;
	} */
	
}
