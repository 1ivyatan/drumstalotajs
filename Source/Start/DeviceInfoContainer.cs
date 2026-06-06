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
	
	private int _idx = 0;
	
	public override void _Ready()
	{
		_close.Pressed += Close;
		_left.Pressed += () => {
			Prev();
		};
		_right.Pressed += () => {
			Next();
		};
	}
	
	private void Next()
	{
		int newIdx = _idx + 1 >= _devices.Length ? 0 : _idx + 1;
		LoadDeviceInfo(newIdx);
	}
	
	private void Prev()
	{
		int newIdx = _idx - 1 < 0 ? _devices.Length - 1 : _idx - 1;
		LoadDeviceInfo(newIdx);
	}
	
	private void LoadDeviceInfo(int idx)
	{
		_idx = idx;
		//GD.Print(_idx);
	}
	
	public void Open()
	{
		LoadDeviceInfo(0);
		Visible = true;
	}
	
	public void Close()
	{
		Visible = false;
	}
}
