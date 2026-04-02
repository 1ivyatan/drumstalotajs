using Godot;
using System;
using System.Collections;
using System.Collections.Generic; 
using Drumstalotajs;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Progress;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Managers.Data;

public partial class DataManager : Node
{
	[Export] public string DataPath { get; private set; }
	[Export] public LevelSet[] LevelSets { get; private set; }
	public OrderedDictionary<LevelSet, LevelProgress> LevelSetProgress { get; private set; }
	
	public override void _Ready()
	{
		LevelSetProgress = new OrderedDictionary<LevelSet, LevelProgress>();
		foreach (var set in LevelSets)
		{
			LevelSetProgress.Add(set, Files.SafeLoadResource<LevelProgress>($"{DataPath}/{set.Name}.dat"));
		}
	}
}
