using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Entities;

public partial class FrameBackground : Sprite2D
{
	[Export] private AtlasLayer _calibratingAtlasLayer;

	public void Calibrate()
	{
		Rect2 usedRect = _calibratingAtlasLayer.GetUsedRect();
		int tileSize = _calibratingAtlasLayer.TileSize;
		GD.Print(usedRect);
		/*
		Rect2 usedRect = _calibratingAtlasLayer.GetUsedRect();
		int tileSize = _calibratingAtlasLayer.TileSize;
		LimitLeft = (int)(usedRect.Position.X * tileSize);
		LimitRight = (int)((usedRect.Position.X + usedRect.Size.X) * tileSize);
		LimitTop = (int)(usedRect.Position.Y * tileSize); //- (int)();
		LimitBottom = (int)((usedRect.Size.Y * tileSize) + (usedRect.Position.Y * tileSize));
		GlobalPosition = usedRect.Position + usedRect.GetCenter() * _calibratingAtlasLayer.TileSize;*/
	}
}
