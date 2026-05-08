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
	
	public int ResupplyTurns {get; private set;} = 0;
	
	public override void _Ready()
	{
		Resupply();
	}
	
	private void Resupply()
	{
		Shells = ((DevicePropertiesData)Properties).Shells;
		ResupplyTurns = 0;
	}
	
	public void ExpendShell()
	{
		Shells--;
		if (Shells == 0)
		{
			ResupplyTurns = ((DevicePropertiesData)Properties).ResupplyTurns;
		}
	}
	
	public void CheckAndTryResupply()
	{
		if (Shells == 0)
		{
			if (ResupplyTurns == 0)
			{
				ResupplyTurns = ((DevicePropertiesData)Properties).ResupplyTurns;
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
