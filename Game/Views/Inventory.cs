using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class Inventory
{
    private static List<MenuOption> _mainViewOptions =
    [
        new("help", "Wyświetla pomoc.", OnHelpAction),
        new("list", "Wylistuj przedmioty z ekwipunku.", OnListItems),
        new("use", "Użyj/załóż przedmiot.", OnUseItem),
        new("close", "Zamknij ekwipunek.", OnCloseAction),
    ];

    public static void MainView()
    {
        Helpers.PrintTitle("Ekwipunek");
        PrintActiveItems();
        Console.WriteLine();

        while (true)
        {
            var command = Helpers.GetInput<string>();
            if (Helpers.ExecuteCommand(command, ref _mainViewOptions))
            {
                return;
            }
        }
    }

    private static void PrintActiveItems()
    {
        Console.WriteLine("Broń:");
        var weapon = Globals.GameSession!.Equipment.EquippedWeapon;
        if (weapon == null)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            weapon.PrintStats();
        }
        Console.WriteLine();

        Console.WriteLine("Zbroja:");
        var armor = Globals.GameSession!.Equipment.ActiveArmor;
        if (armor == null)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            armor.PrintStats();
        }
    }

    private static void OnHelpAction()
    {
        Console.Clear();
        Helpers.PrintTitle("Pomoc");
        Helpers.PrintMenuOptions(ref _mainViewOptions);
        Console.WriteLine();
    }

    private static void OnListItems()
    {
        Console.Clear();
        var items = Globals.GameSession!.Equipment.GetNonActiveItems();
        if (items.Count == 0)
        {
            Console.WriteLine("Brak przedmiotów w ekwipunku!");
            return;
        }

        foreach (var item in items)
        {
            item.PrintStats();
            Console.WriteLine();
        }
    }

    private static void OnUseItem()
    {
        Console.Clear();
        var items = Globals.GameSession!.Equipment.GetNonActiveItems();
        if (items.Count == 0)
        {
            Console.WriteLine("Brak przedmiotów w ekwipunku!");
            return;
        }

        for (var i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]");
            items[i].PrintStats();
            Console.WriteLine();
        }

        var input = Helpers.GetInputInRange(1, items.Count);
        var selectedItem = items[input - 1];
        Globals.GameSession.Equipment.SetActiveItem(selectedItem.Id);

        if (selectedItem.Category == ItemCategory.Potion)
        {
            Console.WriteLine($"Użyto: {selectedItem.Name}");
        }
        else
        {
            Console.WriteLine($"Założono: {selectedItem.Name}");
        }
    }

    private static void OnCloseAction()
    {
        Helpers.ChangeView("OpenWorld:MainView");
    }
}
