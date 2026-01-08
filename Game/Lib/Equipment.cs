using Game.Core;

namespace Game.Lib;

public class Equipment
{
    public List<Item> Items { get; set; } = new List<Item>();
    public Item? ActiveArmor { get; set; }
    public Item? EquippedWeapon { get; set; }
    private PlayerCharacter _player;

    public Equipment(GameSession gameSession)
    {
        if (gameSession == null)
        {
            throw new Exception(
                "GameSession musi być zainicjowane przed utworzeniem instacji Equipment!"
            );
        }
        _player = gameSession.PlayerCharacter;
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        Items = Items.FindAll((i) => i.Id != item.Id);
    }

    public void SetActiveItem(Item item)
    {
        switch (item.Category)
        {
            case ItemCategory.Armor:
                if (ActiveArmor != null)
                {
                    _player.StopUsingItem(ActiveArmor);
                }
                ActiveArmor = item;
                _player.UseItem(ActiveArmor);
                break;
            case ItemCategory.Weapon:
                if (EquippedWeapon != null)
                {
                    _player.StopUsingItem(EquippedWeapon);
                }
                EquippedWeapon = item;
                _player.UseItem(EquippedWeapon);
                break;
            default:
                throw new Exception("Nieobsługiwana kategoria przedmiotu!");
        }
    }

    public void UnsetActiveItem(Item item)
    {
        switch (item.Category)
        {
            case ItemCategory.Armor:
                if (ActiveArmor != null)
                {
                    _player.StopUsingItem(ActiveArmor);
                }
                ActiveArmor = null;
                break;
            case ItemCategory.Weapon:
                if (EquippedWeapon != null)
                {
                    _player.StopUsingItem(EquippedWeapon);
                }
                EquippedWeapon = null;
                break;
            default:
                throw new Exception("Nieobsługiwana kategoria przedmiotu!");
        }
    }
}
