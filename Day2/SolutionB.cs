namespace Day2;

public class Solution : ISolution
{
    private class Game()
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }

    private OutputBuilder builder = new();

    public void Run()
    {
        var gamesList = File.ReadAllText("Day2\\Input1.txt").Split(Environment.NewLine);

        var totalPower = 0;
        foreach (var gameLine in gamesList)
        {
            var gameArray = gameLine.Split(":");
            var gameNumber = int.Parse(gameArray[0].Replace("Game ", ""));

            var drawLine = gameArray[1];
            var game = GetGame(drawLine);
            var power = game.Red * game.Green * game.Blue;
            builder.AppendLine($"Game {gameNumber}: {game.Red} red, {game.Green} green, {game.Blue} blue - Power: {power}");
            totalPower += power;
        }

        builder.AppendNewLine();
        builder.AppendLine("Total Power");
        builder.AppendLine(totalPower.ToString());
        builder.WriteToFile("Day2\\Output.txt");
    }

    private Game GetGame(string drawLine)
    {
        var game = new Game();
        var drawArray = drawLine.Split(";");
        foreach (var draw in drawArray)
        {
            var newDraw = GetDraw(draw);
            if (newDraw.Red > game.Red)
            {
                game.Red = newDraw.Red;
            }
            if (newDraw.Green > game.Green)
            {
                game.Green = newDraw.Green;
            }
            if (newDraw.Blue > game.Blue)
            {
                game.Blue = newDraw.Blue;
            }
        }
        return game;
    }

    private Game GetDraw(string draw)
    {
        var game = new Game();
        var cubeArray = draw.Split(",");
        foreach (var cube in cubeArray)
        {
            if (cube.EndsWith("red"))
            {
                var cubeNumber = int.Parse(cube.Replace(" red", ""));
                game.Red = cubeNumber;
            }
            else if (cube.EndsWith("green"))
            {
                var cubeNumber = int.Parse(cube.Replace(" green", ""));
                game.Green = cubeNumber;
            }
            else if (cube.EndsWith("blue"))
            {
                var cubeNumber = int.Parse(cube.Replace(" blue", ""));
                game.Blue = cubeNumber;
            }
        }
        return game;
    }
}