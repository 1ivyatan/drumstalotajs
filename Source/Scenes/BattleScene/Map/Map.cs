using Godot;
using System;

namespace Drumstalotajs.Scenes.BattleScene
{
	public partial class Map : Node2D
	{
		private Camera2D camera;
		private bool dragging = false;
		
		private void Zoom(Vector2 amount)
		{
			camera.Zoom += amount;
		}
		
		public override void _Ready()
		{
			camera = GetNode<Camera2D>("Camera");
		}
		
		public override void _Input(InputEvent @event)
		{
			if (@event is InputEventMouse mouseEvent)
			{
				if (mouseEvent is InputEventMouseButton mouseClick)
				{
					dragging = mouseClick.Pressed && mouseClick.ButtonIndex == (MouseButton)1;
					
					if (mouseClick.Pressed)
					{
						switch (mouseClick.ButtonIndex)
						{
							case (MouseButton)4: Zoom(new Vector2(0.25f, 0.25f)); break;
							case (MouseButton)5: Zoom(new Vector2(-0.25f, -0.25f)); break;
						}
						GD.Print(mouseClick.ButtonIndex);
					}
					
					/*
					dragging = mouseClick.Pressed && buttonIndex == (MouseButton)1;
						GD.Print(buttonIndex);
						switch (buttonIndex)
						{
							case (MouseButton)4:
								camera.Zoom += new Vector2(0.25f, 0.25f);
								break;
							
							case (MouseButton)5:
								camera.Zoom -= new Vector2(0.25f, 0.25f);
								break;
						}*/
					
					
				}
				
				if (mouseEvent is InputEventMouseMotion mouseMotion)
				{
					if (dragging)
					{
						GD.Print(mouseMotion.Relative);
					}
				}
			}
		}
	}
}
