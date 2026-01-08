using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class Combat
{
    public static void MainView()
    {
        if (Globals.CombatSession == null || Globals.GameSession == null)
            throw new Exception("CombatSession lub/i PlayerCharacter są null!");

        if (Globals.CombatSession.Status != CombatStatus.During)
        {
            Helpers.ChangeView("Combat:SummaryView");
            return;
        }

        var whoseTurn = Globals.CombatSession.PlayerTurn ? "gracza" : "przeciwnika";
        Helpers.PrintTitle($"Walka - tura {whoseTurn}");

        Console.WriteLine($"HP Gracz: {Globals.GameSession.PlayerCharacter.Health}");
        Console.WriteLine($"HP Przeciwnik: {Globals.CombatSession.Enemy.Health}");
        Console.WriteLine();

        if (Globals.CombatSession.PlayerTurn)
        {
            OnPlayerTurn();
        }
        else
        {
            OnEnemyTurn();
        }
    }

    private static void OnPlayerTurn()
    {
        var options = new List<MenuOption>
        {
            new(
                "attack",
                "Zaatakuj przeciwnika",
                () =>
                {
                    Console.WriteLine("Atakowanie przeciwnika..");
                    Thread.Sleep(Globals.ConsoleSleepTime);
                    var damage = Globals.CombatSession!.Attack(
                        Globals.GameSession!.PlayerCharacter!,
                        Globals.CombatSession.Enemy!
                    );
                    Console.WriteLine($"Zadano {damage} obrażeń przeciwnikowi.");
                    Thread.Sleep(Globals.ConsoleSleepTime);
                    Console.Clear();
                }
            ),
            new(
                "block",
                "Zablokuj kolejny atak przeciwnika.",
                () =>
                {
                    Console.WriteLine("Następny atak zostanie zablokowany. Kończę turę.");
                    Thread.Sleep(Globals.ConsoleSleepTime);
                    Globals.CombatSession!.BlockAttack();
                    Console.Clear();
                }
            ),
            new(
                "escape",
                "Spróbuj uciec z walki. Powodzenie kończy walkę bez przyznawania nagród. Niepowodzenie skutkuje koniec tury.",
                () =>
                {
                    Console.WriteLine("Próba ucieczki..");
                    var escaped = Globals.CombatSession!.EscapeCombat();
                    Thread.Sleep(Globals.ConsoleSleepTime);
                    if (escaped)
                    {
                        Helpers.ChangeView("Combat:SummaryView");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ucieczka nieudana. Koniec tury!");
                        Thread.Sleep(Globals.ConsoleSleepTime);
                        return;
                    }
                }
            ),
        };

        while (Globals.CombatSession!.PlayerTurn)
        {
            var input = Helpers.GetInput<string>();
            Helpers.ExecuteCommand(input, ref options);
        }
    }

    private static void OnEnemyTurn()
    {
        Console.WriteLine("Przeciwnik atakuje..");
        Thread.Sleep(Globals.ConsoleSleepTime);
        var damage = Globals.CombatSession!.Attack(
            Globals.CombatSession.Enemy!,
            Globals.GameSession!.PlayerCharacter!
        );
        Console.WriteLine($"Zadano {damage} obrażeń graczowu.");
        Thread.Sleep(Globals.ConsoleSleepTime);
        Helpers.ChangeView("Combat:MainView");
    }

    public static void SummaryView()
    {
        Helpers.PrintTitle("Podsumowanie walki");

        var nextView = "OpenWorld:MainView";

        switch (Globals.CombatSession!.Status)
        {
            case CombatStatus.PlayerWon:
                Console.WriteLine("Pokonano przeciwnika!");

                // get random item
                var drop = Globals.InitialData!.Items[
                    new Random().Next(0, Globals.InitialData.Items.Count)
                ];
                drop.SetLevel(Globals.GameSession!.PlayerCharacter.Level);
                Globals.GameSession.Equipment.AddItem(drop);
                Console.WriteLine();
                var text = "Nagrody za walkę:";
                Console.WriteLine(text);
                Console.WriteLine($"Złoto: {Globals.CombatSession.Enemy.GoldReward}");
                Console.WriteLine($"Punkty doświadczenia: {Globals.CombatSession.Enemy.ExpReward}");
                Console.WriteLine($"Przedmiot: {drop.Name}");
                break;
            case CombatStatus.EnenyWon:
                Console.WriteLine("Bardzo się starałeś, lecz z gry wyleciałeś.. he he");
                Globals.CombatSession = null;
                Globals.GameSession = null;
                nextView = "MainMenu:WelcomeView";
                break;
            case CombatStatus.Escaped:
                Console.WriteLine("Udało Ci się uciec z walki!");
                break;
        }

        Console.WriteLine();
        Console.WriteLine("Wciśnij ENTER aby kontynuować..");
        Console.ReadLine();
        Helpers.ChangeView(nextView);
    }
}
