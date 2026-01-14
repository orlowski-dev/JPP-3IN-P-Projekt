using System.Text.Json;
using Game.Lib;

namespace Game.Core;

public static class SaveSystem
{
    private static readonly string SaveDataDir = Helpers.CombinePath("savedata");

    public static (string, bool) GetSaveDataDirPath(string? dirName = null)
    {
        var dn = dirName ?? SaveDataDir;

        if (!Directory.Exists(dn))
        {
            Directory.CreateDirectory(dn);
            return (dn, true);
        }
        return (dn, false);
    }

    /// <summary>
    /// Zapisuje na dysku plik zapisu gry.
    /// Zwraca - czy zapisano, nazwę pliku zapisu, pełną ścieżkę
    /// </summary>
    public static (bool, string, string) SaveGame(string? dirName = null)
    {
        var dn = dirName ?? SaveDataDir;
        var gameSession =
            Globals.GameSession ?? throw new Exception("Globals.GameSession nie istnieje");

        var saveData = new SaveData(gameSession);
        var jsonContent = JsonSerializer.Serialize(saveData);
        var (path, _) = GetSaveDataDirPath(dn);
        var fileName = $"{Guid.NewGuid()}.savedata.json";
        var filePath = Path.Combine(path, fileName);
        File.WriteAllText(filePath, jsonContent);
        return (true, fileName, filePath);
    }

    public static SaveData? LoadSaveGameFile(string filePath)
    {
        try
        {
            var jsonContent = File.ReadAllText(filePath);
            var saveData = JsonSerializer.Deserialize<SaveData>(jsonContent);

            return saveData;
        }
        catch (Exception e)
        {
            Helpers.PrintException(e);
            return null;
        }
    }

    public static string[] GetSaveFiles(string? dirName = null)
    {
        var dn = dirName ?? SaveDataDir;
        var files = Directory.GetFiles(dn);
        Console.WriteLine(files);
        return files;
    }
}
