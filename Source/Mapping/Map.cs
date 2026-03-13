using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public Layers.GroundLayer GroundLayer { get; private set; }
	public Layers.DecorationLayer DecorationLayer { get; private set; }
	public Layers.EntityLayer EntityLayer { get; private set; }
	public Mapping.Selector Selector { get; private set; }
	public MapCamera Camera { get; private set; }
	public int TileSize { get; private set; }
	public Vector2I CurrentCellPos { get; private set; }
	public bool Editing { get; 
		set
		{
			field = value;
			if (Selector != null) Selector.Readonly = !value;
		}
	} = false;
	
	public override void _Ready()
	{
		GroundLayer = GetNode<TileMapLayer>("GroundLayer") as Layers.GroundLayer;
		DecorationLayer = GetNode<TileMapLayer>("DecorationLayer") as Layers.DecorationLayer;
		EntityLayer = GetNode<Node2D>("EntityLayer") as Layers.EntityLayer;
		Selector = GetNode<Node2D>("Selector") as Mapping.Selector;
		Camera = GetNode<Camera2D>("Camera") as MapCamera;
		
		TileSize = GroundLayer.TileSet.TileSize.X;
		Selector.Hovered += (Vector2I cellPos) => { CurrentCellPos = cellPos; };
		
		Camera.Calibrate(GroundLayer);
		Camera.DraggingChange += (MapCamera.DraggingState draggingState) => {
			switch(draggingState)
			{
				case MapCamera.DraggingState.NONE:
					Input.SetDefaultCursorShape(Input.CursorShape.Cross); 
					Selector.Locked = false;
					break;
				case MapCamera.DraggingState.HORIZONTAL:
				case MapCamera.DraggingState.VERTICAL:
				case MapCamera.DraggingState.ALL:
					Selector.Locked = true;
					Input.SetDefaultCursorShape(Input.CursorShape.Move); break;
			}
		};
		Camera.ZoomingChange += (double factor) => {
			Selector.Reposition();
		};
		
		Input.SetDefaultCursorShape(Input.CursorShape.Cross);
	}
}
