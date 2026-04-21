using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;

namespace Drumstalotajs.Editor.Components;

public partial class EditorTopnav : Topnav
{
	[Export] private PopupMenu _fileMenu;
	[Export] private PopupMenu _viewMenu;
	private PopupMenu _viewModeMenu;
	
	public override void _Ready()
	{
		_viewModeMenu = new PopupMenu();
		_viewModeMenu.Name = "Mode";
		_viewModeMenu.AddRadioCheckItem("View", (int)EditorMode.View);
		_viewModeMenu.AddRadioCheckItem("Insert", (int)EditorMode.Insert);
		_viewModeMenu.AddRadioCheckItem("Edit", (int)EditorMode.Edit);
		_viewMenu.AddChild(_viewModeMenu);
		_viewMenu.AddSubmenuNodeItem("Mode", _viewModeMenu);
	}
}
