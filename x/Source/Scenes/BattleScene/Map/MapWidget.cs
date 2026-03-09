using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene.Map
{
	public partial class MapWidget : Node2D
	{
		[Signal] public delegate void DraggingChangeEventHandler(DraggingState state);
		[Signal] public delegate void ScrollingChangeEventHandler(bool scrolling);
		
		public bool Locked { get; set; } = false;
		public bool Dragging { get; set; } = false;
		public DraggingState DragState { get; set; } = DraggingState.NONE;
	//	public bool Zooming { get; set; } = false;
		
		private Map.Layers.GroundLayer groundLayer;
		private Map.Layers.DecorationLayer decorationLayer;
		private Map.Layers.EntityLayer entityLayer;
		private Map.Selector selector;
		
		private Camera camera;
		
		public override void _Ready()
		{
			groundLayer = GetNode<TileMapLayer>("GroundLayer") as Map.Layers.GroundLayer;
			decorationLayer = GetNode<TileMapLayer>("DecorationLayer") as Map.Layers.DecorationLayer;
			entityLayer = GetNode<TileMapLayer>("EntityLayer") as Map.Layers.EntityLayer;
			selector = GetNode<Node2D>("Selector") as Map.Selector;
			camera = GetNode<Camera2D>("Camera") as Camera;
			
			//Input.SetDefaultCursorShape(Input.CursorShape.Move);
			/*
			DraggingChange += (Map.MapWidget.DraggingState state) => {
				if (state != Map.MapWidget.DraggingState.NONE)
				{
				} else {
					Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
				}
			};*/
		}
		
		public override void _UnhandledInput(InputEvent @event)
		{
			if (!Locked)
			{
				if (@event is InputEventMouse mouseEvent)
				{
					if (mouseEvent is InputEventMouseButton mouseClick)
					{	
						Dragging = mouseClick.Pressed && mouseClick.ButtonIndex == (MouseButton)1;
						
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
						if (mouseMotion.Relative != Vector2.Zero)
						{
							if (Dragging)
							{
								selector.Disabled = true;
								Move(mouseMotion.Relative);
							} else
							{
								selector.Disabled = false;
								DragState = DraggingState.NONE;
								EmitSignal("DraggingChange", (int)DragState);
							}
							
							//if (Zooming)
							//{
							//	Zooming = false;
							//	EmitSignal("ScrollingChange", Zooming);
							//}
						}
					}
				}
			}
		}
		
		public void CenterAndAlign()
		{
			Rect2 usedRect = groundLayer.GetUsedRect();
			camera.SetLimits(usedRect, groundLayer.TileSize);
			camera.Position = usedRect.Position + usedRect.GetCenter() * groundLayer.TileSize;
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
					if (limitLeft < limitRight)
					{
						newPosition.X = Mathf.Clamp(newPosition.X, limitLeft, limitRight);
						DragState = DraggingState.HORIZONTAL;
					}
					
					if (limitTop < limitBottom)
					{
						newPosition.Y = Mathf.Clamp(newPosition.Y, limitTop, limitBottom);
						DragState = DraggingState.VERTICAL;
					}
					
					if (limitLeft < limitRight && limitTop < limitBottom) {
						DragState = DraggingState.ALL;
					}
					
					camera.GlobalPosition = newPosition;
				} else
				{
					DragState = DraggingState.NONE;
				}
			}
		}
		
		private void Zoom(Vector2 amount)
		{
			if (!Locked)
			{
				Vector2 zoom = camera.Zoom + amount;
				camera.Zoom = zoom.Clamp(new Vector2(.25f, .25f), new Vector2(2.5f, 2.5f));
				//Zooming = true;
				//EmitSignal("ScrollingChange", Zooming);
			}
		}
		
		public void LoadLayers(Resources.Levels.Level level)
		{
			groundLayer.LoadLayer(level.GroundPatternPath);
			decorationLayer.LoadLayer(level.DecorationPatternPath);
			entityLayer.LoadLayer(level.EntityPatternPath);
			CenterAndAlign();
		}
	}
}
