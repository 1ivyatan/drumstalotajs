using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Wall : Entity
{
	[Export] private Sprite2D _sprite;
	
	[Export] public override EntityPropertiesData Properties { get; 
		set
		{
			field = value;
			SetSprite();
		}
	}
	
	public override double Integrity { get; 
		set { 
			field = Mathf.Clamp(value, 0, 100);
			SetSprite();
			if (field <= 0) Disable();
		}
	} = 100;
	
	private void SetSprite(bool disabled = false)
	{
		if (Properties != null && Properties is WallPropertiesData wallProps)
		{
			if (((WallPropertiesData)Properties).IntegrityStages != null)
			{
				var sprites = ((WallPropertiesData)Properties).IntegrityStages;
				if (sprites.Length == 0) return;
				if (disabled)
				{
					_sprite.Texture = sprites[^1];
				} else
				{
					int length = sprites.Length;
					int index = GetSpriteIndex(Integrity, length);
					_sprite.Texture = sprites[index];
				}
			}
		}
	}
	
	private int GetSpriteIndex(double integrity, int spriteCount)
	{
		double t = 1f - Math.Clamp(integrity, 0f, 100f) / 100f;
		return (int)Math.Min(Math.Floor(t * (spriteCount - 1)), spriteCount - 1);
	}
	
	public override void Disable()
	{
		Disabled = true;
		SetSprite(true);
	}
	//public double Height { get; private set; } = 0;
	
	//public override void _Ready()
	//{
	//	if (Properties != null && Properties is WallPropertiesData wallProperties)
	//	{
	//		Height = wallProperties.Height;
	//	}
	//}
}
