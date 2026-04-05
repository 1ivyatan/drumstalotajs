using Godot;
using System;
using Drumstalotajs.Scores;

namespace Drumstalotajs.Battle.Counting;

public partial class TimeLeft : Label
{
	public void SetTime(double seconds)
	{
		int secs = (int)seconds % 60;
		int minutes = ((int)seconds % 3600) / 60;
		int hours = (int)seconds / 3600;
		if (hours > 0) Text = $"{hours:D2}:{minutes:D2}:{secs:D2}";
		else Text = $"{minutes:D2}:{secs:D2}";
	}
}
