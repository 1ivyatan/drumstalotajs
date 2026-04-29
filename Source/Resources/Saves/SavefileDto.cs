using Godot;
using System.IO;

namespace Drumstalotajs.Resources.Saves;

public readonly record struct SavefileDto(int FormatVersion, int LastKnownFormatVersion)
{
	public byte[] ToBinary()
	{
		using MemoryStream stream = new();
		using BinaryWriter writer = new(stream);
		writer.Write(FormatVersion);
		writer.Write(LastKnownFormatVersion);
		writer.Flush();
		return stream.ToArray();
	}
	
	public static bool TryFromBinary(byte[] bytes, out SavefileDto dto)
	{
		dto = default;
		if (bytes == null || bytes.Length < sizeof(int) * 2)
		{
			return false;
		}
		
		using MemoryStream stream = new(bytes, writable: false);
		using BinaryReader reader = new(stream);
		int formatVersion = reader.ReadInt32();
		int lastKnownFormatVersion = reader.ReadInt32();
		if (formatVersion != Savefile.CurrentFormatVersion)
		{
			return false;
		}
		
		dto = new SavefileDto(formatVersion, lastKnownFormatVersion);
		return true;
	}
}
