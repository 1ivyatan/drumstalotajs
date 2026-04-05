using Godot;
using System;

namespace Drumstalotajs.Components;

public partial class TimerDisplay : Counter
{
	public void SetTime(double seconds)
	{
		int secs = (int)seconds % 60;
		int minutes = ((int)seconds % 3600) / 60;
		int hours = (int)seconds / 3600;
		if (hours > 0) _counter.Text = $"{hours:D2}:{minutes:D2}:{secs:D2}";
		else _counter.Text = $"{minutes:D2}:{secs:D2}";
	}
}
