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
}
