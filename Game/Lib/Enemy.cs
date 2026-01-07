namespace Game.Lib;

public class Enemy(
    int goldReward,
    int expReward,
    string className,
    string description,
    int maxHealth,
    int health,
    int attack,
    int level
) : BaseCharacter(className, description, maxHealth, health, attack, level)
{
    public int GoldReward { get; set; } = goldReward;
    public int ExpReward { get; set; } = expReward;

    public void SetLevel(int lvl)
    {
        Level = lvl;
        ScaleStats();
    }

    protected override void ScaleStats()
    {
        base.ScaleStats();
        ExpReward += 20 * Level;
        GoldReward += 20 * Level;
    }

    public void PrintStats()
    {
        PrintStatsBase(
            new Dictionary<string, string>
            {
                {
                    "Nagroda za pokonanie przeciwnika",
                    $"{GoldReward} sztuk złota i {ExpReward} punktów doświadczenia."
                },
            }
        );
    }
}
