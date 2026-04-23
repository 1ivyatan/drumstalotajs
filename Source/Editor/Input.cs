using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Mapping;
using Drumstalotajs.Editor.Components;
using Drumstalotajs.Components.Modals;
using Drumstalotajs.Mapping.Cameras;
using Drumstalotajs.Mapping.Selection;
using Drumstalotajs.Mapping.Layers;

namespace Drumstalotajs.Editor;

public partial class EditorScene : Node2D
{
	[Export] private InsertWindow InsertWindow { get; set; }
	
	public override void _UnhandledInput(InputEvent @event)
	{
		switch (Mode)
		{
			case EditorMode.Insert: HandleInsert(@event); break;
			default: break;
		}
	}
	
	private void HandleInsert(InputEvent @event)
	{
		
	}
		//if (@event is InputEventMouseButton)
		//{
	//		var ins = OverlayLayer.GetInstance(new Vector2I(5, 5));
			//if (ins != null)
			//{
				
			//	Export();
			//}
		//}
}
