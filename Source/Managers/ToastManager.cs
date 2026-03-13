using Godot;
using System;
using System.Collections.Generic;

namespace drumstalotajs.Managers;

public partial class ToastManager : Control
{
	private List<Components.Toast> Toasts;
	private PackedScene toastScene;
	
	public override void _Ready()
	{
		Toasts = new List<Components.Toast>();
		toastScene = ResourceLoader.Load<PackedScene>("res://Scenes/Components/Toast.tscn");;
		
		ChildEnteredTree += (Node node) => {
			if (node is Components.Toast) Toasts.Add(node as Components.Toast);
		};
		
		ChildExitingTree += (Node node) => {
			if (node is Components.Toast) Toasts.Remove(node as Components.Toast);
		};
	}
	
	public void Clear()
	{
		for (int i = 0; i < GetChildCount(); i++)
		{
			Components.Toast toast = GetChild(i) as Components.Toast;
			toast.Destroy();
			RemoveChild(toast);
		}
	}
	
	public void SpawnToast(string message)
	{
		Components.Toast toast = toastScene.Instantiate() as Components.Toast;
		toast.SetParams(message);
		AddChild(toast);
	}
}
