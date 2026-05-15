using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping.Tiles;
using Drumstalotajs.Resources.Mapping.Entities;

namespace Drumstalotajs.Mapping.Entities;

public partial class Damage : Node2D
{
	[Export] private Label _label;
	[Export] private Timer _timer;
	
	public override void _Ready()
	{
		Visible = false;
		_timer.Timeout += () => { Visible = false; };
	}
	
	public void Pulse(double count)
	{
		_label.Text = $"-{Math.Round(count, 2)}";
		Visible = true;
		_timer.Start();
	}
}
