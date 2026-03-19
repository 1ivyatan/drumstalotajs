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
	
	private Vector2 ScreenToWorld(Vector2 screenPos)
	{
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		Vector2 viewportCenter = viewportSize / 2f;
		return Position + (screenPos - viewportCenter) / Zoom.X;
	}
}
