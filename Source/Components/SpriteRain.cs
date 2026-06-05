using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;

namespace Drumstalotajs.Components;

public partial class SpriteRain : CanvasLayer
{
	[Export] public Texture2D[] Sprites = [];
	[Export] public Vector2 Direction = Vector2.Down;
	[Export] public int Limit = 100;
	[Export] public bool Activated { get; set; } = true;
	[Export] public Rect2 Rect { get; private set; } = new Rect2();
	
	private List<Sprite2D> _instances = new();
	private bool _isViewported = false;
	
	public override void _Ready()
	{
		if (Rect.Size == Vector2.Zero)
		{
			SetRect(GetViewport().GetVisibleRect(), true);
		}
		GetWindow().SizeChanged += () => {
			if (_isViewported)
			{
				SetRect(GetViewport().GetVisibleRect(), true);
			}
		};
	}

	public override void _Process(double delta)
	{
		if (_instances.Count <= Limit && Activated)
		{
		}
	}
	
	public void SetRect(Rect2 rect, bool viewported)
	{
		Rect = rect;
		_isViewported = viewported;
	}
}
