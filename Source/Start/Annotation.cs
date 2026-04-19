/*using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utils;

namespace Drumstalotajs.Start;

public partial class Annotation : Components.Modals.Window
{
	[Export] private string _annotationFilePath;
	[Export] private Button _close;
	[Export] private RichTextLabel _text;
	
	public override void _Ready()
	{
		_close = GetNode<Button>("Close");
		_text = GetNode<RichTextLabel>("Text");
		
		var annFile = Files.SafeLoadFile(_annotationFilePath,  FileAccess.ModeFlags.Read);
		if (annFile != null)
		{
			_text.Text = annFile.GetAsText();
		}
		
		_close.Pressed += () => {
			GetModal().HideModal();
		};
	}
}
*/
