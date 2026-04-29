using Godot;
using System.Linq;
using Drumstalotajs.Utilities;
using Drumstalotajs.Resources.Levels;
using Drumstalotajs.Resources.Saves;

namespace Drumstalotajs.Managers.Saves;

public partial class SaveManager : Node
{
	[Export] public string SecureSavePath { get; private set; } = "drumstalotajs.save";
	[Export] public LevelSet[] LevelSets { get; private set; } = [];
	public Savefile Savefile { get; private set; } = null;
	
	public override void _EnterTree()
	{
		LoadSaveFile();
	}
	
	private void LoadSaveFile()
	{
		if (SecureSavefiles.TryLoadSignedBinary(SecureSavePath, out byte[] payloadBytes) &&
			SavefileDto.TryFromBinary(payloadBytes, out SavefileDto dto))
		{
			Savefile = new Savefile();
			Savefile.FromDto(dto);
			return;
		}
		
		if (FileAccess.FileExists(SecureSavePath))
		{
			string backupPath = SecureSavefiles.CreateTimestampedBackupPath(SecureSavePath);
			DirAccess.RenameAbsolute(ProjectSettings.GlobalizePath(SecureSavePath), ProjectSettings.GlobalizePath(backupPath));
		}
		
		Savefile = new Savefile();
		SaveData();
	}
	
	public void SaveData()
	{
		Savefile ??= new Savefile();
		SavefileDto dto = Savefile.ToDto();
		SecureSavefiles.SaveSignedBinary(SecureSavePath, dto.ToBinary());
	}
	
	public LevelSet GetLevelSet(string name)
	{
		return LevelSets.FirstOrDefault(s => s.Name == name);
	}
}
