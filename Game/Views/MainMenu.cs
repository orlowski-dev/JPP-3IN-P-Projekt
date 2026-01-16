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
            new("1", "Kontynuuj grę", OnContinueGameAction),
            new("2", "Wczytaj grę", OnLoadGameAction),
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

    private static void OnLoadGameAction()
    {
        Helpers.ChangeView("MainMenu:LoadGameView");
    }

    public static void LoadGameView()
    {
        Helpers.PrintTitle("Wczytaj grę");
        var saveFiles = SaveSystem.GetSaveFiles();

        if (saveFiles.Length == 0)
        {
            Console.Clear();
            Console.WriteLine("Nie ma żadnych zapisów!");
            Helpers.ChangeView("MainMenu:WelcomeView", false);
            return;
        }

        var saves = new List<SaveData>();
        foreach (var file in saveFiles)
        {
            var save = SaveSystem.LoadSaveGameFile(file);
            if (save == null)
            {
                continue;
            }
            saves.Add(save);
        }

        for (var i = 0; i < saves.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]");
            Console.WriteLine($"Zapis: {saves[i].Id}");
            Console.WriteLine($"Data: {saves[i].CreatedAt}");
            Console.WriteLine(
                $"Postać: {saves[i].GameSession.PlayerCharacter.OneLineDescription()}"
            );
            Console.WriteLine();
        }

        Console.WriteLine("Wybierz zapis..");
        var input = Helpers.GetInputInRange(1, saves.Count);
        Globals.GameSession = saves[input - 1].GameSession;
        Globals.GameSession.Equipment.SetPlayer(saves[input - 1].GameSession.PlayerCharacter);
        Helpers.ChangeView("OpenWorld:MainView");
    }

    private static void OnContinueGameAction()
    {
        var saveFiles = SaveSystem.GetSaveFiles();

        if (saveFiles.Length == 0 || Globals.GameSettings.LastSaveId == null)
        {
            Console.Clear();
            Console.WriteLine("Nie ma żadnych zapisów!");
            Helpers.ChangeView("MainMenu:WelcomeView", false);
            return;
        }

        SaveData? saveData = null;
        foreach (var file in saveFiles)
        {
            var save = SaveSystem.LoadSaveGameFile(file);
            if (save == null)
            {
                continue;
            }
            if (save.Id == Globals.GameSettings.LastSaveId)
            {
                saveData = save;
                break;
            }
        }

        if (saveData == null)
        {
            Console.Clear();
            Console.WriteLine("Nie udało się wczytać ostatniego zapisu@");
            Helpers.ChangeView("MainMenu:WelcomeView", false);
            return;
        }

        Globals.GameSession = saveData.GameSession;
        Globals.GameSession.Equipment.SetPlayer(saveData.GameSession.PlayerCharacter);
        Helpers.ChangeView("OpenWorld:MainView");
    }
}
