using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Entity : Node2D
	{
		[Export] public Resources.Entities.Entity EntityResource { get; private set; }
		
		public double Integrity
		{
			get;
			private set 
			{ 
				field = Mathf.Clamp(value, 0, 100);
				if (field == 0)
				{
					
				}
			}
		}
		
		public void DecreaseIntegrity(double amount)
		{
			
		}
		
		public void RestoreIntegrity(double amount)
		{
			
		}
	}
}
