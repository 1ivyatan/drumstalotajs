using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Cameras;

public partial class Camera : Camera2D
{
	[Export] private double MinZoom { get; set; }
	[Export] private double MaxZoom { get; set; }
	[Export] private double ZoomFactor { get; set; }
	public CameraMode Mode { get; set; } = CameraMode.Lock;
	public CameraState State { get; private set { field = value; } } = CameraState.Idle;
	
	private AtlasLayer _calibratingAtlasLayer;
	private bool _dragging = false;
	private Vector2 _dragAnchorWorld;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		switch (Mode)
		{
			case CameraMode.View: 
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
			_dragging = mouseClick.Pressed;
			if (_dragging) _dragAnchorWorld = ScreenToWorld(GetViewport().GetMousePosition());
			else State = CameraState.Idle;
		}
		
		if (mouseEvent is InputEventMouseMotion && _dragging)
		{
			Vector2 mouseScreenPos = GetViewport().GetMousePosition();
			Vector2 currentWorldUnderCursor = ScreenToWorld(mouseScreenPos);
			Vector2 desiredPosition = GlobalPosition - (currentWorldUnderCursor - _dragAnchorWorld);
			GlobalPosition = ClampToLimits(desiredPosition);
			_dragAnchorWorld = ScreenToWorld(mouseScreenPos);
			State = CameraState.Drag;
		}
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
		
		if (mouseEvent is InputEventMouseMotion mouseMotion && !_dragging)
		{
			if (State != CameraState.Idle) State = CameraState.Idle;
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
		
		if (_dragging)
		{
			_dragAnchorWorld = ScreenToWorld(GetViewport().GetMousePosition());
			State = CameraState.Zoomdrag;
		} else
		{
			State = CameraState.Zoom;
		}
	}
}
