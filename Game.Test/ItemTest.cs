using Game.Lib;

namespace Game.Test;

public class ItemTest
{
    public Item GetItem(ItemTier tier)
    {
        return new(
            name: "",
            description: "",
            price: 0,
            hPMod: 0,
            attackMod: 0,
            level: 1,
            tier: tier,
            category: ItemCategory.Weapon
        );
    }

    [Fact]
    public void TestSetLevel()
    {
        var item = GetItem(ItemTier.Common);
        item.SetLevel(2);
        Assert.Equal(20, item.Price);
        Assert.Equal(30, item.HPMod);
        Assert.Equal(30, item.AttackMod);
    }
}
