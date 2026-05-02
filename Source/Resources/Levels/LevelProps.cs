using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Resources;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Resources.Mapping.Layers;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Resources.Levels;

[GlobalClass]
public partial class LevelProps : Resource
{
	[Export] public string Name { get; set; } = "";
	[Export(PropertyHint.MultilineText)] public string Desc { get; set; } = "";
	[Export] public int Order { get; set; } = 0;
	[Export] public LevelType Type { get; set; } = LevelType.Dugout;
	[Export] public Vector2I InMapPosition { get; set; }
	[Export(PropertyHint.File, "*.tres,*.res")] public string MapPath { get; set; } = "";
	
	public OverlayLayerTileData GetTileData(bool unlocked = false)
	{
		OverlayLayerTileData data = new OverlayLayerTileData();
		data.Id = 1;
		data.Position = InMapPosition;
		var meta = new Dictionary();
		meta["Order"] = Order;
		meta["Type"] = (int)Type;
		meta["Unlocked"] = unlocked;
		data.Data = meta;
		return data;
	}
}
