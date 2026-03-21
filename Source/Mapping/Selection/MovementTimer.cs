using Godot;
using System;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	private partial class MovementTimer : Timer
	{
		public void SetTimer(double timeout, Action timeoutAction)
		{
			WaitTime = timeout;
			OneShot = true;
			Timeout += timeoutAction;
		}
		
		public void RestartTimer()
		{
			Stop();
			Start();
		}
	}
	
	private MovementTimer movementTimer;
}
