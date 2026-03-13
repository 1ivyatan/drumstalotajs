using Godot;
using System;

namespace drumstalotajs.Mapping;

public partial class Map : Node2D
{
	public Layers.GroundLayer GroundLayer { get; private set; }
	public Mapping.Selector Selector { get; private set; }
	public MapCamera Camera { get; private set; }
	public int TileSize { get; private set; }
	public Vector2I CurrentCellPos { get; private set; }
	
	public override void _Ready()
	{
		GroundLayer = GetNode<TileMapLayer>("GroundLayer") as Layers.GroundLayer;
		Selector = GetNode<Node2D>("Selector") as Mapping.Selector;
		Camera = GetNode<Camera2D>("Camera") as MapCamera;
		TileSize = GroundLayer.TileSet.TileSize.X;
		Selector.Hovered += (Vector2I cellPos) => { CurrentCellPos = cellPos; };
		Camera.Calibrate(GroundLayer);
		Input.SetDefaultCursorShape(Input.CursorShape.Cross);
	}
}
