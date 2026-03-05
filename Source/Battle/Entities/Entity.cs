using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Entity : Area2D
	{
		[Export] public Resources.Entities.Entity EntityResource { get; private set; }
		
		public double Integrity
		{
			get;
			private set 
			{ 
				field = Mathf.Clamp(value, 0, 100);
				if (field <= 0) Destroy();
			}
		} = 100;
		
		private void Destroy()
		{
			Node parent = GetParent();
			if (parent != null && parent is Map.Layers.EntityLayer)
			{
				(parent as Map.Layers.EntityLayer).RemoveEntity(this);
			} else
			{
				QueueFree();
			}
		}
		
		public void DecreaseIntegrity(double amount)
		{
			Integrity -= amount;
		}
		
		public void RestoreIntegrity(double amount)
		{
			Integrity += amount;
		}
	}
}
