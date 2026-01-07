namespace Game.Lib;

public class PlayerCharacter(
    string name,
    int gold,
    int exp,
    int nextLevelReqExp,
    string className,
    string description,
    int maxHealth,
    int health,
    int attack,
    int level
) : BaseCharacter(className, description, maxHealth, health, attack, level)
{
    public string Name { get; set; } = name;
    public int Gold { get; set; } = gold;
    public int Exp { get; set; } = exp;
    public int NextLevelReqExp { get; set; } = nextLevelReqExp;

    public void PrintStats()
    {
        PrintStatsBase(
            new Dictionary<string, string>
            {
                { "Maks. HP", MaxHealth.ToString() },
                { "Doświadczenie", $"{Exp}/{NextLevelReqExp}" },
                { "Złoto", Gold.ToString() },
            }
        );
    }

    public void AddExp(int amount)
    {
        for (var i = 1; i <= amount; i++)
        {
            Exp += 1;
            if (Exp == NextLevelReqExp)
            {
                Level += 1;
                ScaleStats();
            }
        }
    }

    protected override void ScaleStats()
    {
        base.ScaleStats();
        NextLevelReqExp += 50 * Level;
        Gold += 150 * Level;
    }
}
