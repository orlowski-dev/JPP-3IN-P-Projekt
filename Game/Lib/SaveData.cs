using Game.Core;

namespace Game.Lib;

public class SaveData(GameSession gameSession)
{
    public GameSession GameSession { get; set; } = gameSession;
}
