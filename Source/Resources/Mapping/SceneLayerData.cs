using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class SceneLayerData : LayerData
{
	[Export] public Array<SceneLayerTileData> Tiles;
}
