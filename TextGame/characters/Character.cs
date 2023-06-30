using TextGame.main;
using TextGame.turn;

namespace TextGame.characters;

public abstract class Character
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public int MaxHealthPoints { get; set; }
    public int HealthPoints { get; set; }
    public int DefensePoints { get; set; }
    public int AttackPoints { get; set; }
    public int SuperPoints { get; set; }
    public bool Defending = false;
    
    protected Character(string name)
    {
        Name = name;
    }

    public int TakeDamage(int damage)
    {
        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            return 9999;
        }

        if (DefensePoints == 0)
        {
            TakeBruteDamage(damage);
            return damage;
        }

        var d = damage - DefensePoints / 3;

        if (Defending)
        {
            d -= d / 2; // prends la moitié des dégats si le perso se défend
            Defending = false;
        }

        HealthPoints -= d;

        return d;
    }

    public void TakeBruteDamage(int damage)
    {
        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            return;
        }
        
        HealthPoints -= damage;
    }

    public void Attack(Character enemy, Turn turn)
    {
        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            return;
        }

        var damage = new Random().Next(AttackPoints - 15, AttackPoints);

        var tookDamage = enemy.TakeDamage(damage);

        if (SuperPoints >= SuperBattelu.RequiredTurnsForSpecials)
        {
            SuperPoints = 0;
            Special(enemy, turn);
        }
        else
        {
            SuperPoints++;
            TellAttack(tookDamage, turn);
        }
    }

    public abstract void TellAttack(int damage, Turn turn);
    
    public abstract void Special(Character enemy, Turn turn);
}