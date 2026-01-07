namespace Game.Lib;

public abstract class BaseCharacter(
    string className,
    string description,
    int maxHealth,
    int health,
    int attack,
    int level
)
{
    public string ClassName { get; set; } = className;
    public string Description { get; set; } = description;
    public int MaxHealth { get; set; } = maxHealth;
    public int Health { get; set; } = health;
    public int Attack { get; set; } = attack;
    public int Level { get; set; } = level;

    protected Dictionary<string, string> GetBaseStats()
    {
        return new Dictionary<string, string>
        {
            { "Klasa", ClassName },
            { "Opis", Description },
            { "HP", Health.ToString() },
            { "Atak", Attack.ToString() },
            { "Poziom", Level.ToString() },
        };
    }

    public void PrintBaseStats()
    {
        foreach (var stat in GetBaseStats())
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }
    }

    protected void PrintStatsBase(Dictionary<string, string>? extra = null)
    {
        var stats = Helpers.ConcatTwoDicts(GetBaseStats(), extra);
        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }
    }

    protected virtual void ScaleStats()
    {
        MaxHealth += 50 * Level;
        Health = MaxHealth;
        Attack += 16 * Level;
    }
}
