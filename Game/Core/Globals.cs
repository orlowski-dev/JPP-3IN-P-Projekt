using Game.Lib;

namespace Game.Core;

public static class Globals
{
    public static readonly string ViewsNamespace = "Game.Views";
    public static readonly int ConsoleSleepTime = 1000;
    public static readonly bool Debug = false;
    public static bool ExitGame { get; set; } = false;
    public static string View { get; set; } = "MainMenu:WelcomeView";
    public static InitialData? InitialData { get; set; }
    public static GameSession? GameSession { get; set; }
    public static CombatSession? CombatSession { get; set; }
    public static GameSettings GameSettings { get; set; } = GameSettings.LoadGameSettings();

    public static void Reset()
    {
        InitialData = null;
        GameSession = null;
        CombatSession = null;
    }
}
