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
				public bool Locked { get; set; }
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
					Min = 0;
					Max = 360;
				}
			}
			
			public class AngleProperties
			{
				public double Min { get; private set; }
				public double Max { get; private set; }
				public double Value { 
					get; 
					set { field = Mathf.Clamp(value, Min, Max); }
				}
				
				public AngleProperties(double min, double max)
				{
					Min = min;
					Max = max;
					Value = ((max - min) / 2) + min;
				}
			}
			
			public TraverseProperties Traverse { get; private set; }
			public AngleProperties Angle { get; private set; }
			public double Altitude { get; private set; }
			public double Velocity { get; private set; }
			
			public DeviceProperties(Entities.Device device, Map.Layers.GroundLayer groundLayer, TileData tileData)
			{
				Resources.Entities.Device deviceResource = device.DeviceResource;
				Traverse = new TraverseProperties(deviceResource.TraverseRange);
				Angle = new AngleProperties(deviceResource.AngleMin, deviceResource.AngleMax);
				Velocity = deviceResource.MuzzleVelocity;
				Altitude = groundLayer.Height + (double)tileData.GetCustomData("RelativeHeight");
			}
		}
	}
}
