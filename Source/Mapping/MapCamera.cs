using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class MapCamera : Camera2D
{
	[Signal] public delegate void DraggingChangeEventHandler(DraggingState draggingState);
	[Signal] public delegate void ZoomingChangeEventHandler(double factor);

	public bool Locked { get; set; }
	public bool Dragging { get; private set; } = false;
	public bool Zooming { get; private set; } = false;
	public DraggingState Drag { get; private set; }
	
	private Vector2 minZoom;
	private Vector2 maxZoom;
	private Vector2 zoomFactor;
	private Rect2 usedRect;
	
	public override void _Ready()
	{
		minZoom = new Vector2(.5f, .5f);
		maxZoom = new Vector2(3f, 3f);
		zoomFactor = new Vector2(.5f, .5f);
		usedRect = new Rect2(new Vector2(-1f, -1f), new Vector2(-1f, -1f));
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (!Locked)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				HandleZoom(mouseEvent);
				HandleDrag(mouseEvent);
			}
		}
	}
	
	public void Calibrate(Layers.Layer layer)
	{
		usedRect = layer.GetUsedRect();
		int tileSize = layer.TileSize;
		LimitLeft = (int)(usedRect.Position.X * tileSize);
		LimitRight = (int)((usedRect.Position.X + usedRect.Size.X) * tileSize);
		LimitTop = (int)(usedRect.Position.Y * tileSize);
		LimitBottom = (int)((usedRect.Size.Y * tileSize) + (usedRect.Position.Y * tileSize));
		Position = usedRect.Position + usedRect.GetCenter() * layer.TileSize;
	}
	
	public void Fit(Layers.Layer layer)
	{
		
	}
	
	private void HandleDrag(InputEventMouse mouseEvent)
	{
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
		}
	}
	
	private void ChangePosition(Vector2 amount)
	{
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
		}
	}
	
	private void HandleZoom(InputEventMouse mouseEvent)
	{
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
	}
}
