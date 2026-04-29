using Godot;
using System.IO;

namespace Drumstalotajs.Resources.Saves;

[GlobalClass]
public partial class Savefile : Resource
{
	[ExportGroup("Security")]
	[Export] public int LastKnownFormatVersion { get; private set; } = CurrentFormatVersion;
	public const int CurrentFormatVersion = 1;

	public SavefileDto ToDto()
	{
		return new SavefileDto(CurrentFormatVersion, LastKnownFormatVersion);
	}
	
	public void FromDto(SavefileDto dto)
	{
		LastKnownFormatVersion = dto.LastKnownFormatVersion;
	}
}
