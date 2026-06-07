using Godot;
using System;

namespace Drumstalotajs.Utilities;

public static class Debug
{
	public static bool IsDebug()
	{
		return OS.IsDebugBuild();
	}
	
	public static void DebugControl(Control control)
	{
		bool debug = IsDebug();
		control.Visible = debug;
		
		if (control is BaseButton button && !debug)
		{
			button.Disabled = true;
		}
	}
}
