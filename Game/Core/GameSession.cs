using Game.Lib;

namespace Game.Core;

public class GameSession
{
    public GameSession(PlayerCharacter playerCharacter, Equipment? eq = null)
    {
        PlayerCharacter = playerCharacter;
        Equipment = eq ?? new Equipment(this);
    }

    public PlayerCharacter PlayerCharacter { get; set; }
    public Equipment Equipment { get; set; }
}
