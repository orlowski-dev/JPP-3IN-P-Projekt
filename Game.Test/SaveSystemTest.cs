using Game.Core;
using Game.Lib;

namespace Game.Test;

public class SaveSystemTest : BaseTest
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
        var (saved, _, _, _) = SaveSystem.SaveGame(_dirName);
        Assert.True(saved);
    }

    [Fact]
    public void TestLoadSaveGame()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
        var (saved, _, path, _) = SaveSystem.SaveGame(_dirName);
        Assert.True(saved);

        var saveData = SaveSystem.LoadSaveGameFile(path);

        Assert.NotNull(saveData);
        Assert.True(saveData.GetType() == typeof(SaveData));
    }

    [Fact]
    public void TestGetSaveFiles()
    {
        Globals.GameSession = new GameSession(GetPlayerCharacter());
        var (saved, _, path, _) = SaveSystem.SaveGame(_dirName);
        Assert.True(saved);

        var files = SaveSystem.GetSaveFiles(_dirName);
        Assert.Contains(path, files);
    }
}
