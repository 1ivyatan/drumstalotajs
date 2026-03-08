using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class MapWidget : Node2D
	{
		public bool Locked { get; set; } = false;
		public bool Dragging { get; set; } = false;
		
		private Map.Layers.GroundLayer groundLayer;
		private Camera camera;
		
		public void CenterAndAlign()
		{
			Rect2 usedRect = groundLayer.GetUsedRect();
			camera.SetLimits(usedRect, groundLayer.TileSize);
			camera.Position = usedRect.Position + usedRect.GetCenter() * 16;
		}
		
		private void Move(Vector2 amount)
		{
			if (!Locked)
			{
				Rect2 usedRect = groundLayer.GetUsedRect();
				Vector2 viewportSize = GetViewportRect().Size / camera.Zoom;
				Vector2 newPosition = (camera.GlobalPosition - (amount / camera.Zoom));
				
				float limitLeft = camera.LimitLeft + viewportSize.X / 2;
				float limitRight = camera.LimitRight - viewportSize.X / 2;
				float limitTop = camera.LimitTop + viewportSize.Y / 2;
				float limitBottom = camera.LimitBottom - viewportSize.Y / 2;
				
				if (limitLeft < limitRight || limitTop < limitBottom)
				{
					if (limitLeft < limitRight) newPosition.X = Mathf.Clamp(newPosition.X, limitLeft, limitRight);
					if (limitTop < limitBottom) newPosition.Y = Mathf.Clamp(newPosition.Y, limitTop, limitBottom);
					camera.GlobalPosition = newPosition;
				}
			}
		}
		
		private void Zoom(Vector2 amount)
		{
			if (!Locked)
			{
				Vector2 zoom = camera.Zoom + amount;
				camera.Zoom = zoom.Clamp(new Vector2(.25f, .25f), new Vector2(2f, 2f));
			}
		}
		
		public override void _Ready()
		{
			groundLayer = GetNode<TileMapLayer>("GroundLayer") as Map.Layers.GroundLayer;
			camera = GetNode<Camera2D>("Camera") as Camera;
			CenterAndAlign();
		}
		
		public override void _UnhandledInput(InputEvent @event)
		{
			if (!Locked)
			{
				if (@event is InputEventMouse mouseEvent)
				{
					if (mouseEvent is InputEventMouseButton mouseClick)
					{	
						if (mouseClick.Pressed)
						{
							switch (mouseClick.ButtonIndex)
							{
								case (MouseButton)4: Zoom(new Vector2(0.25f, 0.25f)); break;
								case (MouseButton)5: Zoom(new Vector2(-0.25f, -0.25f)); break;
							}
						}
					}
				
					if (mouseEvent is InputEventMouseMotion mouseMotion)
					{
						if (Dragging)
						{
							Move(mouseMotion.Relative);
						}
					}
				}
			}
		}
	}
}
