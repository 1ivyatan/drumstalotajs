using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Signal] public delegate void HoveredGroundEventHandler(Vector2I cellPos);
	[Signal] public delegate void UnhoveredGroundEventHandler(Vector2I cellPos);
	[Signal] public delegate void HoveredEntityEventHandler(Entities.Entity entity);
	[Signal] public delegate void UnhoveredEntityEventHandler();
	
	[Signal] public delegate void ClickedGroundEventHandler(Vector2I cellPos);
	[Signal] public delegate void UnclickedGroundEventHandler(Vector2I cellPos);
	[Signal] public delegate void ClickedEntityEventHandler(Entities.Entity entity);
	[Signal] public delegate void UnclickedEntityEventHandler();
	
	private Map map;
	private Camera.MapCamera camera;
	private Sprite2D sprite;
	
	private Callable setEntityHoldCall;
	private Callable rescanAfterExitCall;
	private Vector2I currentCellPos;
	private Entities.Entity currentEntity = null;
	private bool canGround = false;
	private bool canEntity = false;
	private bool holdEntity = false;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		map = GetNode<Node2D>("../") as Map;
		camera = map.GetNode<Camera2D>("Camera") as Camera.MapCamera;
		movementTimer = new MovementTimer();
		movementTimer.SetTimer(.025f, ScanEntities);
		AddChild(movementTimer);
		setEntityHoldCall = Callable.From(() => {holdEntity = false;});
		rescanAfterExitCall = Callable.From(() => { 
			GD.Print("??????"); ScanEntities();
		});
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent && Mode != SelectorMode.LOCK)
		{
			HandleEntity(mouseEvent);
			HandleGround(mouseEvent);
		}
	}
	
	private bool HasNewCellPosition(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseMotion mouseMotion || mouseEvent is InputEventMouseButton mouseClick && ( mouseClick.ButtonIndex == MouseButton.WheelUp || mouseClick.ButtonIndex == MouseButton.WheelDown))
		{
			Vector2I cellPos = GetCellPosFromMouse();
			return cellPos != currentCellPos;
		} else return false;
	}
	
	private void HandleGround(InputEventMouse mouseEvent)
	{
		if (HasNewCellPosition(mouseEvent) && mouseEvent is InputEventMouseMotion mouseMotion)
		{
			currentCellPos = GetCellPosFromMouse();
			canGround = AllowedGround(currentCellPos);
			EmitSignal(canGround ? "HoveredGround" : "UnhoveredGround", currentCellPos);
			PlaceHighlighter();
		}
		
		if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.Pressed && mouseClick.ButtonIndex == MouseButton.Left)
		{
			EmitSignal(canGround ? "ClickedGround" : "UnclickedGround", currentCellPos);
		}
	}
	
	private void HandleEntity(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseMotion mouseMotion)
		{
			if (holdEntity) return;
			canEntity = false;
			movementTimer.RestartTimer();
		}
		
		if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.Pressed && mouseClick.ButtonIndex == MouseButton.Left && movementTimer.IsStopped())
		{
			if (canEntity) EmitSignal("ClickedEntity", currentEntity);
			else EmitSignal("UnclickedEntity");
		}
	}
	
	public void RefreshHightlighter()
	{
		PlaceHighlighter();
	}
	
	private void PlaceHighlighter()
	{
		if (holdEntity)
		{
			Position = currentEntity.Position;
			Rotation = currentEntity.Rotation;
			Visible = true;
			
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
			if (!currentEntity.IsConnected("mouse_exited", setEntityHoldCall))
			{
				currentEntity.Connect("mouse_exited", setEntityHoldCall);
			} 
			
			if (!currentEntity.IsConnected("tree_exiting", rescanAfterExitCall))
			{
				currentEntity.Connect("tree_exiting", rescanAfterExitCall);
			}
			canEntity = true;
			holdEntity = true;
			EmitSignal("HoveredEntity", currentEntity);
		} else
		{
			canEntity = false;
			holdEntity = false;
			EmitSignal("UnhoveredEntity");
		}
		PlaceHighlighter();
	}
}
