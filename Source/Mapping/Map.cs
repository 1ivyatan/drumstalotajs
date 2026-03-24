using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class Map : Node2D
{
	[Export] private Resources.Maps.Meta metaData;
	public int TileSize { get; private set; }
	public Layers.GroundLayer GroundLayer { get; private set; }
	public Layers.DecorationLayer DecorationLayer { get; private set; }
	public Layers.EntityLayer EntityLayer { get; private set; }
	public Mapping.Selection.Selector Selector { get; private set; }
	public Camera.MapCamera Camera { get; private set; }
	
	//public Vector2I CurrentCellPos { get; private set; }
	
	public override void _Ready()
	{
		GroundLayer = GetNode<TileMapLayer>("GroundLayer") as Layers.GroundLayer;
		DecorationLayer = GetNode<TileMapLayer>("DecorationLayer") as Layers.DecorationLayer;
		EntityLayer = GetNode<Node2D>("EntityLayer") as Layers.EntityLayer;
		Selector = GetNode<Node2D>("Selector") as Mapping.Selection.Selector;
		Camera = GetNode<Camera2D>("Camera") as Camera.MapCamera;
		TileSize = GroundLayer.TileSet.TileSize.X;
		
		//EntityLayer
		
		//EntityEnteredEventHandler(Entities.Entity entity);
		
	//	Selector.HoveredGround += (Vector2I cellPos) => { CurrentCellPos = cellPos; };
		/*Camera.DraggingChange += (MapCamera.DraggingState draggingState) => {
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
		
		if (metaData != null)
		{
			LoadMap(metaData);
		} else
		{
			Loaded = true;
		}*/
	}
	
	public Resources.Maps.Map ExportMap()
	{
		return null;
	}
	
	public void LoadMap(Resources.Maps.Meta metaData)
	{
		Loaded = false;
		Resources.Maps.Map mapData = metaData.LoadMap(); 
		GroundLayer.LoadLayer(mapData.GroundLayer);
		DecorationLayer.LoadLayer(mapData.DecorationLayer);
		EntityLayer.LoadLayer(mapData.EntityLayer);
		Camera.Calibrate(GroundLayer);
		Loaded = true;
	}
}
