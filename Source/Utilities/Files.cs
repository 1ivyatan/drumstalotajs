using Godot;
using System;

namespace Drumstalotajs.Utilities;

public static class Files
{	
	public static T SafeLoadResource<T>(string path, bool cached = true) where T : Resource
	{
		if (ResourceLoader.Exists(path))
		{
			if (cached)
			{
				return ResourceLoader.Load<T>(path);
			} else return ResourceLoader.Load<T>(path, "", ResourceLoader.CacheMode.Ignore);
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
