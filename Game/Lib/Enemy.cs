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
}
