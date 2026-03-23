using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Managers;

public partial class CursorManager : Node
{
	public void ResetCursor()
	{
		Input.SetCustomMouseCursor(null);
	}
}
