using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Drumstalotajs.Resources.Levels;

namespace Drumstalotajs.Resources.Progress;

[GlobalClass]
public partial class LevelProgress : Resource
{
	public List<LevelProgressScore> CompletedScores { get; private set; }
	
	public LevelProgressScore GetScore(LevelSetProps levelProps)
	{
		var matches = CompletedScores.Where(s => s.LevelProps == levelProps).ToArray();
		if (matches.Length > 0)
		{
			return matches[0];
		} else
		{
			return null;
		}
	}
	
	public bool IsUnlocked(LevelSetProps levelProps)
	{
		var matches = CompletedScores.Where(s => s.LevelProps == levelProps).ToArray();
		if (matches.Length > 0)
		{
			return true;
		} else if (matches.Length == 0 && levelProps.Order == 1)
		{
			return true;
		}
		
		var lastScore = CompletedScores.LastOrDefault();
		
		if (lastScore != null && (levelProps.Order - lastScore.LevelProps.Order) == 1)
		{
			return true;
		} else
		{
			return false;
		}
	}
	
	public ProgressState GetProgressState(LevelSetProps levelProps)
	{
		var matches = CompletedScores.Where(s => s.LevelProps == levelProps).ToArray();
		if (matches.Length > 0)
		{
			return ProgressState.Completed;
		} else if (matches.Length == 0 && levelProps.Order == 1)
		{
			return ProgressState.Available;
		}
		
		var lastScore = CompletedScores.LastOrDefault();
		if (lastScore != null && (levelProps.Order - lastScore.LevelProps.Order) == 1)
		{
			return ProgressState.Available;
		} else
		{
			return ProgressState.Locked;
		}
	}
	
	public void UnlockLevel(LevelProgressScore score)
	{
		
	}
	
	public LevelProgress()
	{
		CompletedScores = new List<LevelProgressScore>();
	}
}
