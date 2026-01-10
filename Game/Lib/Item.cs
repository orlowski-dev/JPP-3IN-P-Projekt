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
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public int Price { get; set; } = price;
    public int HPMod { get; set; } = hPMod;
    public int AttackMod { get; set; } = attackMod;
    public int Level { get; set; } = level;
    public ItemTier Tier { get; set; } = tier;
    public ItemCategory Category { get; set; } = category;

    public Item Clone()
    {
        return new(
            name: Name,
            description: Description,
            price: Price,
            hPMod: HPMod,
            attackMod: AttackMod,
            level: Level,
            tier: Tier,
            category: Category
        );
    }

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

    public void PrintStats(bool printPrice = false)
    {
        var stats = new Dictionary<string, string>
        {
            { "Nazwa", Name },
            { "Opis", Description },
            { "Tier", GetTier() },
            { "Kategoria", GetCategory() },
            { "Poziom", Level.ToString() },
        };

        if (HPMod > 0)
        {
            stats.Add("HP", $"+{HPMod}");
        }

        if (AttackMod > 0)
        {
            stats.Add("Atak", $"+{AttackMod}");
        }

        if (printPrice)
        {
            stats.Add("Cena", $"{Price} sztuk złota");
        }

        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }
    }
}
