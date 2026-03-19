using Godot;
using System;

namespace drumstalotajs.Mapping.Camera;

public partial class MapCamera : Camera2D
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
	
	public void Calibrate(Layers.Layer layer)
	{
		Rect2 usedRect = layer.GetUsedRect();
		int tileSize = layer.TileSize;
		LimitLeft = (int)(usedRect.Position.X * tileSize);
		LimitRight = (int)((usedRect.Position.X + usedRect.Size.X) * tileSize);
		LimitTop = (int)(usedRect.Position.Y * tileSize);
		LimitBottom = (int)((usedRect.Size.Y * tileSize) + (usedRect.Position.Y * tileSize));
		Position = usedRect.Position + usedRect.GetCenter() * layer.TileSize;
	}
	
	public void FitCamera(Layers.Layer layer)
	{
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		Rect2 usedRect = layer.GetUsedRect();
		Rect2 worldRect = new Rect2(
			layer.MapToLocal(new Vector2I((int)usedRect.Position.X, (int)usedRect.Position.Y)),
			usedRect.Size * layer.TileSet.TileSize
		);
		float zoomX = viewportSize.X / worldRect.Size.X;
		float zoomY = viewportSize.Y / worldRect.Size.Y;
		float newZoom = Mathf.Max(zoomX, zoomY);
		Zoom = new Vector2(newZoom, newZoom);
		Position = ClampToLimits(worldRect.GetCenter());
	}
	
	private Vector2 ScreenToWorld(Vector2 screenPos)
	{
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		Vector2 viewportCenter = viewportSize / 2f;
		return Position + (screenPos - viewportCenter) / Zoom.X;
	}
}
