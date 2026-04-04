using Godot;
using System;
using Drumstalotajs.Scores;

namespace Drumstalotajs.Battle.Counting;

public partial class TimeLeft : Label
{
	public void SetTime(Score score)
	{
		GD.Print(score.GetRemainingTime());
	}
}
