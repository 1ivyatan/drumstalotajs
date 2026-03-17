using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace drumstalotajs.Editor;

public partial class MetaContainer : EditorContainer
{
	private Button button;
	private LineEdit title;
	
	public override void _Ready()
	{
		VBoxContainer container = GetNode<VBoxContainer>("VBoxContainer");
		button = container.GetNode<Button>("CloseButton");
		title = container.GetNode<LineEdit>("Title");
		
		button.Pressed += ToggleContainer;
	}
	
	public Resources.Maps.Meta ExportMeta()
	{
		Resources.Maps.Meta metaData = new Resources.Maps.Meta();
		metaData.Title = title.Text;
		return metaData;
	}
}
