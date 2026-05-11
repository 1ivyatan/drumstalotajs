using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Mapping.Entities;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Mapping.Overlays;

public partial class LevelMarker : OverlayTile
{
	[Export] private Texture2D[] _buildings;

	[Export] private Sprite2D _building;
	[Export] private Flag _flag;

	public override Dictionary Data { get; 
		set {
			field = value;
			_building.Texture = _buildings[(int)field["Type"]];
			_flag.SetFlag((bool)field["Unlocked"]);
		}
	} = new();
}

/*namespace Drumstalotajs.Resources.Levels;

public enum LevelType
{
	Dugout, Road, Camp, Battlefield, Depo, Fort, Outpost, Base
}
*/
