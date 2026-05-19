using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Entities;

public partial class FrameBackground : Control
{
	[Export] private AtlasLayer _calibratingAtlasLayer;
	[Export] private Vector2 Padding { get; set; }

	public void Calibrate()
	{
		Rect2 usedRect = _calibratingAtlasLayer.GetUsedRect();
		int tileSize = _calibratingAtlasLayer.TileSize;
		var width = tileSize * usedRect.Size.X;
		var height = tileSize * usedRect.Size.Y;
		Size = new Vector2(width, height) + Padding;
		Position = (usedRect.Position * tileSize) - (Padding / 2);
	}
}
