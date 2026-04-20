using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Resources.Mapping;

[GlobalClass]
public partial class SceneLayerData : LayerData
{
	[Export] public Array<SceneLayerTileData> Tiles;
	
	public SceneLayerData () {}
	public SceneLayerData (SceneLayer layer) {}
}
