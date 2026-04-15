using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export(PropertyHint.Dir)] private string _exportPath;
	[Export(PropertyHint.File, "*.tres")] private string _emptyMapPath;
	[Export] public Map Map { get; private set; }
	
	private bool _edited = false;
	
	public EditorMode Mode { get; 
		private set {
			field = value;
			switch (value)
			{
				case EditorMode.View:
					Map.Camera.Mode = CameraMode.View;
					_tileSelectionContainer.Visible = false;
					break;
				case EditorMode.Edit:
					Map.Camera.Mode = CameraMode.View;
					_tileSelectionContainer.Visible = false;
					break;
				case EditorMode.Insert:
					Map.Camera.Mode = CameraMode.Lock;
					_tileSelectionContainer.Visible = true;
					break;
				default: break;
			}
		}
	} = EditorMode.View;
	
	[Export] private Topnav _topnav;
	[Export] private TileSelectionContainer _tileSelectionContainer;
	[Export] private TileEditingContainer _tileEditingContainer;
	
	public override void _Ready()
	{
		LayerBase[] layers = [ Map.GroundLayer ];
		Map.Mode = MapMode.Edit;
		Map.Camera.SetCalibratingAtlasLayer(Map.GroundLayer);
		Map.Camera.Mode = CameraMode.View;
		Map.Selector.Filter = new SelectorFilter(layers);
		_tileSelectionContainer.Load(layers);
		_topnav.SetTitle("Editor");
		_topnav.SelectedExit += () => { Nodes.GetRoot().SceneManager.Start(); };
		_topnav.SelectedCalibration += () => {
			Map.Camera.Calibrate();
		};
		_topnav.SelectedMode += (EditorMode mode) => { 
			Mode = mode;
		};
		_topnav.SelectedExport += Export;
		_topnav.SelectedNew += LoadNew;
		Map.Edited += () => { _edited = true; };
		LoadNew();
		
		/*
			picker.SelectedTile += (PickedTileData pickedTile) => {*/
		/*cccccc*/
	}
	
	private void Export()
	{
		string path = $"{_exportPath}/Map.tres";
		var export = Map.Export();
		ResourceSaver.Save(export, path);
		Nodes.GetRoot().ToastManager.Spawn($"Done exporting, file is {path}");
		_edited = false;
	}
	
	private void LoadNew()
	{
		if (_edited)
		{
			Nodes.GetRoot().ToastManager.Spawn("Not exported, please export.");
			return;
		} else
		{
			_edited = false;
			Map.Load(_emptyMapPath);
		}
	}
}
