using Godot;
using System;

namespace drumstalotajs.Battle.TopPanels;

public partial class Counters : Control
{
	private Components.Counter deviceCounter;
	
	public override void _Ready()
	{
		deviceCounter = GetNode<Control>("DeviceCounter") as Components.Counter;
	}
	
	

	public override void _Process(double delta)
	{
	}
}
