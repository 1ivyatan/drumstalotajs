using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
{
	[Export] private double MinZoom;
	[Export] private double MaxZoom;
	[Export] private double ZoomFactor;
	private bool dragging = false;
	private Vector2 dragAnchorWorld;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		switch (Mode)
		{
			case MapCameraMode.VIEW: 
				if (@event is InputEventMouse mouseEvent)
				{
					HandleZoom(mouseEvent);
					HandleDrag(mouseEvent);
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
			else State = MapCameraState.IDLE;
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
		Vector2 desiredPosition = GlobalPosition - (currentWorldUnderCursor - dragAnchorWorld);
		GlobalPosition = ClampToLimits(desiredPosition);
		dragAnchorWorld = ScreenToWorld(mouseScreenPos);
		State = MapCameraState.DRAG;
	}
	
	private void HandleZoom(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.Pressed)
		{
			if (mouseClick.Pressed)
			{
				switch (mouseClick.ButtonIndex)
				{
					case MouseButton.WheelUp: ZoomToCursor(ZoomFactor); break;
					case MouseButton.WheelDown: ZoomToCursor(-ZoomFactor); break;
				}
			}
		}
		
		if (mouseEvent is InputEventMouseMotion mouseMotion && !dragging)
		{
			if (State != MapCameraState.IDLE) State = MapCameraState.IDLE;
		}
	}
	
	private void ZoomToCursor(double factor)
	{
		Vector2 mouseScreenPos = GetViewport().GetMousePosition();
		Vector2 viewportCenter = GetViewport().GetVisibleRect().Size / 2f;
		Vector2 offset = mouseScreenPos - viewportCenter;
		float oldZoom = (float)Zoom.X;
		float newZoom = (float)Mathf.Clamp(oldZoom + factor, MinZoom, MaxZoom);
		Vector2 mouseWorldBefore = GlobalPosition + offset / oldZoom;
		Vector2 mouseWorldAfter  = GlobalPosition + offset / newZoom;
		Zoom = new Vector2(newZoom, newZoom);
		GlobalPosition = ClampToLimits(GlobalPosition - (mouseWorldAfter - mouseWorldBefore));
		
		if (dragging)
		{
			dragAnchorWorld = ScreenToWorld(GetViewport().GetMousePosition());
			State = MapCameraState.ZOOMDRAG;
		} else
		{
			State = MapCameraState.ZOOM;
		}
	}
}
