using Game.Core;
using Game.Lib;

namespace Game.Test;

public class SaveSystemTest
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

    private static readonly string _dirName = "testsavedata";

    [Fact]
    public void TestGetSaveDataDirPath()
    {
        var exists = Directory.Exists(_dirName);
        var (path, created) = SaveSystem.GetSaveDataDirPath(_dirName);

        Assert.Contains(_dirName, path);
        Assert.Equal(exists, !created);
    }

    [Fact]
    public void TestSaveGame()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
        var (saved, _, _) = SaveSystem.SaveGame(_dirName);
        Assert.True(saved);
    }

    [Fact]
    public void TestLoadSaveGame()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
        var (saved, _, path) = SaveSystem.SaveGame(_dirName);
        Assert.True(saved);

        var saveData = SaveSystem.LoadSaveGameFile(path);

        Assert.NotNull(saveData);
        Assert.True(saveData.GetType() == typeof(SaveData));
    }

    [Fact]
    public void TestGetSaveFiles()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
        var (saved, _, path) = SaveSystem.SaveGame(_dirName);
        Assert.True(saved);

        var files = SaveSystem.GetSaveFiles(_dirName);
        Assert.Contains(path, files);
    }

    [Fact]
    public void TestSaveAndLoadGame()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
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
        Globals.GameSession.Equipment.AddItem(item1);
        var (saved, _, path) = SaveSystem.SaveGame(_dirName);
        Assert.True(saved);

        var saveData = SaveSystem.LoadSaveGameFile(path);
        Assert.NotNull(saveData);
        Assert.Equal([item1], saveData.GameSession.Equipment.Items);
    }
}
