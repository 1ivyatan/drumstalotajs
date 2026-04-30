using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Disposable;

public partial class CounterTest : Button
{
	public override void _Ready()
	{
		Pressed += () => {
			Nodes.GetRoot().SaveManager.SaveData.Number += 1;
			GD.Print(Nodes.GetRoot().SaveManager.SaveData.Number);
			Nodes.GetRoot().SaveManager.SaveProgress();
		};
	}
}
