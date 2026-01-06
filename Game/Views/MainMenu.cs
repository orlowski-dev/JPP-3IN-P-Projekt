using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class MainMenu
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
        var count = Globals.InitialData!.PlayerCharacters.Count;
        for (var i = 0; i < count; i++)
        {
            var p = Globals.InitialData.PlayerCharacters[i];
            Console.WriteLine($"[{i + 1}]");
            p.PrintBaseStats();
            Console.WriteLine();
        }
        Console.WriteLine("Wybierz klasę postaci");
        var input = Helpers.GetInputInRange(1, count);
    }

    private static void OnExitGameAction()
    {
        Globals.ExitGame = true;
    }

    private static void OnNewGameAction()
    {
        Helpers.ChangeView("MainMenu:NewGameView");
    }
}
