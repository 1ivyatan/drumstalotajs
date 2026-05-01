using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Saves;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Managers.Saves;

public partial class SaveManager : Node
{
	[Export] private string SavePath { get; set; } = "user://save";
	[Export] private string KeyPath { get; set; } = "user://key";

	public SecureFile<Save> Save { get; private set; } = null;
	public Save SaveData => Save?.Data;

	[Export] public LevelSet[] LevelSets { get; private set; } = [];
	
	public override void _EnterTree()
	{
		Load();
	}
	
	/* levelset */
	public LevelSet GetLevelSet(string name)
	{
		return LevelSets.FirstOrDefault(s => s.Name == name);
	}
	
	public bool IsLevelUnlocked(LevelSet levelSet, int order)
	{
		//if (LevelSets.Scores.Count == 0) LevelSets[levelSet]
		if (SaveData.Scores.ContainsKey(levelSet) && SaveData.Scores[levelSet].Count > 0)
		{
			var existingScores = SaveData.Scores[levelSet].Where(s => s.Order == order).ToList();
			if (existingScores.Count > 0)
			{
				return true;
			}
			
			int lastValid = LastScoredLevel(levelSet, order);
			return (order - lastValid) <= 1;
		} else
		{
			int idx = Array.FindIndex(levelSet.Levels, l => l.Order == order);
			return idx == 0;
		}
	}
	
	public void AddScore(LevelSet levelSet, LevelScore levelScore)
	{
		if (!SaveData.Scores.ContainsKey(levelSet))
		{
			SaveData.Scores[levelSet] = new();
		} else
		{
			int lastValid = LastScoredLevel(levelSet, levelScore.Order);
			if (levelScore.Order - lastValid > 1) return;
		}
		
		var existingScores = SaveData.Scores[levelSet].Where(s => s.Order == levelScore.Order).ToList();
		
		if (existingScores != null)
		{
			if (existingScores.Count > 1)
			{
				foreach (var existingScore in existingScores)
				{
					SaveData.Scores[levelSet].Remove(existingScore);
				}
				SaveData.Scores[levelSet].Add(levelScore);
			} else if (existingScores.Count == 1)
			{
				var index = SaveData.Scores[levelSet].IndexOf(existingScores[0]);
				SaveData.Scores[levelSet][index] = levelScore;
			} else
			{
				SaveData.Scores[levelSet].Add(levelScore);
			}
		} else
		{
			SaveData.Scores[levelSet].Add(levelScore);
		}
		SaveProgress();
	}
	
	private int LastScoredLevel(LevelSet levelSet, int order)
	{
		var numbers = SaveData.Scores[levelSet].Select(l => l.Order).ToArray();
		var subset = numbers.Where(n => n <= order).OrderBy(n => n);
		int lastValid = subset.Aggregate(0, (acc, current) => 
		{
			return (current == acc + 1) ? current : acc;
		});
		return lastValid;
	}
	
	private void Load()
	{
		Save = SecureFile<Save>.OpenOrCreate(SavePath, KeyPath);
		GD.Print($"SaveManager load state: {Save.LastLoadState}");
	}

	public void SaveProgress()
	{
		if (Save == null)
		{
			Load();
		}

		Save.Save();
	}
}
