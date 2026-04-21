using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Editor;
using Drumstalotajs.Components;
using Drumstalotajs.Components.Modals;

namespace Drumstalotajs.Editor.Components;

public partial class MetaEditorContainer : ModalWindow
{
	[Export] private LineEdit _title;
	public string Title { get; private set; }
	
	public override void _Ready()
	{
		_title.TextChanged += (string value) => {
			Title = value;
		}; 
	}
}
