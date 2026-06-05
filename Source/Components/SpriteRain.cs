using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Components;

public partial class SpriteRain : CanvasLayer
{
	[Export] public Texture2D[] Sprites = [];
	[Export] public Vector2 Direction = Vector2.Down;
	[Export] public int Limit = 100;
	[Export] public double Delay = 1.1;
	[Export] public double Speed = 100;
	[Export] public bool Activated { get; set; } = true;
	[Export] public Rect2 Rect { get; private set; } = new Rect2();
	
	private List<Sprite2D> _instances = new();
	private bool _isViewported = false;
	private bool _stopSpawn = false;
	private Rect2 _rectBounds = new Rect2();
	
	public override void _Ready()
	{
		GetWindow().SizeChanged += () => {
			if (_isViewported)
			{
				SetRect(GetViewport().GetVisibleRect(), true);
			}
		};
		if (Rect.Size == Vector2.Zero)
		{
			SetRect(GetViewport().GetVisibleRect(), true);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_instances.Count <= Limit && Activated && Sprites.Length > 0 && !_stopSpawn)
		{
			Spawn();
			_stopSpawn = true;
			SceneTreeTimer delay = GetTree().CreateTimer((float)Delay, false);
			delay.Connect(SceneTreeTimer.SignalName.Timeout, Callable.From(() => {
				_stopSpawn = false;
			}));
		}
		
		List<Sprite2D> slammer = new();
		foreach (var instance in _instances)
		{
			var newPos = instance.Position + Direction * ((float)delta * (float)Speed);
			var fade = GetFadeValue(newPos);
			var color = instance.Modulate;
			color.A = (float)fade;
			instance.Modulate = color;
			instance.Position = newPos;
			if (!_rectBounds.HasPoint(newPos))
			{
				slammer.Add(instance);
			}
		}
		
		foreach (var instance in slammer)
		{
			_instances.Remove(instance);
			RemoveChild(instance);
		}
	}
	
	private double GetFadeValue(Vector2 pos)
	{
		if (Rect.HasPoint(pos)) return 1.0f;
		
		double distanceX = 0;
		if (pos.X < Rect.Position.X) distanceX = Rect.Position.X - pos.X;
		else if (pos.X > Rect.End.X) distanceX = pos.X - Rect.End.X;
		
		double distanceY = 0;
		if (pos.Y < Rect.Position.Y) distanceY = Rect.Position.Y - pos.Y;
		else if (pos.Y > Rect.End.Y) distanceY = pos.Y - Rect.End.Y;
		
		double distance = Mathf.Max(distanceX, distanceY);
		double maxFadeZoneWidth = (_rectBounds.Size.X - Rect.Size.X) / 2f;
		return 1.0f - Mathf.Clamp(distance / maxFadeZoneWidth, 0f, 1f);
	}
	
	public void SetRect(Rect2 rect, bool viewported)
	{
		Rect = rect;
		_isViewported = viewported;
		_rectBounds = Rect.Grow(50);
		FollowViewportEnabled = !_isViewported;
	}
	
	public void SpawnAllOverRandomly()
	{
		float range = GD.RandRange(1, Limit / 5);
		for (int i = 0; i < (int)(range) ; i++)
		{
			var point = Calculations.GetRandomPoint(Rect);
			Spawn(point);
		}
	}
	
	private void Spawn(Vector2 pos)
	{
		var randSprite = Sprites[(int)GD.RandRange(0, Sprites.Length - 1)];
		Sprite2D sprite = new Sprite2D();
		sprite.Texture = randSprite;
		sprite.Position = pos;
		_instances.Add(sprite);
		AddChild(sprite);
	}
	
	private void Spawn()
	{
		var randSprite = Sprites[(int)GD.RandRange(0, Sprites.Length - 1)];
		var pos = GetRandomSpawnPoint(randSprite);
		Sprite2D sprite = new Sprite2D();
		sprite.Texture = randSprite;
		sprite.Position = pos;
		_instances.Add(sprite);
		AddChild(sprite);
	}
	
	private Vector2 GetRandomSpawnPoint(Texture2D sprite)
	{
		if (Direction == Vector2.Zero) return Rect.Position;
		
		var normalized = Direction.Normalized();
		var padding = (sprite.GetWidth() / 2) + 10;
		
		if (Mathf.IsEqualApprox(Mathf.Abs(normalized.X), Mathf.Abs(normalized.Y)))
		{
			float targetX = (normalized.X > 0) ? Rect.Position.X - padding : Rect.End.X + padding;
			float targetY = (normalized.Y > 0) ? Rect.Position.Y - padding : Rect.End.Y + padding;
			return new Vector2(targetX, targetY);
		}
		
		if (Mathf.Abs(normalized.X) > Mathf.Abs(normalized.Y))
		{
			var randomY = GD.RandRange(
				Rect.Position.Y - padding,
				Rect.End.Y + padding
			);
			var targetX = (normalized.X > 0)
				? Rect.Position.X - padding
				: Rect.End.X + padding
			;
			return new Vector2((float)targetX, (float)randomY);
		} else
		{
			var randomX = GD.RandRange(
				Rect.Position.X - padding,
				Rect.End.X + padding
			);
			var targetY = (normalized.Y > 0)
				? Rect.Position.Y - padding
				: Rect.End.Y + padding
			;
			return new Vector2((float)randomX, (float)targetY);
		}
	}
}
