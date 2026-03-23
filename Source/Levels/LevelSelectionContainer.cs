using Godot;
using System;

namespace drumstalotajs.Levels;

public partial class LevelSelectionContainer : Control
{
	private Resources.Sets.Levels.LevelProperties SelectedLevelProperties { get; set; }
	
	private MetadataModal metadataModal;
	private LevelMarkers.LevelMarkersContainer levelMarkersContainer;
	
	public override void _Ready()
	{
		metadataModal = GetNode<Control>("MetadataModal") as MetadataModal;
		levelMarkersContainer = GetNode<Control>("LevelMarkersContainer") as LevelMarkers.LevelMarkersContainer;
		
		levelMarkersContainer.ClickedMarker += (Resources.Sets.Levels.LevelProperties levelProps) => {
			metadataModal.LoadModal(levelProps);
		};
		
		levelMarkersContainer.UnclickedMarker += () => {
			metadataModal.CloseModal();
		};
		
		metadataModal.ClickedBattle += (Resources.Sets.Levels.LevelProperties levelProps) => {
			
		};
	}
	
	public void LoadLevelSelection(Resources.Sets.Levels.LevelSet levelSet)
	{
		levelMarkersContainer.LoadMarkers(levelSet.Levels);
	}
}
