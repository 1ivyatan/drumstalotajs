using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class LevelSelectionContainer : Control
{
	private Resources.Sets.Levels.LevelProperties SelectedLevelProperties { get; set; }
	
	public MetadataModal MetadataModal { get; private set; }
	public LevelMarkers.LevelMarkersContainer LevelMarkersContainer { get; private set; }
	
	public override void _Ready()
	{
		this.MetadataModal = GetNode<Control>("MetadataModal") as MetadataModal;
		this.LevelMarkersContainer = GetNode<Control>("LevelMarkersContainer") as LevelMarkers.LevelMarkersContainer;
		
		this.LevelMarkersContainer.ClickedMarker += (Resources.Sets.Levels.LevelProperties levelProps) => {
			this.MetadataModal.LoadModal(levelProps);
		};
		
		this.LevelMarkersContainer.UnclickedMarker += () => {
			this.MetadataModal.CloseModal();
		};
	}
	
	public void LoadLevelSelection(Resources.Sets.Levels.LevelSet levelSet)
	{
		this.LevelMarkersContainer.LoadMarkers(levelSet.Levels);
	}
}
