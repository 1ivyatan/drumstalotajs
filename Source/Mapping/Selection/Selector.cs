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
	[Signal] public delegate void DisappearedSelectedEntityEventHandler();
	
	private Map map;
	private Camera.MapCamera camera;
	private Sprite2D sprite;
	
	private Callable setEntityHoldCall;
	private Callable rescanAfterExitCall;
	
	private Vector2I currentCellPos;
	private bool canGround = false;
	
	private Entities.Entity currentEntity = null;
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
		
		setEntityHoldCall = Callable.From(() => {
			currentEntity = null;
			holdEntity = false;
		});
		
		rescanAfterExitCall = Callable.From(() => {
			EmitSignal("DisappearedSelectedEntity");
			holdEntity = false;
			currentEntity = null;
			movementTimer.Stop();
			ScanEntities();
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
	
	private void HandleEntity(InputEventMouse mouseEvent)
	{
		GD.Print(IsInstanceValid(currentEntity));
		
		if (mouseEvent is InputEventMouseMotion mouseMotion)
		{
			if (holdEntity) return;
			movementTimer.RestartTimer();
		}
		
		if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.Pressed && mouseClick.ButtonIndex == MouseButton.Left && movementTimer.IsStopped())
		{
			if (IsInstanceValid(currentEntity)) EmitSignal("ClickedEntity", currentEntity);
			else EmitSignal("UnclickedEntity");
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
	
	public void RefreshHightlighter()
	{
		PlaceHighlighter();
	}
	
	private bool EntityExists()
	{
		return true;
	}
	
	private void PlaceHighlighter()
	{
		if (holdEntity && IsInstanceValid(currentEntity))
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
			if (IsInstanceValid(currentEntity))
			{
				DetachSignalsToEntity(currentEntity);
			}
			
			currentEntity = entities[0];
			AttachSignalsToEntity(currentEntity);
			EmitSignal("HoveredEntity", currentEntity);
			holdEntity = true;
		} else
		{
			if (IsInstanceValid(currentEntity))
			{
				DetachSignalsToEntity(currentEntity);
			}
			currentEntity = null;
			holdEntity = false;
			EmitSignal("UnhoveredEntity");
		}
		PlaceHighlighter();
	}
	
	private void AttachSignalsToEntity(Entities.Entity entity)
	{
		if (!entity.IsConnected("mouse_exited", setEntityHoldCall))
		{
			entity.Connect("mouse_exited", setEntityHoldCall);
		}

		if (!entity.IsConnected("tree_exiting", rescanAfterExitCall))
		{
			entity.Connect("tree_exiting", rescanAfterExitCall);
		}
	}
	
	private void DetachSignalsToEntity(Entities.Entity entity)
	{
		if (entity.IsConnected("mouse_exited", setEntityHoldCall))
		{
			entity.Disconnect("mouse_exited", setEntityHoldCall);
		}

		if (entity.IsConnected("tree_exiting", rescanAfterExitCall))
		{
			entity.Disconnect("tree_exiting", rescanAfterExitCall);
		}
	}
}
