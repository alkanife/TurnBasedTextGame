using TextGame.turn;

namespace TextGame.characters.heroes.playable;

public class Adam : Character
{
    public Adam() : base("Adam")
    {
        Description = "bla bla blblblblblalbalablablablalb";
        HealthPoints = 400;
        MaxHealthPoints = HealthPoints;
        DefensePoints = 10;
        AttackPoints = 50;
        SuperPoints = 0;
    }

    public override void TellAttack(int damage, Turn turn)
    {
        turn.NextEvents.Add($"Adam lance une attaque de {damage} points.");
    }

    public override void Special(Character enemy, Turn turn)
    {
        turn.NextEvents.Add("La compétence ultime d'Adam inflige 20 points de dégats brutes à l'Orc !");
        enemy.TakeBruteDamage(20);
    }
}