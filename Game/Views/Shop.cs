using Game.Core;
using Game.Lib;

namespace Game.Views;

public static class Shop
{
    private static List<MenuOption> _mainViewOptions =
    [
        new("help", "Wyświetl pomoc.", OnHelpAction),
        new("list", "Wypisz dostępne przedmioty do kupienia.", OnListItemsAction),
        new("buy", "Kup przedmiot", OnBuyItem),
        new(
            "close",
            "Opuść sklep.",
            () =>
            {
                Helpers.ChangeView("OpenWorld:MainView");
            }
        ),
    ];

    public static void MainView()
    {
        Helpers.PrintTitle("Sklep");
        while (true)
        {
            var command = Helpers.GetInput<string>();
            if (Helpers.ExecuteCommand(command, ref _mainViewOptions))
            {
                return;
            }
        }
    }

    private static void OnListItemsAction()
    {
        Console.Clear();
        var items = Globals.GameSession!.ShopItems;
        for (var i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]");
            items[i].PrintStats(printPrice: true);
            Console.WriteLine();
        }
    }

    private static void OnHelpAction()
    {
        Console.Clear();
        Helpers.PrintTitle("Pomoc");
        Helpers.PrintMenuOptions(ref _mainViewOptions);
        Console.WriteLine();
    }

    private static void OnBuyItem()
    {
        Console.Clear();
        OnListItemsAction();
        var items = Globals.GameSession!.ShopItems;
        var input = Helpers.GetInputInRange(1, items.Count);
        var selectedItem = items[input - 1].Clone();
        var player = Globals.GameSession.PlayerCharacter;
        if (selectedItem.Price > player.Gold)
        {
            Console.WriteLine("Nie stać cię na ten przedmiot!");
            return;
        }
        Globals.GameSession.Equipment.AddItem(selectedItem);
        player.SubstractGold(selectedItem.Price);
        Console.WriteLine($"Kupiono przedmiot: {selectedItem.Name}");
    }

    public static void RefreshShop()
    {
        var items = new List<Item>();
        foreach (var item in Globals.InitialData!.Items)
        {
            item.Clone();
            item.SetLevel(Globals.GameSession!.PlayerCharacter.Level);
            items.Add(item);
        }
        Globals.GameSession!.ShopItems = items;
    }
}
