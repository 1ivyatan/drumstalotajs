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
	[Export] private FileDialog _openDialog;
	[Export] private EditorSaveManager EditorSaveManager { get; set; }
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

		_topnav.SelectedExport += Save;
		_topnav.SelectedNew += LoadNew;
		_topnav.SelectedOpen += Open;
		EditorSaveManager.SaveLoaded += (string name) => {
			SetTitle();
			_edited = false;
		};
		Map.Edited += () => { 
			SetTitle();
			_edited = true;
		};
		LoadNew();
		/*
			picker.SelectedTile += (PickedTileData pickedTile) => {*/
		/*cccccc*/
	}
	
	private void Open()
	{
		EditorSaveManager.OpenPrompt();
	}
	
	private void Save()
	{
		EditorSaveManager.SavePrompt();
	}
	
	private void SetTitle()
	{
		_topnav.SetTitle($"Editor - {EditorSaveManager.SaveName}");
	}
	
	private void LoadNew()
	{
		if (_edited)
		{
			Nodes.GetRoot().ToastManager.Spawn("Not exported, please export.");
			return;
		} else
		{
			Map.Load(EditorSaveManager.TemplateMap);
			SetTitle();
			_edited = false;
		}
	}
}
