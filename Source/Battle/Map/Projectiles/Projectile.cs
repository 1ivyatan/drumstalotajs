using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class Projectile : Node2D
	{
		[Signal] public delegate void LandedEventHandler();
		
		public ProjectileProperties Properties { get; private set; }
		private bool Flying { get; set; } = false;
		private Vector2I CurrentPos;
		
		private Map.Layers.GroundLayer _groundLayer;
		private Map.Layers.EntityLayer _entityLayer;
		
		private void CheckHit()
		{
			Vector2I cellPos = _groundLayer.GetCellPos(Position);
			double height = _groundLayer.GetHeight(Position);
			
			if (cellPos != CurrentPos)
			{
				Entities.Type entityType = _entityLayer.GetEntityType(cellPos);
				
				if (entityType != Entities.Type.None)
				{
					height += _entityLayer.EntityPointers[entityType][cellPos].EntityResource.Height;
				}
				
				CurrentPos = cellPos;
			}
				
			if (Properties.Altitude.Value < height)
			{
				GD.Print("HIT");
			}
			
			//GD.Print($"{Properties.Altitude.Value} {_groundLayer.GetHeight(Position)}");
			//return Properties.Altitude.Value > _groundLayer.GetHeight(Position);
		}
		
		private void Destroy()
		{
			
		}
		
		public void Set(Entities.Device device)
		{
			Properties = new ProjectileProperties(device);
			Position = Properties.Position;
		}
		
		public void Launch()
		{
			Visible = true;
			Flying = true;
		}
		
		public override void _PhysicsProcess(double delta)
		{
			if (Flying)
			{
				Position = Properties.Position;
				Properties.NextStep(delta);
				CheckHit();
			}
		}
		
		public override void _Ready()
		{
			_entityLayer = GetNode<TileMapLayer>("../../EntityLayer") as Map.Layers.EntityLayer;
			_groundLayer = GetNode<TileMapLayer>("../../GroundLayer") as Map.Layers.GroundLayer;
			Visible = false;
		}
	}
}
