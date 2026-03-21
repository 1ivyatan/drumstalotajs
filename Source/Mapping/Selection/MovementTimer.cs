using Godot;
using System;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	private Timer movementTimer;
	
	private Timer SetMovementTimer(double timeout, Action timeoutAction)
	{
		Timer timer = new Timer();
		timer.WaitTime = timeout;
		timer.OneShot = true;
		timer.Timeout += timeoutAction;
		return timer;
	}
	
	private void StartMovementTimer(Timer timer)
	{
		timer.Stop();
		timer.Start();
	}
}
