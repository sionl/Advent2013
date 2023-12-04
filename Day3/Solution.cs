using System.Xml.Schema;

namespace Day3;

public class Solution : ISolution
{
    private OutputBuilder builder = new();
    private HashSet<char> symbolList = new HashSet<char>() { '*', '#', '+', '$', '@', '=', '%', '/', '-', '&' };
    private int maxX = 0;
    private int maxY = 0;

    public void Run()
    {
        try
        {
            var data = File.ReadAllLines("Day3\\Input1.txt");

            var lengthX = data[0].Length;
            var lengthY = data.Length;
            var array = new char[lengthY, lengthX];

            maxX = lengthX - 1;
            maxY = lengthY - 1;
            builder.AppendLine($"maxX: {maxX}");
            builder.AppendLine($"maxY: {maxY}");
            builder.AppendNewLine();

            int partsTotal = 0;

            // Read data into Array
            for (int y = 0; y < data.Length; y++)
            {
                string line = data[y];
                for (int x = 0; x < line.Length; x++)
                {
                    array[y, x] = line[x];
                }
            }


            // Find Numbers and check parts
            for (int y = 0; y < array.GetLength(0); y++)
            {
                string number = string.Empty;
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    var ch = array[y, x];
                    if (char.IsDigit(ch))
                    {
                        number += ch;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(number))
                        {
                            builder.AppendLine($"Number: {number}");
                            var isPartNumber = IsPartNumber(array, y, x - number.Length, x - 1);
                            builder.AppendLine($"Part: {isPartNumber}");
                            builder.AppendNewLine();
                            if (isPartNumber)
                            {
                                partsTotal += int.Parse(number);
                            }
                        }
                        number = string.Empty;
                    }
                }
                if (!string.IsNullOrEmpty(number))
                {
                    builder.AppendLine($"Number: {number}");
                    var isPartNumber = IsPartNumber(array, y, array.GetLength(1) - number.Length, array.GetLength(1) - 1);
                    builder.AppendLine($"Part: {isPartNumber}");
                    builder.AppendNewLine();
                    if (isPartNumber)
                    {
                        partsTotal += int.Parse(number);
                    }
                }
            }

            builder.AppendLine($"Parts Total: {partsTotal}");
        }
        finally
        {
            // Output Result
            builder.WriteToFile("Day3\\Output.txt");
        }
    }

    private bool IsPartNumber(char[,] array, int yPoint, int xStart, int xEnd)
    {
        builder.AppendLine($"Checking xStart: {xStart}");
        builder.AppendLine($"Checking xEnd: {xEnd}");

        // Check Top
        var yTop = yPoint - 1;
        if (yTop >= 0 && yTop <= maxY)
        {
            for (int x = xStart - 1; x <= xEnd + 1; x++)
            {
                if (x >= 0 && x <= maxX)
                {
                    builder.AppendLine($"Checking top: {x}");
                    var ch = array[yTop, x];
                    if (symbolList.Contains(ch))
                    {
                        builder.AppendLine($"Found Symbol on Top: {ch}");
                        return true;
                    }
                }
                else
                {
                    builder.AppendLine($"Skipping top: {x}");
                }
            }
        }


        // Check Right
        var xRight = xEnd + 1;
        if (xRight >= 0 && xRight <= maxX)
        {
            builder.AppendLine($"Checking right: {xRight}");
            var ch = array[yPoint, xRight];
            if (symbolList.Contains(ch))
            {
                builder.AppendLine($"Found Symbol on right: {ch}");
                return true;
            }
        }

        // Check Bottom
        var yBottom = yPoint + 1;
        if (yBottom >= 0 && yBottom <= maxY)
        {
            builder.AppendLine($"Checking bottom Y: {yBottom}");
            for (int x = xStart - 1; x <= xEnd + 1; x++)
            {
                if (x >= 0 && x <= maxX)
                {
                    builder.AppendLine($"Checking bottom: {x}");
                    var ch = array[yBottom, x];
                    if (symbolList.Contains(ch))
                    {
                        builder.AppendLine($"Found Symbol on Bottom: {ch}");
                        return true;
                    }
                }
                else
                {
                    builder.AppendLine($"Skipping bottom: {x}");
                }
            }
        }

        // Check left
        var xLeft = xStart - 1;
        if (xLeft >= 0 && xLeft <= maxX)
        {
            builder.AppendLine($"Checking left: {xLeft}");
            var ch = array[yPoint, xLeft];
            if (symbolList.Contains(ch))
            {
                builder.AppendLine($"Found Symbol on left: {ch}");
                return true;
            }
        }

        return false;
    }
}