using Godot;
using System;

namespace drumstalotajs.Battle;

public partial class BattleScene : Node2D
{
	private Resources.Maps.Meta MetaData;
	private Resources.Maps.Map MapData;
	
	public void LoadMap(Resources.Maps.Meta mapData)
	{
		GD.Print(123);
	}
}
