using Godot;
using System;

namespace Drumstalotajs.Battle.Entities
{
	public partial class Device : Entity
	{
		public class DeviceProperties
		{
			public struct Angle
			{
				public double Min { get; private set; }
				public double Max { get; private set; }
				public Angle (double min, double max)
				{
					Min = min;
					Max = max;
				}
			}
			
			public struct Traverse
			{
				public double Min { get; private set; }
				public double Max { get; private set; }
				public Traverse (double min, double max)
				{
					Min = min;
					Max = max;
				}
				
			}
			
			public struct Range
			{
				
			}
			
			public float Altitude { get; private set; }
			public float Velocity { get; private set; }
			
			public DeviceProperties()
			{
				
			}
		}
	}
}
