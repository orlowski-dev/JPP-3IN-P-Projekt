using Game.Lib;

namespace Game.Core;

public class GameSession
{
    public GameSession(
        PlayerCharacter playerCharacter,
        Equipment? eq = null,
        List<Item>? shopItems = null
    )
    {
        PlayerCharacter = playerCharacter;
        Equipment = eq ?? new Equipment(this);
        ShopItems = shopItems ?? InitShop();
    }

    public PlayerCharacter PlayerCharacter { get; set; }
    public Equipment Equipment { get; set; }
    public List<Item> ShopItems { get; set; }

    private static List<Item> InitShop()
    {
        var items = new List<Item>();

        foreach (var item in Globals.InitialData!.Items)
        {
            items.Add(item.Clone());
        }
        return items;
    }
}
