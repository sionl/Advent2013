using System.Globalization;

namespace Day4;

public class Solution : ISolution
{
    private class Card()
    {
        public int Number { get; set; }
        public int Winners { get; set; }
        public int Count { get; set; }
    }

    OutputBuilder builder = new();

    public void Run()
    {
        var cardSets = ReadWinners();

        foreach (var card in cardSets.Values)
        {
            builder.AppendLine($"{card.Number} - {card.Winners} - {card.Count}");
        }

        builder.WriteToFile("Day4\\Output.txt");
    }

    private Dictionary<int, Card> ReadWinners()
    {
        var cardSets = new Dictionary<int, Card>();
        var data = File.ReadAllText("Day4\\Input1.txt").Split(Environment.NewLine);
        var cardNumber = 1;
        foreach (var line in data)
        {
            var cardArray = line.Split(':');
            var array = cardArray[1].Split('|');
            var winnersLine = array[0].Trim();
            var numbersLine = array[1].Trim();

            var winnersList = winnersLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToHashSet();
            var numbersList = numbersLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim()));

            var winners = 0;
            foreach (var num in numbersList)
            {
                if (winnersList.Contains(num))
                {
                    winners += 1;
                }
            }
            cardNumber += 1;
            var card = new Card() { Number = cardNumber, Winners = winners, Count = 1 };
            cardSets.Add(cardNumber, card);
        };
        return cardSets;
    }
}