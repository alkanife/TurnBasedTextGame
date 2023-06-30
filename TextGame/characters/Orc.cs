using TextGame.turn;

namespace TextGame.characters;

public class Orc : Character
{
    public bool Enraged = false;
    
    public Orc() : base("Heapter l'Orc")
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
        turn.NextEvents.Add($"L'orc vous a fait une attaque de {damage} points.");
    }

    public override void Special(Character enemy, Turn turn)
    {
        var dmg = Enraged ? enemy.TakeDamage(70) : enemy.TakeDamage(15);
        
        turn.NextEvents.Add($"L'orc fait son COUP SPÉCIAL ! Cela vous inflige {dmg} points de dégats.");
    }

    public void CalculateRage(Turn turn)
    {
        if (HealthPoints < 100 && !Enraged)
        {
            Enraged = true;
            
            turn.LastEvents.Add($"L'orc devient enragé ! Il n'a plus de défense, mais ses dégats ont considérablement augmentés !");
            
            MaxHealthPoints = 300;
            DefensePoints = 0;
            AttackPoints = 60;
            SuperPoints = 0;
        }
        
    }
    
}