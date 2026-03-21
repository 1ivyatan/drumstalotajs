using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	public enum SelectorMode { LOCK, VIEW, EDIT }
	
	public SelectorMode Mode
	{
		get;
		set { field = value; if (value == SelectorMode.LOCK) Visible = false; }
	}
}
