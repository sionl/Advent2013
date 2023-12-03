namespace Day2;

public class Solution : ISolution
{
    private static readonly int MaxRed = 12;
    private static readonly int MaxGreen = 13;
    private static readonly int MaxBlue = 14;

    private OutputBuilder builder = new();

    public void Run()
    {
        var gamesList = File.ReadAllText("Day2\\Input1.txt").Split(Environment.NewLine);

        var validGames = new List<int>();
        foreach (var game in gamesList)
        {
            var gameArray = game.Split(":");
            var gameNumber = int.Parse(gameArray[0].Replace("Game ", ""));

            var drawLine = gameArray[1];
            if (ValidGame(gameNumber, drawLine))
            {
                validGames.Add(gameNumber);
            }
        }

        builder.AppendNewLine();
        builder.AppendLine("Valid Games Total");
        builder.AppendLine(validGames.Sum().ToString());

        using (StreamWriter writer = new StreamWriter("Day2\\Output.txt"))
        {
            writer.Write(builder.ToString());
        }
    }

    private bool ValidGame(int gameNumber, string drawLine)
    {
        var drawArray = drawLine.Split(";");
        foreach (var draw in drawArray)
        {
            if (!ValidDraw(gameNumber, draw))
            {
                return false;
            }
        }
        builder.AppendLine($"Game {gameNumber} - is Valid");
        return true;
    }

    private bool ValidDraw(int gameNumber, string draw)
    {
        var cubeArray = draw.Split(",");
        foreach (var cube in cubeArray)
        {
            if (cube.EndsWith("red"))
            {
                var cubeNumber = int.Parse(cube.Replace(" red", ""));
                if (cubeNumber > MaxRed)
                {
                    builder.AppendLine($"Game {gameNumber} - has {cubeNumber} number of reds");
                    return false;
                }
            }
            else if (cube.EndsWith("green"))
            {
                var cubeNumber = int.Parse(cube.Replace(" green", ""));
                if (cubeNumber > MaxGreen)
                {
                    builder.AppendLine($"Game {gameNumber} - has {cubeNumber} number of green");
                    return false;
                }
            }
            else if (cube.EndsWith("blue"))
            {
                var cubeNumber = int.Parse(cube.Replace(" blue", ""));
                if (cubeNumber > MaxBlue)
                {
                    builder.AppendLine($"Game {gameNumber} - has {cubeNumber} number of blue");
                    return false;
                }
            }
        }
        return true;
    }
}