using Game.Lib;

namespace Game.Core;

public class GameSession
{
    public GameSession(
        PlayerCharacter playerCharacter,
        Equipment? equipment = null,
        List<Item>? shopItems = null
    )
    {
        PlayerCharacter = playerCharacter;
        Equipment = equipment ?? new Equipment(this);
        ShopItems = shopItems ?? InitShop();
    }

    public PlayerCharacter PlayerCharacter { get; set; }
    public Equipment Equipment { get; set; }
    public List<Item> ShopItems { get; set; }

    private static List<Item> InitShop()
    {
        if (Globals.InitialData == null)
        {
            var loaded = Helpers.LoadInitData();

            if (!loaded)
            {
                throw new Exception("Nie mozna ustaiwć initData!");
            }
        }
        var items = new List<Item>();
        foreach (var item in Globals.InitialData!.Items)
        {
            items.Add(item.Clone());
        }
        return items;
    }
}
