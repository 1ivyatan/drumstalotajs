using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export] public Map Map { get; private set; }
	[Export] public EditorTopnav EditorTopnav { get; private set; }
	
	public override void _Ready()
	{
	//	var test = new OverlayLayerTileData();
		//test.Id = 1;
		//test.Azimuth = 1;
		//test.Position = new Vector2I(5, 5);
		//OverlayLayer.AddTile(test);
		UpdateTitle();
	}
	
	private void UpdateTitle()
	{
		EditorTopnav.Title = "Untitled";
	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		//if (@event is InputEventMouseButton)
		//{
	//		var ins = OverlayLayer.GetInstance(new Vector2I(5, 5));
			//if (ins != null)
			//{
				
			//	Export();
			//}
		//}
	}
}
