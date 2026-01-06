namespace Game.Lib;

public class InitialData(
    List<PlayerCharacter> playerCharacters,
    List<Enemy> enemies,
    List<Item> items
)
{
    public List<PlayerCharacter> PlayerCharacters { get; set; } = playerCharacters;
    public List<Enemy> Enemies { get; set; } = enemies;
    public List<Item> Items { get; set; } = items;
}
