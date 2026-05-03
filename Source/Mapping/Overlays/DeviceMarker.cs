using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Layers;
using Drumstalotajs.Mapping.Tiles;

namespace Drumstalotajs.Mapping.Overlays;

public partial class DeviceMarker : OverlayTile
{
	[Export] private Sprite2D _arrow;

	//public override Dictionary Data { get; 
//		set {
//			field = value;
//		}
//	} = new();

	public void SetArrowRotation(double degrees)
	{
		_arrow.RotationDegrees = (float)degrees;
	}
}
