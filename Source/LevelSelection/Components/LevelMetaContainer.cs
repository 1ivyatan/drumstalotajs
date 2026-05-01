using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.LevelSelection;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Managers.Saves;

namespace Drumstalotajs.LevelSelection.Components;

public partial class LevelMetaContainer : Control
{
	[Export] private Map _map;
	[Export] private Container _levelMeta;
	[Export] private Label _title;
	[Export] private RichTextLabel _desc;
	[Export] private Button _playLevel;
	[Export] private Button _loadCustomMap;
	[Export] private FileDialog _customMapDialog;
	private LevelProps _selectedLevel = null;
	private SaveManager _saveManager;
	
	public override void _Ready()
	{
		_saveManager = Nodes.GetRoot().SaveManager;
		Utilities.Editor.EditorControl(_loadCustomMap);
		_playLevel.Pressed += () => {
			var levelSet = GetCurrentLevelSet();
			if (_selectedLevel != null && _saveManager.IsLevelUnlocked(levelSet, _selectedLevel.Order))
			{
				Nodes.GetRoot().SceneManager.Battle(levelSet, _selectedLevel);
			}
		};
		if (Utilities.Editor.IsEditor())
		{
			_customMapDialog.FileSelected += (string path) => {
				var editedPath = ProjectSettings.LocalizePath(path.Replace("\\", "/"));
				Nodes.GetRoot().SceneManager.Battle(editedPath);
			};
			_loadCustomMap.Pressed += () => {
				_customMapDialog.PopupCentered();
			};
		}
	}
	
	public void Load(OverlayTile tile)
	{
		var atlas = _map.OverlayLayer.GetFullAtlas().FirstOrDefault(a => a.Id == tile.TileId);
		if (atlas != null)
		{
			var levelSet = GetCurrentLevelSet();
			var level = levelSet.Levels.FirstOrDefault(l => l.Order == (int)tile.Data["Order"]);
			if (level != null && _selectedLevel == level)
			{
				Close();
			} else if (level != null)
			{
				_selectedLevel = level;
				_title.Text = level.Name;
				_desc.Text = level.Desc;
				_playLevel.Disabled = !_saveManager.IsLevelUnlocked(levelSet, level.Order);
				_levelMeta.Visible = true;
			}
		} else Close();
	}
	
	private LevelSet GetCurrentLevelSet()
	{
		var sceneRoot = (LevelSelectionScene)Nodes.GetSceneRoot();
		return sceneRoot.LevelSet;
	}
	
	public void Close()
	{
		_levelMeta.Visible = false;
		_selectedLevel = null;
	}
}
