namespace Day4;

public class SolutionA
{
    public void Run()
    {
        var data = File.ReadAllText("Day4\\Input1.txt").Split(Environment.NewLine);
        OutputBuilder builder = new();

        int totalPoints = 0;
        foreach (var line in data)
        {
            var cardArray = line.Split(':');
            var array = cardArray[1].Split('|');
            var winnersLine = array[0].Trim();
            var numbersLine = array[1].Trim();

            var winners = winnersLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToHashSet();
            var numbers = numbersLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim()));

            var score = 0;
            foreach (var num in numbers)
            {
                if (winners.Contains(num))
                {
                    if (score > 0)
                    {
                        score = score * 2;
                    }
                    else
                    {
                        score = 1;
                    }
                }
            }
            builder.Append(cardArray[0]).Append(" : ").Append(score.ToString()).AppendNewLine();
            totalPoints += score;
        }

        builder.AppendNewLine();
        builder.AppendLine("Total Points");
        builder.AppendLine(totalPoints.ToString());
        builder.WriteToFile("Day4\\Output.txt");
    }
}