using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public class DeviceProperties
		{	
			public class TraverseProperties
			{
				private bool Locked { get; set; }
				public double Min { get; private set; }
				public double Max { get; private set; }
				public double Range { get; private set; }
				public double Azimuth { get; private set; }
				public double Value { 
					get; 
					set
					{
						if (!Locked)
						{
							Azimuth = value;
							field = value;
							Min = value - (Range / 2);
							Max = value + (Range / 2);
						} else
						{
							field = Mathf.Clamp(value, Min, Max);
						}
					}
				}
				
				public TraverseProperties(double range)
				{
					Range = range;
					Locked = false;
					Azimuth = 0;
				}
				
			}
			
			public TraverseProperties Traverse { get; private set; }
			public float Altitude { get; private set; }
			public float Velocity { get; private set; }
			
			public DeviceProperties(Entities.Device device, Map.GroundLayer groundLayer)
			{
				Resources.Entities.Device deviceResource = device.DeviceResource;
				Traverse = new TraverseProperties(deviceResource.TraverseRange);
				GD.Print(device);
			}
		}
	}
}
