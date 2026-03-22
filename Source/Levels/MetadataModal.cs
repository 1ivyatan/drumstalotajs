using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class MetadataModal : Control
{
	public void LoadModal(Resources.Sets.Levels.LevelProperties levelProps)
	{
		Visible = true;
	}
	
	public void CloseModal()
	{
		Visible = false;
	}
}
