using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class MainMenuScreen
{
    public static void WelcomeView()
    {
        Console.WriteLine("Gra RPG");
        var options = new List<MenuOption>
        {
            new("1", "Kontynuuj grę", () => { }),
            new("2", "Wczytaj grę", () => { }),
            new("3", "Nowa gra", OnNewGameAction),
            new("0", "Wyjdź z gry", OnExitGameAction),
        };
        Helpers.PrintMenuOptions(ref options);
        var input = Helpers.GetInput<string>();
        Helpers.ExecuteCommand(input, ref options);
    }

    public static void NewGameView()
    {
        Console.WriteLine("Rozpocznij nową grę");
        Console.ReadLine();
    }

    private static void OnExitGameAction()
    {
        Globals.ExitGame = true;
    }

    private static void OnNewGameAction()
    {
        Helpers.ChangeView("MainMenuScreen:NewGameView");
    }
}
