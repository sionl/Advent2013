﻿public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            WriteError("Please provide day as input argument");
            return;
        }

        var day = args[0];
        if (!int.TryParse(day, out int dayInt))
        {
            WriteError("Not a valday day input");
            return;
        }

        var name = $"Day{day}.Solution";
        var solutionType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(ISolution).IsAssignableFrom(p))
            .FirstOrDefault(x => x.FullName == name);

        if (solutionType == null)
        {
            WriteError("No solution found");
            return;
        }

        var solution = Activator.CreateInstance(solutionType) as ISolution;
        if (solution == null)
        {
            WriteError("No solution cound be created");
            return;
        }

        Console.WriteLine($"Solving Day {day}");
        solution.Run();
    }

    private static void WriteError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ForegroundColor = ConsoleColor.White;
    }
}