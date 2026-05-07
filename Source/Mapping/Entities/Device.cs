using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Device : Entity
{
	public double Angle { get; set; } = -1;
	public double Traverse { get; set; } = 0;
	public int Shells { get; set; } = 0;
	
	private int _resupplyTurnTracker = 0;
	
	public override void _Ready()
	{
		Resupply();
	}
	
	private void Resupply()
	{
		Shells = ((DevicePropertiesData)Properties).Shells;
		_resupplyTurnTracker = 0;
	}
	
	public void ExpendShell()
	{
		Shells--;
	}
	
	public void CheckAndTryResupply()
	{
		if (Shells == 0)
		{
			if (_resupplyTurnTracker == 0)
			{
				_resupplyTurnTracker = ((DevicePropertiesData)Properties).ResupplyTurns;
			} else
			{
				_resupplyTurnTracker--;
				if (_resupplyTurnTracker == 0)
				{
					Resupply();
				}
			}
		}
	}
}
