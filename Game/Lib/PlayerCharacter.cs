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
}
