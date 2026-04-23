using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Cameras;

public partial class Camera : Camera2D
{
	[Signal] public delegate void StateChangedEventHandler(CameraState state);
	public CameraMode Mode { get; set; } = CameraMode.Locked;
	public CameraState State { get; 
		private set {
			field = value;
			EmitSignal(SignalName.StateChanged, (int)field);
		}
	} = CameraState.Idle;
	
	[Export] private AtlasLayer _calibratingAtlasLayer;
	private bool _dragging = false;
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			switch (Mode)
			{
				case CameraMode.DragOnly:
					HandleDrag(mouseEvent);
					break;
				default: break;
			}
		}
	}
	
	private void HandleDrag(InputEventMouse mouseEvent)
	{
		if (mouseEvent is InputEventMouseButton mouseClick && mouseClick.ButtonIndex == MouseButton.Left)
		{
			_dragging = mouseClick.Pressed;
			State = _dragging ? CameraState.Dragging : CameraState.Idle;
		}
		
		if (mouseEvent is InputEventMouseMotion mouseMotion && _dragging)
		{
			Vector2 desiredPosition = GlobalPosition - (mouseMotion.Relative / Zoom);
			GlobalPosition = ClampToLimits(desiredPosition);
			State = CameraState.Dragging;
		}
	}
	
	public Vector2 ScreenToWorld(Vector2 screenPos)
	{
		Vector2 viewportCenter = GetViewport().GetVisibleRect().Size / 2f;
		return GlobalPosition + (screenPos - viewportCenter) / Zoom.X;
	}
	
	private Vector2 ClampToLimits(Vector2 desiredPosition)
	{
		Vector2 viewportSize = GetViewportRect().Size / Zoom;
		Vector2 result = desiredPosition;
		float limitLeft   = LimitLeft   + viewportSize.X / 2f;
		float limitRight  = LimitRight  - viewportSize.X / 2f;
		float limitTop    = LimitTop    + viewportSize.Y / 2f;
		float limitBottom = LimitBottom - viewportSize.Y / 2f;
		if (limitLeft < limitRight) result.X = Mathf.Clamp(desiredPosition.X, limitLeft, limitRight);
		if (limitTop < limitBottom) result.Y = Mathf.Clamp(desiredPosition.Y, limitTop, limitBottom);
		return result;
	}
	
	public void Calibrate()
	{
		Rect2 usedRect = _calibratingAtlasLayer.GetUsedRect();
		int tileSize = _calibratingAtlasLayer.TileSize;
		LimitLeft = (int)(usedRect.Position.X * tileSize);
		LimitRight = (int)((usedRect.Position.X + usedRect.Size.X) * tileSize);
		LimitTop = (int)(usedRect.Position.Y * tileSize);
		LimitBottom = (int)((usedRect.Size.Y * tileSize) + (usedRect.Position.Y * tileSize));
		GlobalPosition = usedRect.Position + usedRect.GetCenter() * _calibratingAtlasLayer.TileSize;
	}
	
	public void FitCamera()
	{
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		Rect2 usedRect = _calibratingAtlasLayer.GetUsedRect();
		Rect2 worldRect = new Rect2(
			_calibratingAtlasLayer.MapToLocal(new Vector2I((int)usedRect.Position.X, (int)usedRect.Position.Y)),
			usedRect.Size * _calibratingAtlasLayer.TileSet.TileSize
		);
		float zoomX = viewportSize.X / worldRect.Size.X;
		float zoomY = viewportSize.Y / worldRect.Size.Y;
		float newZoom = Mathf.Max(zoomX, zoomY);
		Zoom = new Vector2(newZoom, newZoom);
		GlobalPosition = ClampToLimits(worldRect.GetCenter());
	}
}
