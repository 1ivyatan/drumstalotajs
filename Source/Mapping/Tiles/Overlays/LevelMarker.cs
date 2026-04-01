using Godot;
using System;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Mapping.Tiles.Overlays;

public partial class LevelMarker : OverlayTile//, IInitializer<int>
{
	public void Init()
	{
		GD.Print(3333);
	}
	
	public void Initialize(LevelSetProps props)
	{
		GD.Print(props.Order);
	}
}
