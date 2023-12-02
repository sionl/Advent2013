using System.Globalization;
using System.Net.Http.Headers;
using Microsoft.VisualBasic;

namespace Day1;

public class Solution : ISolution
{
    string[] numbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

    public void Run()
    {
        var data = File.ReadAllText("Day1\\Input1.txt").Split(Environment.NewLine);

        var values = new List<int>();
        foreach (var line in data)
        {
            int? first = null;
            int? last = null;
            for (int i = 0; i < line.Length; i++)
            {
                var number = GetNumber(line, i);
                if (number != null)
                {
                    if (first == null)
                    {
                        first = number;
                        last = number;
                    }
                    else
                    {
                        last = number;
                    }
                }
            }
            values.Add(int.Parse($"{first}{last}"));
        }

        OutputBuilder builder = new();
        builder.AppendLines(values.Select(x => x.ToString()));
        builder.AppendLine("Calibration Document");
        builder.AppendLine(values.Sum().ToString());

        File.WriteAllText("Day1\\Output.txt", builder.ToString());
    }

    private int? GetNumber(string line, int i)
    {
        if (char.IsDigit(line[i]))
        {
            return int.Parse(line[i].ToString());
        }

        var check = line.Substring(i);
        for (int n = 0; n < numbers.Length; n++)
        {
            if (check.StartsWith(numbers[n]))
            {
                return n + 1;
            }
        }

        return null;
    }
}