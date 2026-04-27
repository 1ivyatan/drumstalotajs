using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
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
	[Export] private InsertWindow InsertWindow { get; set; }
	[Export] private EditWindow EditWindow { get; set; }
	
	public EditorMode Mode { get;
		set {
			field = value;
			switch (field)
			{
				case EditorMode.View:
					InsertWindow.Hide();
					EditWindow.Hide();
					Map.Camera.Mode = CameraMode.DragOnly;
					Map.Selector.Mode = SelectorMode.Invisible;
					break;
				case EditorMode.Insert:
					Map.Camera.Mode = CameraMode.Locked;
					Map.Selector.Mode = SelectorMode.Editing;
					EditWindow.Hide();
					InsertWindow.Show();
					break;
				case EditorMode.Edit:
					InsertWindow.Hide();
					EditWindow.Show();
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
		
		BaseLayer[] layers = [ Map.GroundLayer, Map.DecorationLayer, Map.OverlayLayer ];
		Map.Selector.Filter = new SelectorFilter(layers);
		
		InsertWindow.LoadTiles(layers);
		InsertWindow.CloseRequested += () => { Mode = EditorMode.View; };
		EditWindow.CloseRequested += () => { Mode = EditorMode.View; };
		
		EditorTopnav.SelectedNew += () => { EditorSaveManager.AttemptNew(); };
		EditorTopnav.SelectedOpen += () => { EditorSaveManager.AttemptOpen(); };
		EditorTopnav.SelectedSave += () => { EditorSaveManager.AttemptSave(); };
		EditorTopnav.SelectedSaveAs += () => { EditorSaveManager.AttemptSaveAs(); };
		EditorTopnav.SelectedExport += () => { EditorSaveManager.AttemptExport(); };
		EditorTopnav.SelectedCameraCalibrate += () => { Map.Camera.Calibrate(); };
		EditorTopnav.SelectedMode += (EditorMode mode) => { Mode = mode; };
		EditorTopnav.SelectedClose += () => { Nodes.GetRoot().SceneManager.Start(); };
		
		EditorSaveManager.Changed += () => {
			SetTitle(true);
		};
		EditorSaveManager.Saved += () => {
			SetTitle(false);
		};
		EditorSaveManager.Loaded += () => {
			SetTitle(false);
		};
		
		EditorTopnav.Title = "Editor";
		Map.Mode = MapMode.Editing;
		Mode = EditorMode.View;
		
		EditorSaveManager.AttemptNew();
	}
	
	private void SetTitle(bool edited)
	{
		EditorTopnav.Title = EditorSaveManager.SaveName + (edited ? "*" : "");
	}
}
