namespace Game.Lib;

public enum ItemTier : ushort
{
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Legendary = 4,
    Mythic = 5,
};

public enum ItemCategory : ushort
{
    Weapon = 1,
    Armor = 2,
    Potion = 3,
};

public class Item(
    string name,
    string description,
    int price,
    int hPMod,
    int attackMod,
    int level,
    ItemTier tier,
    ItemCategory category
)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public int Price { get; private set; } = price;
    public int HPMod { get; private set; } = hPMod;
    public int AttackMod { get; private set; } = attackMod;
    public int Level { get; private set; } = level;
    public ItemTier Tier { get; private set; } = tier;
    public ItemCategory Category { get; private set; } = category;

    public string GetTier()
    {
        return Tier switch
        {
            ItemTier.Common => "Zwykły",
            ItemTier.Uncommon => "Niezwykły",
            ItemTier.Rare => "Rzadki",
            ItemTier.Legendary => "Legendarny",
            _ => "Mityczny",
        };
    }

    public string GetCategory()
    {
        return Category switch
        {
            ItemCategory.Weapon => "Broń",
            ItemCategory.Armor => "Zbroja",
            _ => "Mikstura",
        };
    }

    private void ScaleStats()
    {
        Price += 10 * Level * (int)Tier;
        HPMod += 15 * Level * (int)Tier;
        AttackMod += 15 * Level * (int)Tier;
    }

    public void SetLevel(int lvl)
    {
        // przy założeniu że level ustawiany jest tylko raz :)
        Level = lvl;
        ScaleStats();
    }
}
