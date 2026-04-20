using Godot;
using System;

namespace Drumstalotajs.Utilities;

public static class Files
{	
	public static T SafeLoadResource<T>(string path) where T : Resource
	{
		if (ResourceLoader.Exists(path))
		{
			return ResourceLoader.Load<T>(path);
		}
		return null;
	}
	
	public static FileAccess SafeLoadFile(string path, FileAccess.ModeFlags flags)
	{
		if (FileAccess.FileExists(path))
		{
			return FileAccess.Open(path, flags);
		}
		return null;
	}
}
