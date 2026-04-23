using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;

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
					Map.Selector.Mode = SelectorMode.Editing;
					break;
				case EditorMode.Edit:
					Map.Camera.Mode = CameraMode.DragOnly;
					Map.Selector.Mode = SelectorMode.Interactable;
					break;
				default: break;
			}
			EditorTopnav.SetModeMarking(field);
		}
	}
	
	public override void _Ready()
	{
	//	var test = new OverlayLayerTileData();
		//test.Id = 1;
		//test.Azimuth = 1;
		//test.Position = new Vector2I(5, 5);
		//OverlayLayer.AddTile(test);
		
		BaseLayer[] layers = [ Map.GroundLayer, Map.DecorationLayer ];
		Map.Selector.Filter = new SelectorFilter(layers);
		
		EditorTopnav.SelectedNew += () => { EditorSaveManager.AttemptNew(); };
		EditorTopnav.SelectedOpen += () => {};
		EditorTopnav.SelectedSave += () => { /*EditorSaveManager.SaveDialog();*/ };
		EditorTopnav.SelectedSaveAs += () => {};
		EditorTopnav.SelectedProperties += () => {};
		EditorTopnav.SelectedCameraCalibrate += () => {};
		EditorTopnav.SelectedMode += (EditorMode mode) => { Mode = mode; };
		EditorTopnav.SelectedClose += () => {
		};
		
		EditorSaveManager.Changed += () => {
			SetTitle(true);
		};
		EditorSaveManager.Saved += () => {
			SetTitle(false);
		};
		EditorSaveManager.Loaded += () => {
			SetTitle(false);
		};
		//
		
		EditorTopnav.Title = "Editor";
		Map.Mode = MapMode.Editing;
		Mode = EditorMode.View;
		
		/* vvvvvv */
			Map.Camera.Calibrate();
	}
	
	private void SetTitle(bool edited)
	{
		EditorTopnav.Title = EditorSaveManager.SaveName + (edited ? "*" : "");
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
