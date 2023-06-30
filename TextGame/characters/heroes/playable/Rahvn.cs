using TextGame.turn;

namespace TextGame.characters.heroes.playable;

public class Rahvn : Character
{
    public Rahvn() : base("Rahvn")
    {
        Description = "";
        HealthPoints = 1000;
        MaxHealthPoints = HealthPoints;
        DefensePoints = 20;
        AttackPoints = 30;
        SuperPoints = 0;
    }

    public override void TellAttack(int damage, Turn turn)
    {
        turn.NextEvents.Add($"Rahvn lance une attaque de {damage} points.");
    }

    public override void Special(Character enemy, Turn turn)
    {
        turn.NextEvents.Add($"Rahvn fait son coup spécial ! L'orc perd 20 points de vie.");
        enemy.TakeDamage(20);
    }
}