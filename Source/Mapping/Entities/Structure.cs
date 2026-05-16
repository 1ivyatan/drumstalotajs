using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Structure : Entity
{
	[Export] private Flag _flag;
	[Export] private Sprite2D _sprite;
	[Export] private Status _status;
	[Export] private Damage _damage;
	
	[Export] public override EntityPropertiesData Properties { get; 
		set
		{
			field = value;
			SetSprite();
		}
	}
	
	public override double Azimuth { get;
		set {
			field = ((value % 360) + 360) % 360;
			_sprite.RotationDegrees = (float)field;
		}
	} = -1;
	
	public override double Integrity { get; 
		set { 
			var old = field;
			field = Mathf.Clamp(value, 0, 100);
			SetSprite();
			if (field <= 0) Disabled = true;
			else
			{
				Disabled = false;
				if (old > field) _damage.Pulse(old - field);
			} 
		}
	} = 100;
	
	public override bool Player { get;
		set {
			field = value;
			_flag.SetFlag(field, Disabled);
		}
	} = false;

	public override bool Target { 
		get; 
		set {
			field = value;
			_flag.Visible = field;
			if (Disabled && field)
			{
				_status.DisabledIcon();
			} else
			{
				_status.HideIcon();
			}
		}
	} = false;
	
	public override bool Disabled { get;
		set {
			if (value && field != value)
			{
				EmitSignal(SignalName.DisabledEntity);
			}
			field = value;
			SetSprite(field);
			_flag.SetFlag(Player, field);
			if (Target && field)
			{
				_status.DisabledIcon();
			} else
			{
				_status.HideIcon();
			}
		}
	}
	
	private void SetSprite(bool disabled = false)
	{
		if (Properties != null && Properties is StructurePropertiesData wallProps)
		{
			if (((StructurePropertiesData)Properties).IntegrityStages != null)
			{
				var sprites = ((StructurePropertiesData)Properties).IntegrityStages;
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
}
