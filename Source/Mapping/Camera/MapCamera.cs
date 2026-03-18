using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	[Signal] public delegate void DraggingChangeEventHandler(DraggingState draggingState);
	[Signal] public delegate void ZoomingChangeEventHandler(double factor);
	
	[Export] private double MinZoom;
	[Export] private double MaxZoom;
	[Export] private double ZoomFactor;
	private Rect2 UsedRect;
	
	public override void _Ready()
	{
		//UsedRect = new Rect2();
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		switch (Mode)
		{
			case MapCameraMode.VIEW: 
				if (@event is InputEventMouse mouseEvent)
				{
					HandleZoom(mouseEvent);
				}
				break;
			default: break;
		}
		/*
		if (!Locked)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				HandleMouseZoom(mouseEvent);
				HandleDrag(mouseEvent);
			}
		}*/
	}
	
	public void Calibrate(Layers.Layer layer)
	{
		UsedRect = layer.GetUsedRect();
		int tileSize = layer.TileSize;
		LimitLeft = (int)(UsedRect.Position.X * tileSize);
		LimitRight = (int)((UsedRect.Position.X + UsedRect.Size.X) * tileSize);
		LimitTop = (int)(UsedRect.Position.Y * tileSize);
		LimitBottom = (int)((UsedRect.Size.Y * tileSize) + (UsedRect.Position.Y * tileSize));
		Position = UsedRect.Position + UsedRect.GetCenter() * layer.TileSize;
	}
	
	public void Fit(Layers.Layer layer)
	{
		
	}
	
	private void HandleDrag(InputEventMouse mouseEvent)
	{
		/*
		if (!Locked)
		{
			if (mouseEvent is InputEventMouseButton mouseClick)
			{
				if (mouseClick.Pressed)
				{
					switch (mouseClick.ButtonIndex)
					{
						case MouseButton.Left: Dragging = true; break;
					}
				} else
				{
					if (!(Zooming && Dragging))
					{
						Dragging = false;
						Drag = DraggingState.NONE;
						EmitSignal("DraggingChange", (int)Drag);
					}
				}
			}

			if (mouseEvent is InputEventMouseMotion mouseMotion)
			{
				ChangePosition(mouseMotion.Relative);
			}
		}*/
	}
	
	private void ChangePosition(Vector2 amount)
	{
		/*
		if (!Locked && usedRect.Position != new Vector2(-1f, -1f) && Dragging && amount != Vector2.Zero)
		{
			Vector2 viewportSize = GetViewportRect().Size / Zoom;
			Vector2 newPosition = (GlobalPosition - (amount / Zoom));
			float limitLeft = LimitLeft + viewportSize.X / 2;
			float limitRight = LimitRight - viewportSize.X / 2;
			float limitTop = LimitTop + viewportSize.Y / 2;
			float limitBottom = LimitBottom - viewportSize.Y / 2;
			
			if (limitLeft < limitRight || limitTop < limitBottom)
			{
				if (limitLeft < limitRight)
				{
					newPosition.X = Mathf.Clamp(newPosition.X, limitLeft, limitRight);
					Drag = DraggingState.VERTICAL;
				}
				
				if (limitTop < limitBottom)
				{
					newPosition.Y = Mathf.Clamp(newPosition.Y, limitTop, limitBottom);
					Drag = DraggingState.HORIZONTAL;
				}
				
				if (limitLeft < limitRight && limitTop < limitBottom)
				{
					Drag = DraggingState.ALL;
				}
				
				GlobalPosition = newPosition;
			} else
			{
				Drag = DraggingState.NONE;
			}
			
			EmitSignal("DraggingChange", (int)Drag);
		}*/
	}
	
	private void HandleZoom(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseButton mouseClick)
		{
			if (mouseClick.Pressed)
			{
				switch (mouseClick.ButtonIndex)
				{
					case MouseButton.WheelUp: ZoomToCursor(ZoomFactor); break;
					case MouseButton.WheelDown: ZoomToCursor(-ZoomFactor); break;
				}
				//State = MapCameraState.ZOOM;
				////EmitSignal("ZoomingChange", change.Length() * change.X > 0 ? 1 : -1);
			} else
			{
				
			}
		}
	}
	
	private Vector2 ScreenToWorld(Vector2 screenPos)
	{
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		Vector2 viewportCenter = viewportSize / 2f;
		return Position + (screenPos - viewportCenter) / Zoom.X;
	}
	
	private void ZoomToCursor(double factor)
	{
		Vector2 mouseScreenPos = GetViewport().GetMousePosition();
		Vector2 viewportCenter = GetViewport().GetVisibleRect().Size / 2f;
		Vector2 offset = mouseScreenPos - viewportCenter;
		float oldZoom = (float)Zoom.X;
		float newZoom = (float)Mathf.Clamp(oldZoom + factor, MinZoom, MaxZoom);
		Vector2 mouseWorldBefore = Position + offset / oldZoom;
		Vector2 mouseWorldAfter  = Position + offset / newZoom;
		Zoom = new Vector2(newZoom, newZoom);
		Position -= mouseWorldAfter - mouseWorldBefore;
	}
		/*
		if (!Locked)
		{
			if (mouseEvent is InputEventMouseButton mouseClick)
			{
				if (mouseClick.Pressed)
				{
					switch (mouseClick.ButtonIndex)
					{
						case MouseButton.WheelUp: ChangeZoom(zoomFactor); break;
						case MouseButton.WheelDown: ChangeZoom(-zoomFactor); break;
					}
				}
			} else
			{
				Zooming = false;
			}
		}
	}
	
	private void ChangeZoom(Vector2 change)
	{
		if (!Locked)
		{
			Vector2 zoom = Zoom + change;
			Zoom = zoom.Clamp(minZoom, maxZoom);
			EmitSignal("ZoomingChange", change.Length() * change.X > 0 ? 1 : -1);
			Zooming = true;
		}
	}*/
	//}
}
