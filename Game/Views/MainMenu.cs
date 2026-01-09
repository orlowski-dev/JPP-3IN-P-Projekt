using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class MainMenu
{
    public static void WelcomeView()
    {
        // Console.WriteLine("Gra RPG");
        Helpers.PrintTitle("Gra RPG");
        var options = new List<MenuOption>
        {
            new("1", "Kontynuuj grę", () => { }),
            new("2", "Wczytaj grę", () => { }),
            new("3", "Nowa gra", OnNewGameAction),
            new("0", "Wyjdź z gry", OnExitGameAction),
        };
        Helpers.PrintMenuOptions(ref options);
        while (true)
        {
            var input = Helpers.GetInput<string>();
            if (Helpers.ExecuteCommand(input, ref options))
            {
                return;
            }
        }
    }

    public static void NewGameView()
    {
        // Console.WriteLine("Rozpocznij nową grę");
        Helpers.PrintTitle("Rozpocznij nową grę");
        var count = Globals.InitialData!.PlayerCharacters.Count;
        for (var i = 0; i < count; i++)
        {
            var p = Globals.InitialData.PlayerCharacters[i];
            Console.WriteLine($"[{i + 1}]");
            p.PrintBaseStats();
            Console.WriteLine();
        }
        Console.WriteLine("Wybierz klasę postaci..");
        var cIdx = Helpers.GetInputInRange(1, count);
        var selectedCharacter = Globals.InitialData.PlayerCharacters[cIdx - 1].Clone();
        Console.WriteLine($"Wybrana klasa: {selectedCharacter.ClassName}");
        Console.WriteLine("Podaj nazwę postaci..");
        string cName;
        do
        {
            cName = Helpers.GetInput<string>("conajmniej 3 znaki");
        } while (cName.Length < 3);
        selectedCharacter.Name = cName;
        Globals.GameSession = new GameSession(selectedCharacter);
        Helpers.ChangeView("OpenWorld:MainView");
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
