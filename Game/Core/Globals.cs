using Game.Lib;

namespace Game.Core;

public static class Globals
{
    public static readonly String ViewsNamespace = "Game.Views";
    public static Boolean ExitGame { get; set; } = false;
    public static String View { get; set; } = "MainMenu:WelcomeView";
    public static InitialData? InitialData { get; set; }
}
