using System.Text.Json.Serialization;
using Game.Core;

namespace Game.Lib;

public class SaveData
{
    public SaveData(GameSession gameSession)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        GameSession = gameSession;
    }

    [JsonConstructor]
    public SaveData(GameSession gameSession, Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
        GameSession = gameSession;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public GameSession GameSession { get; set; }
}
