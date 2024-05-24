using System.IO;
using ChronoArkMod.ModData;
using Newtonsoft.Json;

namespace Mcm.Common;

public static class ConfigCereal
{
    public static void WriteConfig<T>(T data, string path)
    {
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using var sw = new StreamWriter(path);
            sw.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
        } catch {
            Debug.Log("internal failure");
            // noexcept
        }
    }

    public static bool ReadConfig<T>(string path, out T? inferred)
    {
        try {
            if (File.Exists(path)) {
                using var sr = new StreamReader(path);
                inferred = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                return true;
            }
        } catch {
            Debug.Log("failed to read config");
            throw;
        }

        inferred = default;
        return false;
    }

    public static void WriteMcmConfig<T>(this ModInfo modInfo, T data)
    {
        try {
            modInfo.BackupMcmConfig();

            var path = modInfo.GetMcmConfigPath();
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            WriteConfig(data, path);
        } catch {
            Debug.Log("failed to write config");
            // noexcept
        }
    }

    public static T? ReadMcmConfig<T>(this ModInfo modInfo)
    {
        try {
            var path = modInfo.GetMcmConfigPath();
            if (File.Exists(path)) {
                using var sr = new StreamReader(path);
                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }

            if (modInfo.RestoreMcmConfig()) {
                return modInfo.ReadMcmConfig<T>();
            }
        } catch {
            Debug.Log("failed to read config");
            throw;
        }

        return default;
    }

    public static void BackupMcmConfig(this ModInfo modInfo)
    {
        try {
            var backupPath = modInfo.GetMcmBackupPath();
            Directory.CreateDirectory(Path.GetDirectoryName(backupPath));

            var configPath = modInfo.GetMcmConfigPath();
            if (File.Exists(configPath)) {
                File.Copy(configPath, backupPath, true);
            }
        } catch {
            Debug.Log("failed to backup config");
            // noexcept
        }
    }

    public static bool RestoreMcmConfig(this ModInfo modInfo)
    {
        try {
            var backupPath = modInfo.GetMcmBackupPath();
            if (File.Exists(backupPath)) {
                var configPath = modInfo.GetMcmConfigPath();
                File.Copy(backupPath, configPath, true);
                return true;
            }
        } catch {
            Debug.Log("failed to restore config");
            // noexcept
        }

        return false;
    }

    public static string GetMcmConfigPath(this ModInfo modInfo)
    {
        return Path.Combine(Application.persistentDataPath, $"Mod/Mcm/{modInfo.id}.json");
    }

    public static string GetMcmBackupPath(this ModInfo modInfo)
    {
        return Path.Combine(Application.persistentDataPath, $"Mod/Mcm/Backups/{modInfo.id}.json");
    }
}