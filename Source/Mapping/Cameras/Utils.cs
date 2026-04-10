using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Cameras;

public partial class Camera : Camera2D
{
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
	
	public void SetCalibratingGroundLayer(GroundLayer layer)
	{
		_calibratingGroundLayer = layer;
	}
	
	public void Calibrate()
	{
		Rect2 usedRect = _calibratingGroundLayer.GetUsedRect();
		int tileSize = _calibratingGroundLayer.TileSize;
		LimitLeft = (int)(usedRect.Position.X * tileSize);
		LimitRight = (int)((usedRect.Position.X + usedRect.Size.X) * tileSize);
		LimitTop = (int)(usedRect.Position.Y * tileSize);
		LimitBottom = (int)((usedRect.Size.Y * tileSize) + (usedRect.Position.Y * tileSize));
		GlobalPosition = usedRect.Position + usedRect.GetCenter() * _calibratingGroundLayer.TileSize;
	}
	
	public void FitCamera()
	{
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		Rect2 usedRect = _calibratingGroundLayer.GetUsedRect();
		Rect2 worldRect = new Rect2(
			_calibratingGroundLayer.MapToLocal(new Vector2I((int)usedRect.Position.X, (int)usedRect.Position.Y)),
			usedRect.Size * _calibratingGroundLayer.TileSet.TileSize
		);
		float zoomX = viewportSize.X / worldRect.Size.X;
		float zoomY = viewportSize.Y / worldRect.Size.Y;
		float newZoom = Mathf.Max(zoomX, zoomY);
		Zoom = new Vector2(newZoom, newZoom);
		GlobalPosition = ClampToLimits(worldRect.GetCenter());
	}
	
	public Vector2 ScreenToWorld(Vector2 screenPos)
	{
		Vector2 viewportCenter = GetViewport().GetVisibleRect().Size / 2f;
		return GlobalPosition + (screenPos - viewportCenter) / Zoom.X;
	}
}
