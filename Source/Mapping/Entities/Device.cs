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
	[Export] private Flag _flag;
	[Export] private Status _status;
	
	[Export] public override EntityPropertiesData Properties { get; 
		set
		{
			field = value;
			if (field != null && field is DevicePropertiesData deviceProps)
			{
				_body.Texture = deviceProps.DeviceBody;
				_head.Texture = deviceProps.DeviceHead;
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
			_flag.SetFlag(field);
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
	
	public override void _Ready()
	{
		Resupply();
	}
	
	public override void Disable()
	{
		Disabled = true;
		_head.Visible = false;
		_body.Texture = ((DevicePropertiesData)Properties).DestroyedDeviceBody;
		_status.DisabledIcon();
		_flag.SetFlag(Player, true);
	}
	
	private void Resupply()
	{
		Shells = ((DevicePropertiesData)Properties).Shells;
		ResupplyTurns = 0;
		_status.HideIcon();
	}
	
	public void ExpendShell()
	{
		Shells--;
		if (Shells == 0)
		{
			ResupplyTurns = ((DevicePropertiesData)Properties).ResupplyTurns;
			_status.ResupplyIcon();
		}
	}
	
	public void CheckAndTryResupply()
	{
		if (Shells == 0)
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
