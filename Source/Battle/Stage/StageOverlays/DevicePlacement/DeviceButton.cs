using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.DevicePlacement
{
	public partial class DeviceButton : Button
	{
		public void PrepareButton(int entityId)
		{
			Text = $"{entityId}";
		}
	}
}
