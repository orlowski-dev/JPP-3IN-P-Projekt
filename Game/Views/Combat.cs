using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class Combat
{
    public static void MainView()
    {
        if (Globals.CombatSession == null || Globals.PlayerCharacter == null)
            throw new Exception("CombatSession lub/i PlayerCharacter są null!");

        var whoseTurn = Globals.CombatSession.PlayerTurn ? "gracza" : "przeciwnika";
        Helpers.PrintTitle($"Walka - tura {whoseTurn}");

        Console.WriteLine($"HP Gracz: {Globals.PlayerCharacter.Health}");
        Console.WriteLine($"HP Przeciwnik: {Globals.CombatSession.Enemy.Health}");
        Console.WriteLine();

        if (Globals.CombatSession.PlayerTurn)
        {
            Globals.CombatSession.PlayerTurn = false;
            Console.Read();
            Helpers.ChangeView("Combat:MainView");
            return;
        }
        else
        {
            Globals.CombatSession.PlayerTurn = true;
            Console.Read();
            Helpers.ChangeView("Combat:MainView");
            return;
        }
    }
}
