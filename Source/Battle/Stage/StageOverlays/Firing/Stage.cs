using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.Firing
{
	public partial class Stage : Battle.Stage.StageOverlay
	{
		private Battle.Map.Selector _selector;
		private Battle.Map.EntityLayer _entityLayer;
		private TopPanel _topPanel;
		
		private void StartFiring()
		{
			/*
			int firedCount = 0;
			foreach (var cell in _entityLayer.EntityPointers[Entities.Type.Device])
			{
				SceneTreeTimer delayToFire = GetTree().CreateTimer(GD.RandRange(0.01f, 1f));
				delayToFire.Connect("timeout", Callable.From(() => {
					Entities.Device device = cell.Value as Entities.Device;
					Entities.Projectile projectile = device.Fire();
					
					projectile.Connect("Landed", Callable.From(() => {
						firedCount++;
						if (firedCount == _entityLayer.EntityPointers[Entities.Type.Device].Count)
						{
							(GetParent<Control>() as Battle.Stage.Manager).DeviceAdjustment();
						}
					}));
				}));
			}*/
		}
		
		public override void _Ready()
		{
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			_topPanel = GetNode<Control>("../../../TopPanel") as Battle.TopPanel;
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Battle.Map.EntityLayer;
			
			_topPanel.SetLabel("Firing!");
			_selector.Enabled = false;
			
			StartFiring();
		}
	}
}
