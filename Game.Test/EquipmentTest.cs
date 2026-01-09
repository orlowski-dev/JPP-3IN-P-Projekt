using Game.Core;
using Game.Lib;

namespace Game.Test;

public class EquipmentTest
{
    public static List<Item> InitItems()
    {
        var item1 = new Item(
            name: "",
            description: "",
            price: 1,
            hPMod: 1,
            attackMod: 1,
            level: 1,
            tier: ItemTier.Common,
            category: ItemCategory.Armor
        );
        var item2 = new Item(
            name: "",
            description: "",
            price: 1,
            hPMod: 1,
            attackMod: 1,
            level: 1,
            tier: ItemTier.Common,
            category: ItemCategory.Weapon
        );

        return [item1, item2];
    }

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
    public void TestAddAndRemoveItem()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
        var eq = Globals.GameSession.Equipment;
        var items = InitItems();

        eq.AddItem(items[0]);
        eq.AddItem(items[1]);
        Assert.Equal([items[0], items[1]], eq.Items);
        eq.RemoveItem(items[0].Id);
        Assert.Equal([items[1]], eq.Items);
    }

    [Fact]
    public void TestSetAndUnsetActiveItem()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
        var eq = Globals.GameSession.Equipment;
        var player = Globals.GameSession.PlayerCharacter;
        var items = InitItems();
        items.Add(
            new(
                name: "",
                description: "",
                price: 1,
                hPMod: 1,
                attackMod: 1,
                level: 1,
                tier: ItemTier.Common,
                category: ItemCategory.Weapon
            )
        );

        foreach (var item in items)
        {
            eq.AddItem(item);
        }

        eq.SetActiveItem(eq.Items[0].Id);
        eq.SetActiveItem(eq.Items[1].Id);
        eq.SetActiveItem(eq.Items[2].Id);

        Assert.Equal(items[0], eq.ActiveArmor);
        Assert.Equal(items[2], eq.EquippedWeapon);
        Assert.Equal(102, player.MaxHealth);
        Assert.Equal(12, player.Attack);

        eq.UnsetActiveItem(eq.Items[0].Id);
        Assert.Null(eq.ActiveArmor);
        Assert.Equal(items[2], eq.EquippedWeapon);

        eq.UnsetActiveItem(eq.Items[2].Id);
        Assert.Null(eq.EquippedWeapon);

        Assert.Equal(100, player.MaxHealth);
        Assert.Equal(10, player.Attack);
    }
}
