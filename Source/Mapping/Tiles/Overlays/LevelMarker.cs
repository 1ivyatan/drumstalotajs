using Godot;
using System;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Mapping.Tiles.Overlays;

public partial class LevelMarker : OverlayTile, IInitializer<LevelSetProps>
{
	public LevelSetProps Props { get; private set; }
	
	public void Initialize(LevelSetProps props)
	{
		Props = props;
	}
}
