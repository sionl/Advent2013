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
        var cards = ReadWinners();

        var totalCards = 0;
        var iteration = 1;
        var cardsReminding = cards.Count(x => x.Value.Count > 0);
        while (cardsReminding > 0)
        {
            totalCards += cardsReminding;
            ProcessResults(cards);
            cardsReminding = cards.Count(x => x.Value.Count > 0);
            iteration += 1;
        }

        foreach (var card in cards.Values)
        {
            builder.AppendLine($"{card.Number} - {card.Winners} - {card.Count}");
        }

        builder.AppendNewLine();
        builder.AppendLine($"Total Cards: {totalCards}");

        builder.WriteToFile("Day4\\Output.txt");
    }

    private Dictionary<int, Card> ProcessResults(Dictionary<int, Card> cards)
    {
        foreach (var card in cards.Values)
        {
            if (card.Count > 0)
            {
                card.Count -= 1;
                if (card.Winners > 0)
                {
                    for (int i = 1; i <= card.Winners; i++)
                    {
                        var cardNumber = card.Number + i;
                        cards[cardNumber].Count += 1;
                    }
                }
            }
        }
        return cards;
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
            var card = new Card() { Number = cardNumber, Winners = winners, Count = 1 };
            cardSets.Add(cardNumber, card);
            cardNumber += 1;
        };
        return cardSets;
    }
}