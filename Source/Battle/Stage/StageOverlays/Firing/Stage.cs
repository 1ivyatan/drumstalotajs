using Godot;
using System;

namespace Drumstalotajs.Battle.Stage.StageOverlays.Firing
{
	public partial class Stage : Battle.Stage.StageOverlay
	{
		private Map.Selector _selector;
		private Map.Layers.EntityLayer _entityLayer;
		private Map.Projectiles.ProjectileManager _projectileManager;
		private TopPanel _topPanel;
		
		private void StartFiring()
		{
			int firedCount = 0;
			foreach (var cell in _entityLayer.EntityPointers[Entities.Type.Device])
			{
				SceneTreeTimer delayToFire = GetTree().CreateTimer(GD.RandRange(0.01f, 1f));
				delayToFire.Connect("timeout", Callable.From(() => {
					Entities.Device device = cell.Value as Entities.Device;
					Map.Projectiles.Projectile projectile = _projectileManager.SpawnShell(device);
					
					projectile.Connect("Landed", Callable.From(() => {
						firedCount++;
					}));
					/*
					Entities.Projectile projectile = device.Fire();
					
					projectile.Connect("Landed", Callable.From(() => {
						
						if (firedCount == _entityLayer.EntityPointers[Entities.Type.Device].Count)
						{
							(GetParent<Control>() as Battle.Stage.Manager).DeviceAdjustment();
						}
					}));*/
				}));
			}
		}
		
		public override void _Ready()
		{
			_selector = GetNode<Node2D>("../../Map/Selector") as Battle.Map.Selector;
			_topPanel = GetNode<Control>("../../../TopPanel") as Battle.TopPanel;
			_entityLayer = GetNode<Node2D>("../../Map/EntityLayer") as Map.Layers.EntityLayer;
			_projectileManager = GetNode<Node2D>("../../Map/ProjectileManager") as Map.Projectiles.ProjectileManager;
			_topPanel.SetLabel("Firing!");
			_selector.Enabled = false;
			StartFiring();
		}
	}
}
