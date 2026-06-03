using Godot;
using System;
using Drumstalotajs;

namespace Drumstalotajs.Battle.Components;

public partial class RulerOverlay : Control
{
	public override void _Ready()
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (!Visible) return;
	}
}
