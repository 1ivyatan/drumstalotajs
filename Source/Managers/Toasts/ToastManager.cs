using Godot;
using System;
using System.Collections.Generic;
using Drumstalotajs;

namespace Drumstalotajs.Managers.Toasts;

public partial class ToastManager : Control
{
	[Export] private double ToastFadeTime { get; set; }
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
			Toast toast = GetChild(i) as Toast;
			toast.QueueFree();
			RemoveChild(toast);
		}
	}
	
	public void Spawn(string message)
	{
		AddChild(new Toast(message, ToastFadeTime));
	}
	
	public void SpawnOne(string message)
	{
		Clear();
		AddChild(new Toast(message, ToastFadeTime));
	}
}
