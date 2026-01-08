using Game.Core;
using Game.Lib;

namespace Game.Test;

public class CombatSessionTest
{
    private (PlayerCharacter, Enemy) Init()
    {
        var player = new PlayerCharacter(
            name: "",
            gold: 0,
            exp: 0,
            nextLevelReqExp: 50,
            className: "",
            description: "",
            maxHealth: 10,
            health: 10,
            attack: 10,
            level: 1
        );
        var enemy = new Enemy(
            className: "",
            description: "",
            maxHealth: 10,
            health: 10,
            attack: 10,
            expReward: 10,
            goldReward: 10,
            level: 1
        );

        Globals.GameSession = new GameSession(player);
        Globals.CombatSession = new CombatSession(enemy);
        return (player, enemy);
    }

    [Fact]
    public void TestPlayerWon()
    {
        var (player, enemy) = Init();
        Globals.CombatSession!.Attack(player, enemy);
        Assert.Equal(CombatStatus.PlayerWon, Globals.CombatSession.Status);
        Assert.False(Globals.CombatSession.PlayerTurn);
    }

    [Fact]
    public void TestEnemyWon()
    {
        var (player, enemy) = Init();
        Globals.CombatSession!.Attack(enemy, player);
        Assert.Equal(CombatStatus.EnenyWon, Globals.CombatSession.Status);
        Assert.True(Globals.CombatSession.PlayerTurn);
    }
}
