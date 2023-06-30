using TextGame.turn;

namespace TextGame.characters.heroes.playable;

public class Mignis : Character
{
    public bool Revived = false;
    
    public Mignis() : base("Mignis")
    {
        Description = "AAAAAAAAAAAAAAAAAAAAA.";
        HealthPoints = 500;
        MaxHealthPoints = HealthPoints;
        DefensePoints = 20;
        AttackPoints = 30;
        SuperPoints = 0;
    }

    public override void TellAttack(int damage, Turn turn)
    {
        turn.NextEvents.Add($"Mignis lance une attaque de {damage} points.");
    }

    public override void Special(Character enemy, Turn turn)
    {
        turn.NextEvents.Add("La compétence ultime de Mignis le soigne ! [+10 PV]");
        HealthPoints += 10;
    }
}