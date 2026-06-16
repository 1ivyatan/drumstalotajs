using Godot;
using System;
using System.Linq;
using Drumstalotajs;
using Drumstalotajs.Mapping;
using Drumstalotajs.Mapping.Entities;

namespace Drumstalotajs.Battle.Components;

public partial class DebugOverlay : Control
{
	[Signal] public delegate void AppliedDebugCheatEventHandler();
	
	[Export] private Map _map;
	[Export] private Button _disableEnemy;
	[Export] private Button _disablePlayer;
	
	public override void _Ready()
	{
		_disableEnemy.Pressed += () => {
			var targets = _map.EntityLayer.Instances
				.Where(i => i is Entity)
				.Where(e => ((Entity)e).Player == false)
				.Where(e => ((Entity)e).Target == true).ToList();
				
			foreach (Entity t in targets)
			{
				t.DecreaseIntegrity(200);
			}
			
			EmitSignal(SignalName.AppliedDebugCheat);
		};
		
		_disablePlayer.Pressed += () => {
			var targets = _map.EntityLayer.Instances
				.Where(i => i is Entity)
				.Where(e => ((Entity)e).Player == true).ToList();
				
			foreach (Entity t in targets)
			{
				t.DecreaseIntegrity(200);
			}
			
			EmitSignal(SignalName.AppliedDebugCheat);
		};
	}
}
