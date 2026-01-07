using Game.Lib;

namespace Game.Test;

public class PlayerCharacterTest
{
    [Fact]
    public void TestLevelUp()
    {
        var player = new PlayerCharacter(
            name: "",
            gold: 0,
            exp: 0,
            nextLevelReqExp: 50,
            className: "",
            description: "",
            maxHealth: 100,
            health: 100,
            attack: 10,
            level: 1
        );

        player.AddExp(50);
        Assert.Equal(2, player.Level);
        Assert.Equal(50, player.Exp);
        Assert.Equal(150, player.NextLevelReqExp);
        Assert.Equal(300, player.Gold);
        Assert.Equal(200, player.Health);
        Assert.Equal(200, player.MaxHealth);
        Assert.Equal(42, player.Attack);
    }

    [Fact]
    public void Test2LevelsUp()
    {
        var player = new PlayerCharacter(
            name: "",
            gold: 0,
            exp: 0,
            nextLevelReqExp: 50,
            className: "",
            description: "",
            maxHealth: 100,
            health: 100,
            attack: 10,
            level: 1
        );

        player.AddExp(200);
        Assert.Equal(3, player.Level);
        Assert.Equal(200, player.Exp);
        Assert.Equal(300, player.NextLevelReqExp);
        Assert.Equal(750, player.Gold);
    }
}
