using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Mapping.Entities;

public partial class FrameBackground : Sprite2D
{
	[ExportGroup("Texture")]
	[Export] private Texture2D Border { get; set; }
	
	[ExportGroup("Layer")]
	[Export] private AtlasLayer _calibratingAtlasLayer;
	
	[ExportGroup("Margin")]
	[Export] private int BorderLeft { get; set; } = 6;
	[Export] private int BorderTop { get; set; } = 6;
	[Export] private int BorderRight { get; set; } = 6;
	[Export] private int BorderBottom { get; set; } = 6;
	
	[ExportGroup("Padding")]
	[Export] private int PaddingLeft { get; set; } = 6;
	[Export] private int PaddingTop { get; set; } = 6;
	[Export] private int PaddingRight { get; set; } = 6;
	[Export] private int PaddingBottom { get; set; } = 6;
	
	private bool _draw = false;
	private Rect2 _drawRect;
	private StyleBoxTexture _styleBox;
	
	public void Calibrate()
	{
		Rect2 usedRect = _calibratingAtlasLayer.GetUsedRect();
		int tileSize = _calibratingAtlasLayer.TileSize;
		
		var baseWidth = (int)(tileSize * usedRect.Size.X);
		var baseHeight = (int)(tileSize * usedRect.Size.Y);
		Vector2 topLeft = (new Vector2(usedRect.Position.X, usedRect.Position.Y)) * (float)tileSize;
		
		var finWidth = baseWidth + PaddingLeft + PaddingRight;
		var finHeight = baseHeight + PaddingTop + PaddingBottom;
		Vector2 padded = new Vector2(topLeft.X - PaddingLeft, topLeft.Y - PaddingTop);
		
		_styleBox = new StyleBoxTexture();
		_styleBox.Texture = Border;
		_styleBox.TextureMarginLeft = BorderLeft;
		_styleBox.TextureMarginTop = BorderTop;
		_styleBox.TextureMarginRight = BorderRight;
		_styleBox.TextureMarginBottom = BorderBottom;
		
		_drawRect = new Rect2(padded, new Vector2(finWidth, finHeight));
		_draw = true;
		QueueRedraw();
	}
	
	public override void _Draw()
	{
		if (_draw && _styleBox != null)
		{
			DrawStyleBox(_styleBox, _drawRect);
			GD.Print(_drawRect);
		}
	}
}
