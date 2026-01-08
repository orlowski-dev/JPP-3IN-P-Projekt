using Game.Lib;

namespace Game.Core;

public static class Globals
{
    public static readonly string ViewsNamespace = "Game.Views";
    public static readonly int ConsoleSleepTime = 2000;
    public static readonly bool Debug = true;
    public static bool ExitGame { get; set; } = false;
    public static string View { get; set; } = "MainMenu:WelcomeView";
    public static InitialData? InitialData { get; set; }
    public static PlayerCharacter? PlayerCharacter { get; set; }
    public static CombatSession? CombatSession { get; set; }
}
