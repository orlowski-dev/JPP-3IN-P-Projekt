using Game.Lib;

namespace Game.Test;

public class EnemyTest
{
    [Fact]
    public void TestSetLevel()
    {
        var enemy = new Enemy(
            className: "",
            description: "",
            maxHealth: 100,
            health: 100,
            attack: 10,
            expReward: 10,
            goldReward: 10,
            level: 1
        );

        enemy.SetLevel(2);
        Assert.Equal(2, enemy.Level);
        Assert.Equal(200, enemy.MaxHealth);
        Assert.Equal(200, enemy.Health);
        Assert.Equal(42, enemy.Attack);
        Assert.Equal(50, enemy.ExpReward);
        Assert.Equal(50, enemy.GoldReward);
    }
}
