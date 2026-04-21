using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor.Components;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export] public Map Map { get; private set; }
	[Export] public EditorTopnav EditorTopnav { get; private set; }
	public EditorMode Mode { get;
		set {
			field = value;
			switch (field)
			{
				case EditorMode.View:
					
					break;
				case EditorMode.Insert:
					
					break;
				case EditorMode.Edit:
					
					break;
				default: break;
			}
		}
	}
	
	public override void _Ready()
	{
	//	var test = new OverlayLayerTileData();
		//test.Id = 1;
		//test.Azimuth = 1;
		//test.Position = new Vector2I(5, 5);
		//OverlayLayer.AddTile(test);
		EditorTopnav.SelectedSave += Save;
		UpdateTitle(); //!!!!!!!
		Mode = EditorMode.View;
	}
	
	private void Save()
	{
		
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
