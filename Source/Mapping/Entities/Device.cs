using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Device : Entity
{
	[Export] private Sprite2D _body;
	[Export] private Sprite2D _head;
	[Export] private Sprite2D _resupplyStatus;
	[Export] private Flag _flag;
	
	public double Angle { get; set; } = -1;
	public int Shells { get; set; } = 0;
	public int ResupplyTurns { get; private set; } = 0;
	
	new public double Azimuth { get;
		set {
			field = ((value % 360) + 360) % 360;
			_body.RotationDegrees = (float)field;
		}
	} = -1;
	
	public double Traverse { get;
		set {
			field = value;
			_head.RotationDegrees = RotationDegrees + (float)field;
		}
	} = 0;
	
	[Export] new public DevicePropertiesData Properties { get;
		set {
			field = value;
			if (field != null)
			{
				if (field.DeviceBody != null) { 
				//	_body.Texture = field.DeviceBody;
				}
				if (field.DeviceHead != null)
				{
					_head.Texture = field.DeviceHead;
					_head.Position = field.DeviceHeadPosition;
				}
				Resupply();
			}
		}
	}
	
	new public bool Player { get; 
		set
		{
			field = value;
			_flag.SetFlag(field);
		}
	} = false;
	
	public override void Disable()
	{
		Disabled = true;
		_head.Visible = false;
		_body.Texture = Properties.DestroyedDevice;
	}
	
	public override void _Ready()
	{
	}
	
	private void Resupply()
	{
		Shells = ((DevicePropertiesData)Properties).Shells;
		_resupplyStatus.Visible = true;
		ResupplyTurns = 0;
	}
	
	public void ExpendShell()
	{
		Shells--;
		if (Shells == 0)
		{
			ResupplyTurns = ((DevicePropertiesData)Properties).ResupplyTurns;
			_resupplyStatus.Visible = false;
		}
	}
	
	public void CheckAndTryResupply()
	{
		if (Shells == 0)
		{
			if (ResupplyTurns == 0)
			{
				ResupplyTurns = ((DevicePropertiesData)Properties).ResupplyTurns;
				_resupplyStatus.Visible = _resupplyStatus.Visible = false;
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
