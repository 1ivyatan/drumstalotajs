using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;

namespace Drumstalotajs.Components;

public partial class GameRunStatus : Label
{
	public override void _Ready()
	{
		var debug = Debug.IsDebug() ? "Debug mode\n" : "";
		var editor = Utilities.Editor.IsEditor() ? "Inside editor\n" : "";
		Text = $"{debug}{editor}";
	}
}
