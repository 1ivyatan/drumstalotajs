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

namespace Drumstalotajs.LevelSelection.Components;

public partial class LevelMetaContainer : Control
{
	[Export] private Map _map;
	[Export] private Container _levelMeta;
	[Export] private Label _title;
	[Export] private RichTextLabel _desc;
	[Export] private Button _loadCustomMap;
	[Export] private FileDialog _customMapDialog;
	private LevelProps _selectedLevel = null;
	
	public override void _Ready()
	{
		Utilities.Editor.EditorControl(_loadCustomMap);
		if (Utilities.Editor.IsEditor())
		{
			_customMapDialog.FileSelected += (string path) => {
				GD.Print(path);
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
			var sceneRoot = (LevelSelectionScene)Nodes.GetSceneRoot();
			var levelSet = sceneRoot.LevelSet;
			var level = levelSet.Levels.FirstOrDefault(l => l.Order == (int)tile.Data["Order"]);
			if (level != null && _selectedLevel == level)
			{
				Close();
			} else if (level != null)
			{
				_selectedLevel = level;
				_title.Text = level.Name;
				_desc.Text = level.Desc;
				_levelMeta.Visible = true;
			}
		} else Close();
	}
	
	public void Close()
	{
		_levelMeta.Visible = false;
		_selectedLevel = null;
	}
}
