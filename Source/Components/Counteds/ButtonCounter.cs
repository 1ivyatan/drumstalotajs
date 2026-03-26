using Godot;
using System;

namespace drumstalotajs.Components.Counteds;

public partial class ButtonCounter : Button
{
	private int TargetEntityId { get; set; }

	public void SetCounter(int num)
	{
		Text = $"{num}";
	}
	
	public void SetDevice()
	{
		
	}
	
	public void SetButton(int id, Texture2D texture)
	{
		TargetEntityId = id;
		Icon = texture;
		SetCounter(0);
	}
}
