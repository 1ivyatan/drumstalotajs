using Godot;
using System;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	private Timer SetMovementTimer(double timeout)
	{
		Timer timer = new Timer();
		timer.WaitTime = timeout;
		timer.OneShot = true;
		return timer;
	}
	
	private void ResetMovementTimer(Timer timer)
	{
		timer.Stop();
		timer.Start();
	}
}
