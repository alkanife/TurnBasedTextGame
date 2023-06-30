namespace TextGame.utils;

public class Utils
{
    public static void WaitForAnyKey()
    {
        if (!Console.ReadKey().Key.Equals(ConsoleKey.Escape)) return;
        
        Stop();
    }

    public static void Stop()
    {
        Console.WriteLine();
        Console.WriteLine("Quitté");
        Environment.Exit(0);
    }
}