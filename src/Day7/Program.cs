// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day7;

var inputLines = File.ReadAllLines("input.txt");
var hands = CreateHands(inputLines, false).ToArray();
var stopWatch = new Stopwatch();

stopWatch.Start();
Array.Sort(hands);
long result = 0;
for (var i = 0; i < hands.Length; i++)
{
    result += hands[i].Bid * (i + 1);
}

stopWatch.Stop();
Console.WriteLine($"first: {result} - {stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");

var handsWithJokers = CreateHands(inputLines, true).ToArray();
stopWatch.Restart();
stopWatch.Start();
Array.Sort(handsWithJokers);
result = 0;
for (var i = 0; i < handsWithJokers.Length; i++)
{
    result += handsWithJokers[i].Bid * (i + 1);
}

stopWatch.Stop();
Console.WriteLine($"second: {result} - {stopWatch.Elapsed.Minutes}:{stopWatch.Elapsed.Seconds}:{stopWatch.Elapsed.Milliseconds}");
return;

IEnumerable<Hand> CreateHands(IEnumerable<string> input, bool useJoker)
{
    foreach (var line in input)
    {
        var split = line.Split(" ");
        var cards = split[0].Select(item => useJoker ? ParseCardWithJoker(item): ParseCard(item)).ToArray();
        var bid = int.Parse(split[1]);
        yield return new Hand(cards, bid, useJoker);
    }
}

int ParseCard(char value)
{
    return value switch
    {
        'T' => 10,
        'J' => 11,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => int.Parse(value.ToString())
    };
}
int ParseCardWithJoker(char value)
{
    return value switch
    {
        'T' => 10,
        'J' => 1,
        'Q' => 11,
        'K' => 12,
        'A' => 13,
        _ => int.Parse(value.ToString())
    };
}