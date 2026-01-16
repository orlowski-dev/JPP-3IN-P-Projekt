using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class OpenWorld
{
    private static List<MenuOption> options =
    [
        new("help", "Wyświetla stronę pomocy.", OnHelpAction),
        new("stats", "Wyświetla statystyki postaci.", OnStatsPrintAction),
        new("fight", "Rozpocznij walkę z losowym przeciwnikiem.", OnFightAction),
        new("eq", "Otwórz ekwipunek.", OnOpenEqAction),
        new("shop", "Odwiedź sklep w mieście.", OnGoToShopAction),
        new("save", "Zapisz stan rozgrywki.", OnSaveGameAction),
        new("menu", "Wyjdź do menu.", OnExitToMenuAction),
        new("exit", "Zakończ grę", OnExitGameAction),
    ];

    public static void MainView()
    {
        // PrintDebugData();
        // Console.WriteLine("Jaki jest Twój następny krok?");
        Helpers.PrintTitle("Jaki jest Twój następny krok?");
        while (true)
        {
            var command = Helpers.GetInput<string>();
            if (Helpers.ExecuteCommand(command, ref options))
            {
                return;
            }
        }
    }

    private static void OnHelpAction()
    {
        Console.Clear();
        Helpers.PrintTitle("Pomoc");
        Helpers.PrintMenuOptions(ref options);
        Console.WriteLine();
    }

    private static void OnStatsPrintAction()
    {
        Console.Clear();
        // Console.WriteLine("Statystyki postaci");
        Helpers.PrintTitle("Statystyki postaci");
        Globals.GameSession!.PlayerCharacter!.PrintStats();
        Console.WriteLine();
    }

    private static void OnExitGameAction()
    {
        Globals.CombatSession = null;
        Globals.GameSession = null;
        Globals.ExitGame = true;
    }

    private static void OnExitToMenuAction()
    {
        Helpers.ChangeView("MainMenu:WelcomeView");
    }

    private static void OnFightAction()
    {
        Console.Clear();

        // TODO: Make it clone!
        var randomEnemy = Globals
            .InitialData!.Enemies[new Random().Next(0, Globals.InitialData.Enemies.Count)]
            .Clone();

        if (Globals.GameSession!.PlayerCharacter.Level > 1)
        {
            randomEnemy.SetLevel(
                new Random().Next(
                    Globals.GameSession!.PlayerCharacter!.Level,
                    Globals.GameSession.PlayerCharacter.Level + 2
                )
            );
        }

        Helpers.PrintTitle("Przygotowanie do walki z przeciwnikiem");
        randomEnemy.PrintStats();
        Console.WriteLine("Czy chcesz rozpocząć walkę?");
        var accepted = Helpers.GetActionConfirmation();
        if (!accepted)
        {
            Helpers.ChangeView("OpenWorld:MainView");
            return;
        }

        Globals.CombatSession = new CombatSession(randomEnemy);
        Helpers.ChangeView("Combat:MainView");
    }

    // private static void PrintDebugData()
    // {
    //     if (!Globals.Debug)
    //         return;

    //     Console.WriteLine(
    //         Globals.CombatSession == null ? "combatsession is null" : "combatsession is set"
    //     );
    //     Console.WriteLine(
    //         Globals.GameSession == null ? "gamesession is null" : "gamesession is set"
    //     );
    // }

    private static void OnOpenEqAction()
    {
        Helpers.ChangeView("Inventory:MainView");
    }

    private static void OnGoToShopAction()
    {
        Helpers.ChangeView("Shop:MainView");
    }

    private static void OnSaveGameAction()
    {
        var (saved, _, _, id) = SaveSystem.SaveGame();
        if (saved)
        {
            Console.Clear();
            Globals.GameSettings.LastSaveId = id;
            GameSettings.SaveSettings();
            Console.WriteLine("Zapisano.");
        }
    }
}
