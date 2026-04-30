using Godot;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using GFileAccess = Godot.FileAccess;

namespace Drumstalotajs.Utilities;

public enum SecureFileLoadState
{
	LoadedMain,
	RecoveredBackup,
	InitializedNew
}

public class SecureFile<TResource> where TResource : Resource, new()
{
	private const int CurrentVersion = 1;
	private const int KeySizeBytes = 32;

	private readonly string _savePath;
	private readonly string _backupPath;
	private readonly string _keyPath;

	public TResource Data { get; private set; }
	public SecureFileLoadState LastLoadState { get; private set; }

	private SecureFile(string savePath, string keyPath, TResource data)
	{
		_savePath = savePath;
		_backupPath = $"{savePath}.bak";
		_keyPath = keyPath;
		Data = data;
		LastLoadState = SecureFileLoadState.InitializedNew;
	}

	public static SecureFile<TResource> OpenOrCreate(string savePath, string keyPath)
	{
		SecureFile<TResource> secureFile = new(savePath, keyPath, new TResource());

		byte[] key = secureFile.LoadOrCreateKey();
		if (secureFile.TryReadSecureResource(savePath, key, out TResource mainData))
		{
			secureFile.Data = mainData;
			secureFile.LastLoadState = SecureFileLoadState.LoadedMain;
			return secureFile;
		}

		if (secureFile.TryReadSecureResource(secureFile._backupPath, key, out TResource backupData))
		{
			secureFile.Data = backupData;
			secureFile.LastLoadState = SecureFileLoadState.RecoveredBackup;
			secureFile.Save();
			return secureFile;
		}

		secureFile.Data = new TResource();
		secureFile.LastLoadState = SecureFileLoadState.InitializedNew;
		secureFile.Save();
		return secureFile;
	}

	public void Save()
	{
		byte[] key = LoadOrCreateKey();
		byte[] payload = SerializeResource(Data);
		byte[] mac = ComputeMac(CurrentVersion, payload, key);

		SecureEnvelope envelope = new()
		{
			Version = CurrentVersion,
			Payload = payload,
			Mac = mac
		};

		string serializedEnvelope = JsonSerializer.Serialize(envelope);
		WriteAtomically(_savePath, serializedEnvelope);
		WriteAtomically(_backupPath, serializedEnvelope);
	}

	private byte[] LoadOrCreateKey()
	{
		if (GFileAccess.FileExists(_keyPath))
		{
			using GFileAccess readHandle = GFileAccess.Open(_keyPath, GFileAccess.ModeFlags.Read);
			return readHandle.GetBuffer((long)readHandle.GetLength());
		}

		byte[] key = new byte[KeySizeBytes];
		System.Security.Cryptography.RandomNumberGenerator.Fill(key);

		using GFileAccess writeHandle = GFileAccess.Open(_keyPath, GFileAccess.ModeFlags.Write);
		writeHandle.StoreBuffer(key);
		return key;
	}

	private bool TryReadSecureResource(string path, byte[] key, out TResource resource)
	{
		resource = null;
		if (!GFileAccess.FileExists(path))
		{
			return false;
		}

		try
		{
			using GFileAccess file = GFileAccess.Open(path, GFileAccess.ModeFlags.Read);
			string content = file.GetAsText();
			SecureEnvelope envelope = JsonSerializer.Deserialize<SecureEnvelope>(content);
			if (envelope == null || envelope.Payload == null || envelope.Mac == null)
			{
				return false;
			}

			byte[] expectedMac = ComputeMac(envelope.Version, envelope.Payload, key);
			if (!CryptographicOperations.FixedTimeEquals(expectedMac, envelope.Mac))
			{
				GD.PushWarning($"SecureFile verification failed for {path}.");
				return false;
			}

			TResource loaded = DeserializeResource(envelope.Payload);
			if (loaded == null)
			{
				return false;
			}

			resource = loaded;
			return true;
		}
		catch (Exception ex)
		{
			GD.PushWarning($"SecureFile could not load {path}: {ex.Message}");
			return false;
		}
	}

	private static byte[] ComputeMac(int version, byte[] payload, byte[] key)
	{
		using HMACSHA256 hmac = new(key);
		byte[] versionBytes = BitConverter.GetBytes(version);
		byte[] input = new byte[versionBytes.Length + payload.Length];
		Buffer.BlockCopy(versionBytes, 0, input, 0, versionBytes.Length);
		Buffer.BlockCopy(payload, 0, input, versionBytes.Length, payload.Length);
		return hmac.ComputeHash(input);
	}

	private static byte[] SerializeResource(TResource resource)
	{
		string tempPath = $"user://.secure_tmp_{Guid.NewGuid():N}.res";
		Error saveError = ResourceSaver.Save(resource, tempPath);
		if (saveError != Error.Ok)
		{
			throw new IOException($"Resource serialization failed: {saveError}");
		}

		try
		{
			using GFileAccess file = GFileAccess.Open(tempPath, GFileAccess.ModeFlags.Read);
			return file.GetBuffer((long)file.GetLength());
		}
		finally
		{
			if (GFileAccess.FileExists(tempPath))
			{
				DirAccess.RemoveAbsolute(tempPath);
			}
		}
	}

	private static TResource DeserializeResource(byte[] payload)
	{
		string tempPath = $"user://.secure_tmp_{Guid.NewGuid():N}.res";
		using (GFileAccess file = GFileAccess.Open(tempPath, GFileAccess.ModeFlags.Write))
		{
			file.StoreBuffer(payload);
		}

		try
		{
			return ResourceLoader.Load<TResource>(tempPath, "", ResourceLoader.CacheMode.Ignore);
		}
		finally
		{
			if (GFileAccess.FileExists(tempPath))
			{
				DirAccess.RemoveAbsolute(tempPath);
			}
		}
	}

	private static void WriteAtomically(string destinationPath, string content)
	{
		string tempPath = $"{destinationPath}.tmp";
		using (GFileAccess file = GFileAccess.Open(tempPath, GFileAccess.ModeFlags.Write))
		{
			file.StoreString(content);
		}

		if (GFileAccess.FileExists(destinationPath))
		{
			DirAccess.RemoveAbsolute(destinationPath);
		}

		Error renameError = DirAccess.RenameAbsolute(tempPath, destinationPath);
		if (renameError != Error.Ok)
		{
			throw new IOException($"Atomic write failed for {destinationPath}: {renameError}");
		}
	}

	private sealed class SecureEnvelope
	{
		public int Version { get; set; }
		public byte[] Payload { get; set; }
		public byte[] Mac { get; set; }
	}
}
