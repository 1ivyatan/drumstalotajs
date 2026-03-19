using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	//[Signal] public delegate void DraggingChangeEventHandler(DraggingState draggingState);
	//[Signal] public delegate void ZoomingChangeEventHandler(double factor);
	[Export] private double MinZoom;
	[Export] private double MaxZoom;
	[Export] private double ZoomFactor;
	private Rect2 UsedRect;
	private bool dragging = false;
	private Vector2 dragAnchorWorld;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		switch (Mode)
		{
			case MapCameraMode.VIEW: 
				if (@event is InputEventMouse mouseEvent)
				{
					HandleDrag(mouseEvent);
					HandleZoom(mouseEvent);
				}
				break;
			default: break;
		}
	}
	
	private void HandleDrag(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.ButtonIndex == MouseButton.Left)
		{
			dragging = mouseClick.Pressed;
			if (dragging) dragAnchorWorld = ScreenToWorld(GetViewport().GetMousePosition());
		}
		
		if (mouseEvent is InputEventMouseMotion && dragging)
		{
			Drag();
		}
	}
	
	private void Drag()
	{
		Vector2 mouseScreenPos = GetViewport().GetMousePosition();
		Vector2 currentWorldUnderCursor = ScreenToWorld(mouseScreenPos);
		Vector2 desiredPosition = Position - (currentWorldUnderCursor - dragAnchorWorld);
		Position = ClampToLimits(desiredPosition);
		dragAnchorWorld = ScreenToWorld(mouseScreenPos);
	}
	
	private void HandleZoom(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.Pressed)
		{
			switch (mouseClick.ButtonIndex)
			{
				case MouseButton.WheelUp: ZoomToCursor(ZoomFactor); break;
				case MouseButton.WheelDown: ZoomToCursor(-ZoomFactor); break;
			}
		}
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
		Position = ClampToLimits(Position - (mouseWorldAfter - mouseWorldBefore));
		if (dragging) dragAnchorWorld = ScreenToWorld(GetViewport().GetMousePosition());
	}
}
