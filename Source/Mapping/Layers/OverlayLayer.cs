using Godot;
using Godot.Collections;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Utils;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping;

namespace Drumstalotajs.Mapping.Layers;

public partial class OverlayLayer : SceneLayer, ISceneLayer<OverlayTile>
{
	new public Array<OverlayTile> Flash(Vector2I position)
	{
		var arr = base.Flash(position);
		return new Array<OverlayTile>(arr.Select(t => t as OverlayTile));
	}
}
