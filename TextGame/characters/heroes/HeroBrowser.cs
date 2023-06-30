using TextGame.main;
using TextGame.utils;

namespace TextGame.characters.heroes;

public class HeroBrowser
{

    private SuperBattelu _superBattelu;
    
    private int _currentIndex;
    
    public HeroBrowser(SuperBattelu superBattelu)
    {
        _superBattelu = superBattelu;
    }

    public void Browse()
    {
        var browse = true;
        while (browse)
        {
            Console.WriteLine(_currentIndex);
            ShowHero(_superBattelu.Heroes[_currentIndex]);
            
            var key = Console.ReadKey().Key;
            
            switch (key)
            {
                case ConsoleKey.Escape:
                    browse = false;
                    Utils.Stop();
                    break;
                
                case ConsoleKey.RightArrow:
                    AlterIndex(1);
                    break;
                
                case ConsoleKey.LeftArrow:
                    AlterIndex(-1);
                    break;
                
                case ConsoleKey.Enter:
                    browse = false;
                    _superBattelu.Hero = _superBattelu.Heroes[_currentIndex];
                    break;
                
                default:
                    break;
            }
        }
    }

    private void AlterIndex(int n)
    {
        var newIndex = _currentIndex + n;

        if (!(newIndex < 0 || newIndex > (_superBattelu.Heroes.Count-1)))
            _currentIndex = newIndex;
    }

    private void ShowHero(Character hero)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($" {hero.Name}");
        Console.WriteLine($" {hero.Description}");
        Console.WriteLine();
        Console.WriteLine($"Attaque : {hero.AttackPoints}");
        Console.WriteLine($"Défense : {hero.DefensePoints}");
        Console.WriteLine($"Vie : {hero.HealthPoints}");
        Console.WriteLine();
    }
}