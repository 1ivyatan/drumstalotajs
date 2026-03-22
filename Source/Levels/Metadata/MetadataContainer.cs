using Godot;
using System;

namespace drumstalotajs.Levels.Metadata;

public partial class MetadataContainer : Control
{
	private Modal modal;
	
	public override void _Ready()
	{
		modal = GetNode<Control>("Modal") as Modal;
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouse mouseEvent)
		{
			GD.Print(123);
		}
	}
	
	public void LoadModal(Resources.Sets.Levels.LevelProperties levelProps)
	{
		modal.LoadModal(levelProps);
	}
	
	public void CloseModal()
	{
		modal.CloseModal();
	}/*
	
	public void LoadContainer(Resources.Sets.Levels.LevelProperties levelProps)
	{
		LevelProperties = levelProps;
		title.Text = levelProps.Meta.Title;
		desc.Text = levelProps.Meta.Desc;
		Visible = true;
	}
	
	public void HideContainer()
	{
		Visible = false;
	}*/
}
