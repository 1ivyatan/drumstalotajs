using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public Resources.Entities.Device DeviceResource => EntityResource as Resources.Entities.Device;
		
		public double Angle { 
			get; 
			set
			{
				field = Mathf.Clamp(
					value, 
					DeviceResource.StartingAngle - DeviceResource.AngleRadius,
					DeviceResource.StartingAngle + DeviceResource.AngleRadius
				);
			}
		}
		
		private TileMapLayer _parent;
		private Sprite2D _sprite;
		
		public sealed override void _Ready()
		{
			_parent = GetParent<TileMapLayer>();
			_sprite = GetNode<Sprite2D>("Sprite");
			Angle = DeviceResource.StartingAngle;
			
			if (EntityResource != null)
			{
				_sprite.Texture = EntityResource.Sprites[0];
			}
		}
	}
}
