using Game.Core;

namespace Game.Lib;

public class GameSettings
{
    public Guid? LastSaveId { get; set; }

    public static GameSettings LoadGameSettings()
    {
        var settings =
            Helpers.GetObjectFromJsonFile<GameSettings>(
                Helpers.CombinePath(["Data", "settings.json"])
            ) ?? new GameSettings();

        Globals.GameSettings = settings;

        return settings;
    }

    public static void SaveSettings()
    {
        if (Globals.GameSettings == null)
        {
            throw new Exception("Globals.GameSettings nie istnieje!");
        }

        Helpers.SaveJsonFile<GameSettings>(Globals.GameSettings, ["Data"], "settings.json");
    }
}
