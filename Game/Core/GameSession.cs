using Game.Lib;

namespace Game.Core;

public class GameSession
{
    public GameSession(PlayerCharacter playerCharacter)
    {
        PlayerCharacter = playerCharacter;
        Equipment = new Equipment(this);
    }

    public PlayerCharacter PlayerCharacter { get; set; }
    public Equipment Equipment { get; set; }
}
