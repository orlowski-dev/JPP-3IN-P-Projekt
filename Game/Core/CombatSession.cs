using Game.Lib;

namespace Game.Core;

public enum CombatStatus
{
    During,
    PlayerWon,
    EnenyWon,
    Escaped,
};

public class CombatSession
{
    public CombatSession(Enemy enemy)
    {
        Enemy = enemy;
    }

    public Boolean PlayerTurn { get; set; } = true;
    public CombatStatus Status { get; set; } = CombatStatus.During;
    public Enemy Enemy { get; set; }
}
