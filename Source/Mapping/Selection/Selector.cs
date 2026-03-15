using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	[Signal] public delegate void SelectedEventHandler(Vector2I cellPos);
	[Signal] public delegate void HoveredGroundEventHandler(Vector2I cellPos);
	
	public bool Locked { get; set; } = false;
	public bool Readonly { get; set; } = true;
	
	private Map map;
	private Sprite2D sprite;
	
	private Vector2I currentCellPos;
	private bool moving = false;
	private Timer timer;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite");
		map = GetNode<Node2D>("../") as Map;
		timer = SetMovementTimer(.1f);
		timer.Timeout += ScanEntities;
		AddChild(timer);
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (!Locked)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				Vector2 localPos = GetLocalPos();
				Vector2I cellPos = GetCellPos(localPos);
				bool allowedGroundFilter = false;
				
				if (mouseEvent is InputEventMouseMotion mouseMotion)
				{
					if (cellPos != currentCellPos)
					{
						allowedGroundFilter = AllowedTileFilter(cellPos);
						cellPos = currentCellPos;
						ResetMovementTimer(timer);
					}
				}
				
				if (allowedGroundFilter)
				{
					cellPos = GetCellPos(localPos);
					MoveGroundHighlighter(cellPos);
				}
				
				
				
				/*bool allowedGroundFilter = false;
				bool allowedEntityFilter = false;
				
				if (mouseEvent is InputEventMouseMotion mouseMotion)
				{
					if (cellPos != currentCellPos)
					{
						allowedGroundFilter = AllowedTileFilter(cellPos);
						cellPos = currentCellPos;
						moving = true;
						ResetMovementTimer(timer);
					}
				}
				
				if (!moving)
				{
					allowedEntityFilter = AllowedEntityFilter(localPos);
				}
				
				GD.Print(cellPos);
				if (allowedGroundFilter)
				{
					MoveHighlighter(cellPos);
				} else
				{
					HideHighlighter();
				}*/
				
			
			/*	if (AllowedTileFilter(cellPos))
				{
					if (mouseEvent is InputEventMouseMotion mouseMotion)
					{
						if (cellPos != currentCellPos)
						{
							MoveHighlighter(cellPos);
						}
					} else
					{
						GD.Print(123);
					}
				} */
			}
		} else
		{
			HideHighlighter();
		}
	}
	
	private void ScanEntities()
	{
		Vector2 localPos = GetLocalPos();
		Vector2I cellPos = GetCellPos(localPos);
		Entities.Entity[] entities = AllowedEntityFilter(localPos);
		
		if (entities != null && entities.Length > 0)
		{
			Entities.Entity entity = entities[0];
			MoveHighlighter(entity.Position);
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
	
	private void MoveHighlighter(Vector2 localPos)
	{
		SetPosition(localPos);
		Visible = true;
		//EmitSignal("HoveredGround", cellPos);
	}
	
	private void MoveGroundHighlighter(Vector2I cellPos)
	{
		SetPosition((cellPos * map.GroundLayer.TileSize) + new Vector2I(map.GroundLayer.TileSize / 2, map.GroundLayer.TileSize / 2));
		Visible = true;
		EmitSignal("HoveredGround", cellPos);
	}
	
	private void HideHighlighter()
	{
		Visible = false;
	}
}
