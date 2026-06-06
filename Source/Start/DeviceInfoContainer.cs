using Godot;
using System;
using Drumstalotajs;
using Drumstalotajs.Utilities;
using Drumstalotajs.Managers.Audio;
using Drumstalotajs.Resources.Lore;

namespace Drumstalotajs.Start;

public partial class DeviceInfoContainer : Control
{
	[Export] private DeviceLore[] _devices = [];
	
	[Export] private TextureRect _icon;
	[Export] private Label _title;
	[Export] private TextureRect _image;
	[Export] private RichTextLabel _desc;
	[Export] private RichTextLabel _stats;
	
	[Export] private Button _close;
	[Export] private Button _left;
	[Export] private Button _right;
	
	public override void _Ready()
	{
		_close.Pressed += Close;
	}
	
	public void Open()
	{
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
	}
}
