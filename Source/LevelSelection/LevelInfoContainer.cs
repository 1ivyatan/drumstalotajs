using Godot;
using System;
using Drumstalotajs.Resources.Levels;

public partial class LevelInfoContainer : Control
{
	public LevelSetProps Props { get; private set; } = null;
	
	public void Open(LevelSetProps props)
	{
		Props = props;
		Visible = true;
	}
	
	public void Close()
	{
		Props = null;
		Visible = false;
	}
}
