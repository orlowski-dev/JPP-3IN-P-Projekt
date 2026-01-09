using Game.Lib;

namespace Game.Core;

public enum CombatStatus
{
    During,
    PlayerWon,
    EnenyWon,
    Escaped,
};

public class CombatSession(Enemy enemy)
{
    public bool PlayerTurn { get; set; } = true;
    public CombatStatus Status { get; set; } = CombatStatus.During;
    public Enemy Enemy { get; set; } = enemy;
    public bool NextAttackBlocked = false;

    public int Attack<A, E>(A attacker, E enemy)
        where A : BaseCharacter
        where E : BaseCharacter
    {
        if (NextAttackBlocked)
        {
            NextAttackBlocked = false;
            PlayerTurn = !PlayerTurn;
            return 0;
        }

        var damage = enemy.TakeDamage(attacker.Attack);

        // jeżeli atakującym jest gracz i przeciwnik weźmie i umrze to wtedy wygrywa
        if (attacker.GetType() == typeof(PlayerCharacter))
        {
            PlayerTurn = false;

            if (enemy.Health <= 0)
            {
                Status = CombatStatus.PlayerWon;
            }
        }
        else
        {
            PlayerTurn = true;
            if (enemy.Health <= 0)
            {
                Status = CombatStatus.EnenyWon;
            }
        }

        return damage;
    }

    public void BlockAttack()
    {
        NextAttackBlocked = true;
        PlayerTurn = !PlayerTurn;
    }

    public bool EscapeCombat()
    {
        // ucieć może tylko gracz
        var chance = new Random().Next(0, 100);
        if (chance < 80)
        {
            Status = CombatStatus.Escaped;
            PlayerTurn = false;
            return true;
        }
        else
        {
            PlayerTurn = false;
            return false;
        }
    }

    public void AddRewardsForPlayer()
    {
        var drop = Globals
            .InitialData!.Items[new Random().Next(0, Globals.InitialData.Items.Count)]
            .Clone();
        drop.SetLevel(Globals.GameSession!.PlayerCharacter.Level);
        Globals.GameSession.Equipment.AddItem(drop);
        Globals.GameSession.PlayerCharacter.AddExp(Enemy.ExpReward);
        Globals.GameSession.PlayerCharacter.AddGold(Enemy.GoldReward);
        Console.WriteLine($"Złoto: {Enemy.GoldReward}");
        Console.WriteLine($"Punkty doświadczenia: {Enemy.ExpReward}");
        Console.WriteLine($"Przedmiot: {drop.Name}");
    }
}
