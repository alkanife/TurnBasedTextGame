using TextGame.characters.heroes.playable;
using TextGame.main;
using TextGame.utils;

namespace TextGame.turn;

public class Turn
{
    public readonly SuperBattelu SuperBattelu;

    public List<string> LastEvents = new();
    public List<string> NextEvents = new();
    
    public Turn(SuperBattelu superBattelu)
    {
        SuperBattelu = superBattelu;
    }
    
    public void PlayerDeath()
    {
        LastEvents.Add("Vous succombez de vos blessures...");
        PrintStats();
        Console.WriteLine("Vous avez perdu !");
    }

    public void Start()
    {
        if (SuperBattelu.Hero.HealthPoints <= 0 && SuperBattelu.Hero.Name != "Mignis")
        {
            PlayerDeath();
            return;
        }
        
        if (SuperBattelu.Hero is { HealthPoints: <= 0, Name: "Mignis" })
        {
            var mignis = (Mignis)SuperBattelu.Hero;

            if (mignis.Revived)
            {
                PlayerDeath();
                return;
            }
            
            mignis.Revived = true;
            mignis.HealthPoints = 1;
            mignis.MaxHealthPoints = 1;
            mignis.DefensePoints = 0;
            mignis.AttackPoints *= 2;
            LastEvents.Add("Mignis utilise sa compétence DERNIER RAMPART !");
        }

        if (SuperBattelu.Orc.HealthPoints <= 0)
        {
            LastEvents.Add("L'orc tombe sur le sol.");
            PrintStats();
            Console.WriteLine("Vous avez gagné !");
            return;
        }
        
        SuperBattelu.Orc.CalculateRage(this);
        
        new EntityAction(this).AskPlayer();
    }

    public void End()
    {
        SuperBattelu.TurnNumber++;
        SuperBattelu.Turn = new Turn(SuperBattelu)
        {
            LastEvents = NextEvents
        };
        SuperBattelu.Turn.Start();
    }

    public void PrintStats()
    {
        Console.Clear();
        
        Console.WriteLine($"Tour n°{SuperBattelu.TurnNumber}");
        
        var table = new Table();
        table.SetShowVerticalLines(false);
        table.HorizontalSep = " ";
        const string vsSep = "   ";

        var hero = SuperBattelu.Hero;
        var orc = SuperBattelu.Orc;
        
        table.SetHeader(new[]
        {
            $"{SuperBattelu.Hero.Name}",
            " ",
            $"{SuperBattelu.Orc.Name}"
        });

        table.AddRow(new[]
        {
            $"Vie..........: {hero.HealthPoints}/{hero.MaxHealthPoints}",
            $"{vsSep} _  _  ___{vsSep}",
            $"Vie..........: {orc.HealthPoints}/{orc.MaxHealthPoints}",
        });

        table.AddRow(new[]
        {
            $"Défense......: {hero.DefensePoints}",
            $"{vsSep}( \\/ )/ __){vsSep}",
            $"Défense......: {orc.DefensePoints}",
        });

        table.AddRow(new[]
        {
            $"Attaque......: {hero.AttackPoints}",
            $"{vsSep} \\  / \\__ \\{vsSep}",
            $"Attaque......: {orc.AttackPoints}",
        });

        table.AddRow(new[]
        {
            $"Coup spécial.: {(hero.SuperPoints > SuperBattelu.RequiredTurnsForSpecials ? SuperBattelu.RequiredTurnsForSpecials : hero.SuperPoints)}/{SuperBattelu.RequiredTurnsForSpecials}",
            $"{vsSep}  \\/  (___/{vsSep}",
            $"Coup spécial.: {(orc.SuperPoints > SuperBattelu.RequiredTurnsForSpecials ? SuperBattelu.RequiredTurnsForSpecials : orc.SuperPoints)}/{SuperBattelu.RequiredTurnsForSpecials}"
        });

        table.AddRow(new[]
        {
            "Effet(s).....: aucun",
            " ",
            "Effet(s).....: aucun"
        });

        table.Print();
        
        foreach (var e in LastEvents)
        {
            Console.WriteLine(" * " + e);
        }
        Console.WriteLine();
    }
}