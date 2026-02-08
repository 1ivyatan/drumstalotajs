using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public class AngleProperties
		{
			public double Min {get; private set;}
			public double Max {get; private set;}
			public double Value
			{
				get;
				set
				{
					field = Mathf.Clamp(value, Min, Max);
				}
			}
			
			public AngleProperties(double startingAngle, double angleRadius)
			{
				Min = startingAngle - angleRadius;
				Max = startingAngle + angleRadius;
				Value = startingAngle;
			}
		}
		
		public Resources.Entities.Device DeviceResource => EntityResource as Resources.Entities.Device;
		public AngleProperties Angle { get; private set; }
		
		private TileMapLayer _parent;
		private Sprite2D _sprite;
		
		public sealed override void _Ready()
		{
			_parent = GetParent<TileMapLayer>();
			_sprite = GetNode<Sprite2D>("Sprite");
			Angle = new AngleProperties(DeviceResource.StartingAngle, DeviceResource.AngleRadius);
			
			if (EntityResource != null)
			{
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
