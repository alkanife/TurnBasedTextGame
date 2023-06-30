using TextGame.characters;
using TextGame.characters.heroes;
using TextGame.characters.heroes.playable;
using TextGame.turn;
using TextGame.utils;

namespace TextGame.main;

public class SuperBattelu
{
    
    public readonly List<Character> Heroes = new();
    public Character Hero;
    public Orc Orc;
    public Turn Turn;
    public int TurnNumber = 1;
    public static int RequiredTurnsForSpecials = 5;

    public SuperBattelu()
    {
        // Adding heroes
        Heroes.Add(new Adam());
        Heroes.Add(new Mignis());
        Heroes.Add(new Rahvn());
        
        // Initializing orc
        Orc = new Orc();
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine(" _____                        ______       _   _       _         _____ _____ _____ ");
        Console.WriteLine("/  ___|                       | ___ \\     | | | |     | |       |_   _|_   _|_   _|");
        Console.WriteLine("\\ `--. _   _ _ __   ___ _ __  | |_/ / __ _| |_| |_ ___| |_   _    | |   | |   | |  ");
        Console.WriteLine(" `--. \\ | | | '_ \\ / _ \\ '__| | ___ \\/ _` | __| __/ _ \\ | | | |   | |   | |   | |  ");
        Console.WriteLine("/\\__/ / |_| | |_) |  __/ |    | |_/ / (_| | |_| ||  __/ | |_| |  _| |_ _| |_ _| |_ ");
        Console.WriteLine("\\____/ \\__,_| .__/ \\___|_|    \\____/ \\__,_|\\__|\\__\\___|_|\\__,_|  \\___/ \\___/ \\___/ ");
        Console.WriteLine("            | |           ");
        Console.WriteLine("            |_|        ");
        Console.WriteLine("");
        Console.WriteLine("Blabla blablalblblblblb");
        
        Utils.WaitForAnyKey();
        new HeroBrowser(this).Browse();
        
        Console.Clear();
        
        Turn = new Turn(this);
        Turn.LastEvents = new[]
        {
            "Vous approchez de l'orc des montagnes.",
            "Vous vous regardez dans les yeux en vous demandant qui va commencer le combat."
        }.ToList();
        Turn.Start();
    }
}