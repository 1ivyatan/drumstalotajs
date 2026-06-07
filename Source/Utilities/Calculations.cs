using System;
using System.Linq;
using System.Collections.Generic;
using Godot;

namespace Drumstalotajs.Utilities;

public static class Calculations
{
	public static int GetQuadrant(double degrees)
	{
		degrees %= 360.0;
		if (degrees < 0) degrees += 360.0;
		return ((int)degrees/90) % 4 + 1;
	}
	
	public static double ToRadians(double degrees)
	{
		return (Math.PI / 180) * degrees;
	}
		
	public static Vector2 AzimuthToDirection(double azimuth)
	{
		double radians = ToRadians(90.0 - azimuth);
		return new Vector2((float)Math.Cos(radians), (float)-Math.Sign(Math.Sin(radians)));
	}
		
	public static double GetAirDensity(double altitude)
	{
		return Constants.Physics.SeaLevelAirDensity * Math.Exp(-altitude / Constants.Physics.ScaleHeight);
	}
	
	public static double GetDecimal(double number)
	{
		decimal preciseNumber = (decimal)number;
		decimal decimalPart = preciseNumber - Math.Truncate(preciseNumber);
		return (float)decimalPart;
	}
	
	public static double DirectionToAzimuth(Vector2 direction)
	{
		float directionRads = Mathf.Atan2(direction.X, -direction.Y);
		return Mathf.PosMod(Mathf.RadToDeg(directionRads), 360f);
	}
	
	public static Vector2 GetRandomPoint(Rect2 rect)
	{
		float x = (float)GD.RandRange(rect.Position.X, rect.End.X);
		float y = (float)GD.RandRange(rect.Position.Y, rect.End.Y);
		return new Vector2(x, y);
	}
	
	public static Vector2[][] GetClusters(List<Vector2> points, List<Vector2> filter = null, double minDistance = 50)
	{
		if (points.Count == 0)
		{
			return new Vector2[0][];
		}
		
		List<List<Vector2>> clusters = new();
		bool[] visited = new bool[points.Count];
		
		for (int i = 0; i < points.Count; i++)
		{
			if (visited[i]) continue;
			
			List<Vector2> cluster = new List<Vector2>();
			Queue<int> queue = new Queue<int>();
			
			queue.Enqueue(i);
			visited[i] = true;
			
			while (queue.Count > 0)
			{
				int current = queue.Dequeue();
				cluster.Add(points[current]);
				for (int j = 0; j < points.Count; j++)
				{
					if (!visited[j] && points[current].DistanceTo(points[j]) < minDistance)
					{
						visited[j] = true;
						queue.Enqueue(j);
					}
				}
			}
			
			if (filter != null && filter.Count > 0)
			{
				cluster = cluster.Where(p => filter.Contains(p)).ToList();
			}
			
			if (cluster.Count > 1) clusters.Add(cluster);
		}
		
		return clusters.ConvertAll(c => c.ToArray()).ToArray();
	}
}
