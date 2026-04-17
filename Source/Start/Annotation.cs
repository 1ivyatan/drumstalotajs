using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Start;

public partial class Annotation : Components.Modals.Window
{
	[Export(PropertyHint.File, "*.txt")] private string _annotationFilePath;
	[Export] private Button _close;
	[Export] private RichTextLabel _text;
	
	public override void _Ready()
	{
		var annFile = Files.SafeLoadFile(_annotationFilePath,  FileAccess.ModeFlags.Read);
		if (annFile != null)
		{
			_text.Text = annFile.GetAsText();
		}
		
		_text.MetaClicked += (Variant meta) => {
			if (meta.VariantType == Variant.Type.String)
			{
				OS.ShellOpen(meta.ToString());
			}
		};
		
		_close.Pressed += () => {
			GetModal().HideModal();
		};
	}
}
