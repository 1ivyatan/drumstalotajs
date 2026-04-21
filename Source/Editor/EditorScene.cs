using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Selection;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export] public Map Map { get; private set; }
	[Export] public EditorTopnav EditorTopnav { get; private set; }
	[Export] private EditorSaveManager EditorSaveManager { get; set; }
	
	public EditorMode Mode { get;
		set {
			field = value;
			switch (field)
			{
				case EditorMode.View:
					Map.Camera.Mode = CameraMode.DragOnly;
					Map.Selector.Mode = SelectorMode.Invisible;
					break;
				case EditorMode.Insert:
					Map.Camera.Mode = CameraMode.Locked;
					Map.Selector.Mode = SelectorMode.Visible;
					break;
				case EditorMode.Edit:
					Map.Camera.Mode = CameraMode.DragOnly;
					Map.Selector.Mode = SelectorMode.Visible;
					break;
				default: break;
			}
			/* topnav change vvvv */
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
		EditorTopnav.Title = "Editor";
		Map.Mode = MapMode.Editing;
		Mode = EditorMode.View;
	}
	
	private void Save()
	{
		EditorSaveManager.SaveDialog();
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
