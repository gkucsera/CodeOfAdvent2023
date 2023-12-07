namespace Day7;

public class Hand : IComparable<Hand>
{
    private int[] Cards { get; }
    public int Bid { get; }
    private CardCombo Combo { get; }

    public Hand(int[] cards, int bid, bool useJoker)
    {
        Cards = cards;
        Bid = bid;
        Combo = GetCombo(cards, useJoker);
    }

    private static CardCombo GetCombo(int[] cards, bool userJoker)
    {
        var numberOfCards = new Dictionary<int, int>();
        foreach (var card in cards)
        {
            if (numberOfCards.ContainsKey(card))
            {
                numberOfCards[card]++;
            }
            else
            {
                numberOfCards[card] = 1;
            }
        }

        return userJoker ? CreateComboWithJoker(numberOfCards) : CreateCombo(numberOfCards);
    }

    private static CardCombo CreateCombo(Dictionary<int, int> numberOfCards)
    {
        return numberOfCards.Count switch
        {
            5 => CardCombo.HighCard,
            4 => CardCombo.OnePair,
            3 when numberOfCards.Any(item => item.Value == 3) => CardCombo.ThreeOfAKind,
            3 when numberOfCards.MaxBy(item => item.Value).Value == 2 => CardCombo.TwoPair,
            2 when numberOfCards.Any(item => item.Value == 3) => CardCombo.FullHouse,
            2 when numberOfCards.Any(item => item.Value == 4) => CardCombo.FourOfAKind,
            1 => CardCombo.FiveOfAKind,
            _ => throw new Exception("unknown card combo")
        };
    }

    private static CardCombo CreateComboWithJoker(Dictionary<int, int> numberOfCards)
    {
        var hasJoker = numberOfCards.TryGetValue(1, out var jokerCount);
        if (!hasJoker)
        {
            return CreateCombo(numberOfCards);
        }

        var bestCombo = CardCombo.HighCard;
        for (var substitute = 2; substitute < 14; substitute++)
        {
            var substituted = new Dictionary<int, int>(numberOfCards);
            substituted.Remove(1);
            if (substituted.ContainsKey(substitute))
            {
                substituted[substitute] += jokerCount;
            }
            else
            {
                substituted[substitute] = jokerCount;
            }

            var combo = CreateCombo(substituted);
            if (combo > bestCombo)
            {
                bestCombo = combo;
            }
        }

        return bestCombo;
    }

    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Combo < other.Combo)
        {
            return -1;
        }

        if (Combo > other.Combo)
        {
            return 1;
        }

        for (var i = 0; i < Cards.Length; i++)
        {
            if (Cards[i] < other.Cards[i])
            {
                return -1;
            }

            if (Cards[i] > other.Cards[i])
            {
                return 1;
            }
        }

        return 0;
    }
}