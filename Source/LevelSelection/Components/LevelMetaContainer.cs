using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.LevelSelection;

namespace Drumstalotajs.LevelSelection.Components;

public partial class LevelMetaContainer : Control
{
	[Export] private Map _map;
	[Export] private Button _loadCustomMap;
	[Export] private FileDialog _customMapDialog;
	
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
			
			GD.Print(level);
			
			Visible = true;
		} else Close();
	}
	
	public void Close()
	{
		Visible = false;
	}
}
