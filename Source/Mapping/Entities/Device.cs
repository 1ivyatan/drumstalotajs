using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Device : Entity
{	
	[Export] private Sprite2D _body;
	[Export] private Sprite2D _head;
	[Export] private Sprite2D _smoke;
	[Export] private Flag _flag;
	[Export] private Status _status;
	[Export] private Damage _damage;
	[Export] private AudioStreamPlayer _shootSfx;
	
	[Export] public override EntityPropertiesData Properties { get; 
		set
		{
			field = value;
			if (field != null && field is DevicePropertiesData deviceProps)
			{
				_body.Texture = deviceProps.DeviceBody;
				_head.Texture = deviceProps.DeviceHead;
				_head.Position = deviceProps.DeviceHeadPosition;
				_smoke.Offset = new Vector2(0, -22.5f);
			}
		}
	}
	
	public override double Azimuth { get;
		set {
			field = ((value % 360) + 360) % 360;
			_body.RotationDegrees = (float)field;
		}
	} = -1;
	
	public override bool Player { get;
		set {
			field = value;
			_flag.SetFlag(field, Disabled);
		}
	} = false;
	
	public double Traverse { get;
		set {
			field = value;
			_head.RotationDegrees = (float)field;
		}
	} = 0;
	
	public double Angle { get; set; } = -1;
	public int Shells { get; set; } = 0;
	public int ResupplyTurns { get; private set; } = 0;
	public int ShellsPerTurn { get; set; } = 1;
	
	public override double Integrity { get; 
		set { 
			var old = field;
			field = Mathf.Clamp(value, 0, 100);
			if (field <= 0) Disabled = true;
			else
			{
				Disabled = false;
				if (old > field) _damage.Pulse(old - field);
			} 
		}
	} = 100;
	
	
	public override bool Target { 
		get; 
		set {
			field = value;
			if (!Player && !field)
			{
				_flag.Visible = false;
			} else
			{
				_flag.Visible = true;
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
			if (field)
			{
				_head.Visible = false;
				if ((Properties) != null)
				{
					_body.Texture = ((DevicePropertiesData)Properties).DestroyedDeviceBody;
				}
				_flag.SetFlag(Player, true);
				_status.DisabledIcon();
			} else
			{
				_head.Visible = true;
				if ((Properties) != null)
				{
					_body.Texture = ((DevicePropertiesData)Properties).DeviceBody;
				}
				_flag.SetFlag(Player, false);
				if (Shells > 0)
				{
					_status.HideIcon();
				} else
				{
					_status.ResupplyIcon();
				}
			}
		}
	}
	public void Resupply(int amount)
	{
		if (amount > 0)
		{
			Shells = amount;
			ResupplyTurns = 0;
			_status.HideIcon();
		}
	}
	
	public void Resupply()
	{
		Shells = ((DevicePropertiesData)Properties).Shells;
		ResupplyTurns = 0;
		_status.HideIcon();
	}
	
	public void ExpendShell()
	{
		if (!Disabled)
		{
			Shells--;
			_shootSfx.Play();
			if (Shells == 0)
			{
				ResupplyTurns = ((DevicePropertiesData)Properties).ResupplyTurns;
				_status.ResupplyIcon();
			}
			_smoke.Visible = true;
			SceneTreeTimer smokeTimer = GetTree().CreateTimer(.125f);
			smokeTimer.Connect(SceneTreeTimer.SignalName.Timeout , Callable.From(() => {
				_smoke.Visible = false;
			}));
		}
	}
	
	public void CheckAndTryResupply()
	{
		if (Shells <= 0)
		{
			if (ResupplyTurns == 0)
			{
				ResupplyTurns = ((DevicePropertiesData)Properties).ResupplyTurns;
				_status.ResupplyIcon();
			} else
			{
				ResupplyTurns--;
				if (ResupplyTurns == 0)
				{
					Resupply();
				}
			}
		}
	}
}
