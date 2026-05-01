using Godot;
using Godot.Collections;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Saves;

namespace Drumstalotajs.Disposable;

public partial class CounterTest : Button
{
	public override void _Ready()
	{
		Pressed += () => {
			
			//Nodes.GetRoot().SaveManager.SaveData.Scores[0] = new Array<LevelScore>();
			//var test = new LevelScore();
			//test.Order = 6;
			//Nodes.GetRoot().SaveManager.SaveData.Scores[0].Add(test);
			
			//foreach (var h in Nodes.GetRoot().SaveManager.SaveData.Scores[])
			//{
			//	GD.Print(h.Order);
			//}
			
		//	GD.Print(Nodes.GetRoot().SaveManager.SaveData.Scores.Count);
		//	Nodes.GetRoot().SaveManager.SaveProgress();
		};
	}
}
