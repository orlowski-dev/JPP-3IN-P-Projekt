using System.Text.Json.Serialization;
using Game.Core;

namespace Game.Lib;

public class Equipment
{
    public List<Item> Items { get; set; } = [];
    public Item? ActiveArmor { get; set; }
    public Item? EquippedWeapon { get; set; }

    private PlayerCharacter? _player;

    // konstrukor dla json
    [JsonConstructor]
    public Equipment()
    {
        _player = null;
    }

    // konsturktor dla GameSession
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

    public void RemoveItem(Guid id)
    {
        Items = Items.FindAll((i) => i.Id != id);
    }

    public void SetActiveItem(Guid id)
    {
        if (_player == null)
        {
            throw new Exception("_player jest null!");
        }
        var item = Items.Find((i) => i.Id == id);
        if (item == null)
            return;
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
            case ItemCategory.Potion:
                _player.UsePotion(item);
                RemoveItem(item.Id);
                break;
            default:
                throw new Exception("Nieobsługiwana kategoria przedmiotu!");
        }
    }

    public void UnsetActiveItem(Guid id)
    {
        if (_player == null)
        {
            throw new Exception("_player jest null!");
        }

        var item = Items.Find((i) => i.Id == id);
        if (item == null)
            return;

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

    public List<Item> GetNonActiveItems()
    {
        return Items.FindAll((i) => i != ActiveArmor && i != EquippedWeapon);
    }

    public List<Item> GetItemsByCategory(ItemCategory category)
    {
        return Items.FindAll((item) => item.Category == category);
    }

    public void SetPlayer(PlayerCharacter playerCharacter)
    {
        _player = playerCharacter;
    }
}
