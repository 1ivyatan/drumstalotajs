using Godot;
using System;

namespace drumstalotajs.Battle.TopPanels;

public partial class Counters : Control
{
	private Components.Counteds.DisplayCounter deviceCounter;
	
	public override void _Ready()
	{
		deviceCounter = GetNode<Control>("DeviceCounter") as Components.Counteds.DisplayCounter;
	}
	
	public void AddDevices()
	{
		
	}
}
