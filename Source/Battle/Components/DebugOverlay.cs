using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Mapping;

namespace Drumstalotajs.Battle.Components;

public partial class DebugOverlay : Control
{
	[Export] private Map _map;
	[Export] private Button _disableEnemy;
	[Export] private Button _disablePlayer;
	
}
