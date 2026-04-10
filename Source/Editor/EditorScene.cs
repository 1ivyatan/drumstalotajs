using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export] public Map Map { get; private set; }
	public EditorMode Mode { get; private set; } = EditorMode.View;
	
	[Export] private Topnav _topnav;
	[Export] private TileSelectionContainer _tileSelectionContainer;
	
	public override void _Ready()
	{
		LayerBase[] layers = [ Map.GroundLayer ];
		Map.Mode = MapMode.Edit;
		_tileSelectionContainer.Load(layers);
		_topnav.SetTitle("Editor");
		_topnav.SelectedExit += () => { Nodes.GetRoot().SceneManager.Start(); };
		_topnav.SelectedCalibration += () => {
			Map.Camera.Calibrate();
		};
		_topnav.SelectedMode += (EditorMode mode) => { 
			Mode = mode;
			switch (mode)
			{
				case EditorMode.View:
					_tileSelectionContainer.Visible = false;
					break;
				case EditorMode.Edit:
					_tileSelectionContainer.Visible = false;
					break;
				case EditorMode.Insert:
					_tileSelectionContainer.Visible = true;
					break;
				default: break;
			}
		};
		
		/*cccccc*/
		Map.Camera.SetCalibratingAtlasLayer(Map.GroundLayer);
		Map.Camera.Mode = CameraMode.View;
		Map.Camera.Calibrate();
	}
}
