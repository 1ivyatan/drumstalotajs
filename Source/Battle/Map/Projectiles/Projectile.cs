using Godot;
using System;

namespace Drumstalotajs.Battle.Map.Projectiles
{
	public partial class Projectile : Node2D
	{
		[Signal] public delegate void DetonatedEventHandler();
		
		public ProjectileProperties Properties { get; private set; }
		
		private bool Flying { get; set; } = false;
		private Vector2I CurrentPos;
		
		private Map.Layers.GroundLayer _groundLayer;
		private Map.Layers.EntityLayer _entityLayer;
		
		private void CheckHit()
		{
			Vector2I cellPos = _groundLayer.GetCellPos(Position);
			double height = _groundLayer.GetHeight(cellPos);
			
			if (cellPos != CurrentPos)
			{
				Entities.Type entityType = _entityLayer.GetEntityType(cellPos);
				
				if (entityType != Entities.Type.None)
				{
					height += _entityLayer.EntityPointers[entityType][cellPos].EntityResource.Height;
				}
				CurrentPos = cellPos;
			}
				
			if (Properties.Altitude < height)
			{
				Detonate(height);
			}
		}
		
		private void Detonate(double height)
		{
			Flying = false;
			
			var spaceState = GetWorld2D().DirectSpaceState;
			PhysicsShapeQueryParameters2D query = new PhysicsShapeQueryParameters2D();
			CircleShape2D scannerShape = new CircleShape2D();
			scannerShape.Radius = (float)Properties.Device.Projectile.Blast.CasualityRadius;
			query.Shape = scannerShape;
			query.Transform = GlobalTransform;
			query.CollideWithAreas = true;
			
			GD.Print(Properties.Device.Projectile.Blast.CasualityRadius);
			
			foreach (var area in spaceState.IntersectShape(query))
			{
				Node2D collided = (Node2D)area["collider"];
				Vector2I cellPos = _groundLayer.GetCellPos(collided.Position);
				Entities.Entity entity = _entityLayer.GetEntity(cellPos);
				double damage = Properties.CalcDamage(entity);
				entity.DecreaseIntegrity(damage);
			}
			
			EmitSignal(SignalName.Detonated);
			QueueFree();
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
