using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Mapping;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Saves;

namespace Drumstalotajs.Battle.Components;

public partial class ScoreManager : Node
{
	public double TimeLimit { get; private set; } = 0;
	public bool Running { get; private set; } = false;
	
	public LevelProps LevelProps { get; private set; } = null;
	public LevelSet LevelSet { get; private set; } = null;
	
	public override void _Ready()
	{
	}
	
	public void PrepareScoring(MapResource mapResource)
	{
		TimeLimit = mapResource.TimeLimitSecs;
	}
	
	public void PrepareScoring(MapResource mapResource, LevelSet levelSet, LevelProps levelProps)
	{
		LevelSet = levelSet;
		LevelProps = levelProps;
		PrepareScoring(mapResource);
	}
	
	public void RecordScore()
	{
		if (LevelSet != null && LevelProps != null)
		{
			var saveManager = Nodes.GetRoot().SaveManager;
			var score = new LevelScore(LevelProps);
			saveManager.AddScore(LevelSet, score);
		}
	}
}
