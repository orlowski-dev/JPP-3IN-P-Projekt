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
        new("menu", "Wyjdź do menu.", OnExitToMenuAction),
        new("exit", "Zakończ grę", OnExitGameAction),
    ];

    public static void MainView()
    {
        // Console.WriteLine("Jaki jest Twój następny krok?");
        Helpers.PrintTitle("Jaki jest Twój następny krok?");
        var command = Helpers.GetInput<string>();
        Helpers.ExecuteCommand(command, ref options);
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
        Globals.PlayerCharacter!.PrintStats();
        Console.WriteLine();
    }

    private static void OnExitGameAction()
    {
        Globals.ExitGame = true;
    }

    private static void OnExitToMenuAction()
    {
        Helpers.ChangeView("MainMenu:WelcomeView");
    }

    private static void OnFightAction()
    {
        Console.Clear();
        var randomEnemy = Globals.InitialData!.Enemies[
            new Random().Next(0, Globals.InitialData.Enemies.Count)
        ];
        randomEnemy.SetLevel(
            new Random().Next(Globals.PlayerCharacter!.Level, Globals.PlayerCharacter.Level + 2)
        );

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
}
