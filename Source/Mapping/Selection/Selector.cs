using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Mapping.Selection;

public partial class Selector : Node2D
{
	public SelectorMode Mode { get; set; } = SelectorMode.Locked;
	
	public override void _Ready()
	{
	}
}
