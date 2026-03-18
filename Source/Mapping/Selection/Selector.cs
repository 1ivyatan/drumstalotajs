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
	private Sprite2D sprite;
	
	private Vector2I currentCellPos;
	private Entities.Entity currentEntity = null;
	private bool moving = false;
	private Timer timer;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		map = GetNode<Node2D>("../") as Map;
		//currentEntity = null;
		//Visible = false;
		//timer = SetMovementTimer(.001f);
		//timer.Timeout += ScanEntities;
		//AddChild(timer);
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
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
	}
	
	private void ScanEntities()
	{/*
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
	}
	
	public void Reposition()
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
	}
	
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
	}*/
	}
}
