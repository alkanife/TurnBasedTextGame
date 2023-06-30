using TextGame.main;
using TextGame.utils;

namespace TextGame.turn;

public class EntityAction
{

    private readonly Turn _turn;
    private int _index;

    public EntityAction(Turn turn)
    {
        _turn = turn;
    }

    public void AskPlayer()
    {
        _turn.PrintStats();
        Console.WriteLine(" Choisissez votre prochain mouvement :");
        Console.WriteLine();

        printAction(0, "ATTAQUER");
        printAction(1, "DEFENDRE");
        printAction(2, "PASSER");
        Console.WriteLine();

        var key = Console.ReadKey().Key;

        switch (key)
        {
            case ConsoleKey.Enter:
                CalculatePlayerAction();
                break;
            
            case ConsoleKey.LeftArrow:
                AlterIndex(-1);
                AskPlayer();
                break;
            
            case ConsoleKey.RightArrow:
                AlterIndex(1);
                AskPlayer();
                break;
            
            case ConsoleKey.Escape:
                Utils.Stop();
                break;
            
            default:
                AskPlayer();
                break;
        }
    }

    public void CalculatePlayerAction()
    {
        switch (_index)
        {
            case 0:
                // Attack
                _turn.SuperBattelu.Hero.Attack(_turn.SuperBattelu.Orc, _turn);
                break;
            
            case 1:
                // Defend
                _turn.SuperBattelu.Hero.Defending = true;
                _turn.NextEvents.Add($"Vous vous défendez.");
                break;
            
            default:
                _turn.NextEvents.Add("Vous passez votre tour.");
                break;
        }
        
        DoOrc();
        _turn.End();
    }

    public void DoOrc()
    {
        var rand = new Random().Next(0, 2);

        if (rand == 0) // attaque
        {
            _turn.SuperBattelu.Orc.Attack(_turn.SuperBattelu.Hero, _turn);
        }
        else // défendre
        {
            _turn.SuperBattelu.Orc.Defending = true;
            _turn.NextEvents.Add($"L'orc se défend.");
        }
    }

    private void AlterIndex(int n)
    {
        var newIndex = _index + n;

        if (newIndex < 0)
            return;
        
        if (newIndex > 2)
            return;
        
        _index = newIndex;
    }

    private void printAction(int index, string name)
    {
        if (_index == index)
            Console.Write($"{AnsiColors.Red}      [{name}]{AnsiColors.Reset}");
        else
            Console.Write("      [" + name + "]");
    }
}