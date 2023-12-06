// See https://aka.ms/new-console-template for more information

using Day6;

var inputLines = File.ReadAllLines("input.txt");
var races = CreateRaces(inputLines);
var winningCounts = races.Select(item => item.GetWinningCount());

var firstResult = winningCounts.Aggregate((long)1, (current, winningCount) => current * winningCount);
Console.WriteLine($"first: {firstResult}");

var race = CreateRace(inputLines);
var result = race.GetWinningCount();
Console.WriteLine($"second: {result}");
return;

IEnumerable<Race> CreateRaces(string[] lines)
{
    var times = lines[0].Split(":")[1].Trim().Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(long.Parse).ToArray();
    var distances = lines[1].Split(":")[1].Trim().Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(long.Parse).ToArray();

    for (var i = 0; i < times.Length; i++)
    {
        yield return new Race(times[i], distances[i]);
    }
}
Race CreateRace(string[] lines)
{
    var timeString = lines[0].Split(":")[1].Replace(" ", "");
    var distanceString = lines[1].Split(":")[1].Replace(" ", "");

    return new Race(long.Parse(timeString), long.Parse(distanceString));
}