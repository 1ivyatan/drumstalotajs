using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;

namespace Drumstalotajs.Managers.Toasts;

public partial class ToastManager : Control
{
	[Export] private double ToastFadeTime { get; set; }
	[Export] private PackedScene ToastScene { get; set; }
	
	private List<Toast> Toasts;
	private int limit = 5;
	
	public override void _Ready()
	{
		Toasts = new List<Toast>();
		
		ChildEnteredTree += (Node node) => {
			if (node is Toast)
			{
				if (Toasts.Count >= limit)
				{
					Pop();
				}
				Toasts.Add(node as Toast);
			}
		};
		
		ChildExitingTree += (Node node) => {
			if (node is Toast) Toasts.Remove(node as Toast);
		};
	}
	
	public void Pop()
	{
		Toast toast = GetChild(0) as Toast;
		toast.QueueFree();
		RemoveChild(toast);
	}
	
	public void Clear()
	{
		for (int i = 0; i < GetChildCount(); i++)
		{
			if (GetChild(i) is Toast toast)
			{
				toast.QueueFree();
				RemoveChild(toast);
			}
		}
	}
	
	public void Spawn(string message)
	{
		var toast = ToastScene.Instantiate() as Toast;
		toast.AddData(message, ToastFadeTime);
		AddChild(toast);
		toast.StartTimer();
	}
	
	public void SpawnOne(string message)
	{
		Clear();
		Spawn(message);
	}
}
