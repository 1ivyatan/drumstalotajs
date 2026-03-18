using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	public enum SelectorState { LOCK, VIEW, EDIT }
	
	public SelectorState Mode
	{
		get;
		set
		{
			field = value;
			switch (value)
			{
				case SelectorState.LOCK: 
					break;
				case SelectorState.VIEW: 
					break;
				case SelectorState.EDIT: 
					break;
				default: break;
			}
		}
	}
}
