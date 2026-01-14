using Game.Lib;

namespace Game.Test;

public class PlayerCharacterTest
{
    public static PlayerCharacter GetPlayerCharacter()
    {
        return new(
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
    }

    [Fact]
    public void TestLevelUp()
    {
        var player = GetPlayerCharacter();

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
        var player = GetPlayerCharacter();

        player.AddExp(200);
        Assert.Equal(3, player.Level);
        Assert.Equal(200, player.Exp);
        Assert.Equal(300, player.NextLevelReqExp);
        Assert.Equal(750, player.Gold);
    }

    [Fact]
    public void TestUseAndStopUsing()
    {
        var player = GetPlayerCharacter();
        var armor = new Item(
            name: "",
            description: "",
            price: 1,
            hPMod: 1,
            attackMod: 1,
            level: 1,
            tier: ItemTier.Common,
            category: ItemCategory.Armor
        );
        var weapon = new Item(
            name: "",
            description: "",
            price: 1,
            hPMod: 1,
            attackMod: 1,
            level: 1,
            tier: ItemTier.Common,
            category: ItemCategory.Weapon
        );

        player.UseItem(armor);
        Assert.Equal(101, player.MaxHealth);
        Assert.Equal(11, player.Attack);

        player.UseItem(weapon);
        Assert.Equal(102, player.MaxHealth);
        Assert.Equal(12, player.Attack);

        player.StopUsingItem(armor);
        Assert.Equal(101, player.MaxHealth);
        Assert.Equal(11, player.Attack);

        player.StopUsingItem(weapon);
        Assert.Equal(100, player.MaxHealth);
        Assert.Equal(10, player.Attack);
    }
}
